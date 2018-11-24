/************************************************
** MainWindow.xaml.cs	                       **
** Author : HERR Nicolas			           **
** GitHub : https://github.com/CockpitPP       **
** Created on 17/11/2018				       **
** Modified on 21/11/2018				       **
** Description : Main Class 			       **
************************************************/

using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Input;
using System.Net;
using System.Net.Sockets;

namespace CockpitPP_installer_lua
{

    public partial class MainWindow : Window
    {
        public ResourceDictionary obj;
        private string USERPROFILFOLDER;
        private string SCRIPTFOLDER;

        /// <summary>
        /// Constructor of the main class.
        /// Initialize variables, language, Local IP, DCS version saved and read data from LUA file
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            foreach (ResourceDictionary item in Application.Current.Resources.MergedDictionaries)
            {
                if (item.Source != null && item.Source.OriginalString.Contains(@"Resources\"))
                {
                    obj = item;
                }
            }

            USERPROFILFOLDER = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Initialize_CB_Lang();

            Initialize_CB_Dcs_Versions();

            Lbl_Ip.Content += GetLocalIP();
        }

        /// <summary>
        /// Change language program.
        /// </summary>
        /// <param name=dictionnaryUri>URI of the new language dictionnary file.</param>
        public void ChangeLanguage(Uri dictionnaryUri)
        {
            if (String.IsNullOrEmpty(dictionnaryUri.OriginalString) == false)
            {
                ResourceDictionary objNewLanguageDictionary = new ResourceDictionary { Source = dictionnaryUri };

                if (objNewLanguageDictionary != null)
                {
                    Application.Current.Resources.MergedDictionaries.Remove(obj);
                    Application.Current.Resources.MergedDictionaries.Add(objNewLanguageDictionary);

                    CultureInfo culture =
                       new CultureInfo((string)Application.Current.Resources["Culture"]);
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                }
            }
        }

        /// <summary>
        /// Initialize the ComboBox to choise the language and select the save language.
        /// </summary>
        private void Initialize_CB_Lang()
        {
            CB_Lang.Items.Add(new Item { Value = "fr-FR", Text = "Français" });
            CB_Lang.Items.Add(new Item { Value = "en-US", Text = "English" });

            foreach (Item item in CB_Lang.Items)
            {
                if (item.Value as String == Properties.Settings.Default.Lang)
                {
                    CB_Lang.SelectedItem = item;
                    break;
                }
            }
        }

        /// <summary>
        /// Initialize the ComboBox DCS versions. Search DCS folder in the user profil folder and select the save DCS version.
        /// Set the script folder for the LUA files.
        /// Read dat from the select DCS version.
        /// </summary>
        private void Initialize_CB_Dcs_Versions()
        {
            if (Directory.Exists(USERPROFILFOLDER + @"\Saved Games\DCS"))
                CB_Dcs_Versions.Items.Add(new Item { Value = "DCS", Text = "Release" });
            if (Directory.Exists(USERPROFILFOLDER + @"\Saved Games\DCS.openbeta"))
                CB_Dcs_Versions.Items.Add(new Item { Value = "DCS.openbeta", Text = "OpenBeta" });

            foreach (Item item in CB_Dcs_Versions.Items)
            {
                if (item.Value as String == Properties.Settings.Default.DCS_Version)
                {
                    CB_Dcs_Versions.SelectedItem = item;

                    SCRIPTFOLDER = USERPROFILFOLDER + @"\Saved Games\" + item.Value + @"\Scripts\";

                    break;
                }
            }
        }

        /// <summary>
        /// Read datas from LUA files and filled fields.
        /// Display warning if the LUA files is missing or not complete (the export.lua) -> need update.
        /// </summary>
        private void GetDatas()
        {

            if (File.Exists(SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit))
            {
                LB_IPs_Android.Items.Clear();

                foreach (string ip in ReadIPs(SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit).Split(','))
                {
                    LB_IPs_Android.Items.Add(ip);
                }

                Txt_Dcs_Port.Text = ReadDCSPort(SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit);
                Txt_Android_Port.Text = ReadAndroidPort(SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit);
            }
            else
            {
                MessageBox.Show((string)Application.Current.Resources["Warning_lua1"], (string)Application.Current.Resources["Warning_Title"], MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (!File.Exists(SCRIPTFOLDER + Properties.Settings.Default.LuaExport) || !MatchExportLua(SCRIPTFOLDER + Properties.Settings.Default.LuaExport))
            {
                MessageBox.Show((string)Application.Current.Resources["Warning_lua2"], (string)Application.Current.Resources["Warning_Title"], MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Read ip line. (android client)
        /// </summary>
        /// <param name=file>cockpit++.lua file</param>
        /// <returns>string, ips separate by , exemple : 192.168.1.1,192.168.1.2</returns>
        private string ReadIPs(string file)
        {
            Regex rx_IPLine = new Regex("^local clientIP=");
            if (File.Exists(file))
            {
                string[] lines = System.IO.File.ReadAllLines(file);

                foreach (string line in lines)
                {
                    if (rx_IPLine.IsMatch(line))
                    {
                        string IPsLine = line.Split('=')[1];
                        Regex rx = new Regex("[\"{}]");
                        IPsLine = rx.Replace(IPsLine, "");
                        IPsLine = IPsLine.Replace(" ", "");
                        return IPsLine;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// Read DCS port.
        /// </summary>
        /// <param name=file>cockpit++.lua file</param>
        /// <returns>string, DCS port/returns>
        private string ReadDCSPort(string file)
        {
            Regex rx = new Regex("^local DCS_PORT =");
            if (File.Exists(file))
            {
                string[] lines = System.IO.File.ReadAllLines(file);

                foreach (string line in lines)
                {
                    if (rx.IsMatch(line))
                    {
                        return line.Split('=')[1].Replace(" ", "");
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// Read DCS port.
        /// </summary>
        /// <param name=file>cockpit++.lua file</param>
        /// <returns>string, Android port/returns>
        private string ReadAndroidPort(string file)
        {
            Regex rx = new Regex("^local ANDROID_PORT =");
            if (File.Exists(file))
            {
                string[] lines = System.IO.File.ReadAllLines(file);

                foreach (string line in lines)
                {
                    if (rx.IsMatch(line))
                    {
                        return line.Split('=')[1].Replace(" ", "");
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// Write new IPs android client. 
        /// </summary>
        /// <param name=file>cockpit++.lua file</param>
        /// <param name=IPs>String array of the IPs</param>
        private void WriteIPs(string file, string[] IPs)
        {
            if (File.Exists(file))
            {
                Regex rx_IPLine = new Regex("^local clientIP=");
                string newtext = "";
                StreamReader sr = new StreamReader(file);
                string readline = ""; ;

                while ((readline = sr.ReadLine()) != null)
                {
                    if (rx_IPLine.IsMatch(readline))
                    {
                        string newline = "local clientIP={";
                        foreach (string ip in IPs)
                        {
                            newline += '"' + ip + "\",";
                        }
                        newline = newline.TrimEnd(',');
                        newline += "}";
                        newtext += newline + "\r\n";
                    }
                    else
                    {
                        newtext += readline + "\r\n";
                    }
                }
                sr.Close();

                // Ré-écriture du fichier
                StreamWriter sr2 = new StreamWriter(file);
                sr2.WriteLine(newtext);
                sr2.Close();

            }
        }

        /// <summary>
        /// Write new DCS port. 
        /// </summary>
        /// <param name=file>cockpit++.lua file</param>
        /// <param name=IPs>String DCS port</param>
        private void WriteDCSPort(string file, string port)
        {
            if (File.Exists(file))
            {
                Regex rx = new Regex("^local DCS_PORT =");
                string newtext = "";
                StreamReader sr = new StreamReader(file);
                string readline = ""; ;

                while ((readline = sr.ReadLine()) != null)
                {
                    if (rx.IsMatch(readline))
                    {
                        string newline = "local DCS_PORT = " + port;
                        newtext += newline + "\r\n";
                    }
                    else
                    {
                        newtext += readline + "\r\n";
                    }
                }
                sr.Close();

                // Ré-écriture du fichier
                StreamWriter sr2 = new StreamWriter(file);
                sr2.WriteLine(newtext);
                sr2.Close();

            }
        }

        /// <summary>
        /// Write new Android port. 
        /// </summary>
        /// <param name=file>cockpit++.lua file</param>
        /// <param name=IPs>String Android port</param>
        private void WriteAndroidPort(string file, string port)
        {
            if (File.Exists(file))
            {
                Regex rx = new Regex("^local ANDROID_PORT =");
                string newtext = "";
                StreamReader sr = new StreamReader(file);
                string readline = ""; ;

                while ((readline = sr.ReadLine()) != null)
                {
                    if (rx.IsMatch(readline))
                    {
                        string newline = "local ANDROID_PORT = " + port;
                        newtext += newline + "\r\n";
                    }
                    else
                    {
                        newtext += readline + "\r\n";
                    }
                }
                sr.Close();

                // Ré-écriture du fichier
                StreamWriter sr2 = new StreamWriter(file);
                sr2.WriteLine(newtext);
                sr2.Close();

            }
        }

        /// <summary>
        /// Test if the export.lua is OK (line needed present)
        /// </summary>
        /// <param name=file>export.lua file</param>
        /// <returns>true if the file is OK false if NOK/returns>
        private bool MatchExportLua(string file)
        {
            Regex rx = new Regex("^local Cockpitpp");
            if (File.Exists(file))
            {
                string[] lines = System.IO.File.ReadAllLines(file);

                foreach (string line in lines)
                {
                    if (rx.IsMatch(line.Trim()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Get the local IP.
        /// Connect a UDP socket and read its local endpoint.
        /// </summary>
        /// <returns>string : local IP/returns>
        private string GetLocalIP()
        {
            string localIP ="";
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }

        /// <summary>
        /// Event when the selection change on the ComboBox language.
        /// Change the language of the program and save it.
        /// </summary>
        private void CB_Lang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeLanguage(new Uri("pack://application:,,,/Resources/" + (CB_Lang.SelectedItem as Item).Value.ToString() + ".xaml"));
            Properties.Settings.Default.Lang = (CB_Lang.SelectedItem as Item).Value.ToString();

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Event when the selection change on the ComboBox DCS version.
        /// Save the new DCS version select ans change the script folder.
        /// Read data from from the DCS version select.
        /// </summary>
        private void CB_Dcs_Versions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.DCS_Version = (CB_Dcs_Versions.SelectedItem as Item).Value.ToString();

            Properties.Settings.Default.Save();

            SCRIPTFOLDER = USERPROFILFOLDER + @"\Saved Games\" + Properties.Settings.Default.DCS_Version + @"\Scripts\";

            GetDatas();
        }

        /// <summary>
        /// Event when user click on the button Add : Add IP in the list of android client.
        /// Read and test the ip (TextBox Txt_Ip) and add the the ListBox LB_IPs_Android
        /// Change background on red if the trext in texbox is no match.
        /// </summary>
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Regex rx_IP = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");

            if (rx_IP.IsMatch(Txt_Ip.Text))
            {
                Txt_Ip.Background = Brushes.White;
                LB_IPs_Android.Items.Add(Txt_Ip.Text);
            }
            else
            {
                Txt_Ip.Background = Brushes.Red;
            }
        }

        /// <summary>
        /// Event when user click on the button Delete.
        /// Delete the selected Client IP in the ListBox.
        /// </summary>
        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (LB_IPs_Android.SelectedItem != null)
                LB_IPs_Android.Items.Remove(LB_IPs_Android.SelectedItem);
        }

        /// <summary>
        /// Event when user click on the button Close.
        /// Ask and close the program.
        /// </summary>
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show((string)Application.Current.Resources["exit_text"], "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Environment.Exit(0);
        }

        /// <summary>
        /// Event when user click on the button Apply.
        /// Test if the client IP liste, DCS port and Android match and write it in the cockpit++.lua file
        /// </summary>
        private void Btn_Apply_Click(object sender, RoutedEventArgs e)
        {
            bool errorAndroidPort = false;
            bool errotDcsPort = false;
            bool errotIpsListe = false;

            if (Txt_Android_Port.Text != "" && Convert.ToInt32(Txt_Android_Port.Text) > 0 && Convert.ToInt32(Txt_Android_Port.Text) < 65535)
            {
                errorAndroidPort = false;
                Txt_Android_Port.Background = Brushes.White;
            }
            else
            {
                errorAndroidPort = true;
                Txt_Android_Port.Background = Brushes.Red;
            }

            if (Txt_Dcs_Port.Text != "" && Convert.ToInt32(Txt_Dcs_Port.Text) > 0 && Convert.ToInt32(Txt_Dcs_Port.Text) < 65535)
            {
                errotDcsPort = false;
                Txt_Dcs_Port.Background = Brushes.White;
            }
            else
            {
                errotDcsPort = true;
                Txt_Dcs_Port.Background = Brushes.Red;
            }

            if (LB_IPs_Android.Items.Count > 0)
            {
                errotIpsListe = false;
                LB_IPs_Android.Background = Brushes.White;
            }
            else
            {
                errotIpsListe = true;
                LB_IPs_Android.Background = Brushes.Red;
            }

            if (!errorAndroidPort && !errotDcsPort && !errotIpsListe)
            {
                if (File.Exists(SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit))
                {
                    string[] ips = new string[LB_IPs_Android.Items.Count];
                    LB_IPs_Android.Items.CopyTo(ips, 0);

                    WriteIPs(SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit, ips);

                    WriteDCSPort(SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit, Txt_Dcs_Port.Text);

                    WriteAndroidPort(SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit, Txt_Android_Port.Text);

                    MessageBox.Show((string)Application.Current.Resources["Apply"], "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        /// <summary>
        /// Event to prevent input on the TextBox DCS port and Android port.
        /// allow only number.
        /// </summary>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Event when user click on the button Update.
        /// Test if the client IP liste, DCS port and Android match.
        /// Download LUA files from github.
        /// Write it into the cockpit++.lua download file, save the cocpit++.lua destination file andd copy the cocpit++.luya download file.
        /// Test if the export.lua is OK (line needed present), if file missing copy the download file, if line missig add it into the file.
        /// </summary>
        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            bool errorAndroidPort = false;
            bool errotDcsPort = false;
            bool errotIpsListe = false;

            if (Txt_Android_Port.Text != "" && Convert.ToInt32(Txt_Android_Port.Text) > 0 && Convert.ToInt32(Txt_Android_Port.Text) < 65535)
            {
                errorAndroidPort = false;
                Txt_Android_Port.Background = Brushes.White;
            }
            else
            {
                errorAndroidPort = true;
                Txt_Android_Port.Background = Brushes.Red;
            }

            if (Txt_Dcs_Port.Text != "" && Convert.ToInt32(Txt_Dcs_Port.Text) > 0 && Convert.ToInt32(Txt_Dcs_Port.Text) < 65535)
            {
                errotDcsPort = false;
                Txt_Dcs_Port.Background = Brushes.White;
            }
            else
            {
                errotDcsPort = true;
                Txt_Dcs_Port.Background = Brushes.Red;
            }

            if (LB_IPs_Android.Items.Count > 0)
            {
                errotIpsListe = false;
                LB_IPs_Android.Background = Brushes.White;
            }
            else
            {
                errotIpsListe = true;
                LB_IPs_Android.Background = Brushes.Red;
            }

            if (!errorAndroidPort && !errotDcsPort && !errotIpsListe)
            {
                if (!Directory.Exists(Properties.Settings.Default.TempFolder))
                    Directory.CreateDirectory(Properties.Settings.Default.TempFolder);

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; //TLS 1.2
                WebClient wc = new WebClient();
                wc.DownloadFile(Properties.Settings.Default.URLGitHubLuaFiles, Properties.Settings.Default.TempFolder + @"\" + Properties.Settings.Default.ZipFile);

                System.IO.Compression.ZipFile.ExtractToDirectory(Properties.Settings.Default.TempFolder + @"\" + Properties.Settings.Default.ZipFile, Properties.Settings.Default.TempFolder);

                string pathLuaCockpitTemp = Properties.Settings.Default.TempFolder + @"\" + Properties.Settings.Default.UnzipFolder + @"\" + Properties.Settings.Default.LuaCockpit;

                if (File.Exists(pathLuaCockpitTemp))
                {
                    string[] ips = new string[LB_IPs_Android.Items.Count];
                    LB_IPs_Android.Items.CopyTo(ips, 0);

                    WriteIPs(pathLuaCockpitTemp, ips);

                    WriteDCSPort(pathLuaCockpitTemp, Txt_Dcs_Port.Text);

                    WriteAndroidPort(pathLuaCockpitTemp, Txt_Android_Port.Text);

                    File.Copy(SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit, SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit + ".old", true);
                    File.Copy(pathLuaCockpitTemp, SCRIPTFOLDER + Properties.Settings.Default.LuaCockpit, true);
                }

                if (!File.Exists(SCRIPTFOLDER + Properties.Settings.Default.LuaExport))
                {
                    File.Copy(Properties.Settings.Default.TempFolder + @"\" + Properties.Settings.Default.UnzipFolder + @"\" + Properties.Settings.Default.LuaExport, SCRIPTFOLDER + @"\" + Properties.Settings.Default.LuaExport);
                }
                else
                {
                    if (!MatchExportLua(SCRIPTFOLDER + Properties.Settings.Default.LuaExport))
                    {
                        File.Copy(SCRIPTFOLDER + Properties.Settings.Default.LuaExport, SCRIPTFOLDER + Properties.Settings.Default.LuaExport + ".old", true);
                        
                        StreamWriter sr = new StreamWriter(SCRIPTFOLDER + Properties.Settings.Default.LuaExport,true);
                        sr.WriteLine(Properties.Settings.Default.LineExport);
                        sr.Close();
                    }
                }

                Directory.Delete(Properties.Settings.Default.TempFolder,true);
                MessageBox.Show((string)Application.Current.Resources["UpdateComplete"], "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        /// <summary>
        /// Class for the ComboBox items.
        /// </summary>
        public class Item
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
