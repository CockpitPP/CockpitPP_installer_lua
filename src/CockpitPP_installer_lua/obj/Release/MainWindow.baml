   M S B A M L   `   `   `  ����   CockpitPP_installer_lua(    "CockpitPP_installer_lua.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  MainWindows�� � Title$  ��$	��550��$	��450��$	��450��$	��550��$	��550��$	��450��.��� $��0,2,-8,0q�.+��� -    ��Name$ 	Lbl_Title�� 	  Title#���   $
��Left=�$��	10,10,0,0q�$	��Top=�$	��430��$��Center=�$��18(�$
��Bold"� J� RenderTransformOrigin$ 	0.581,-12&��� -    ��Name$ 
Btn_Update��  
Btn_Update#���  $��289,77,76,410q�$ 0.4,-0.4&��� -   $ Lbl_Dcs_Versions��  Lbl_Dcs_Versions#���  $
��Left=�$��	24,79,0,0q�$	��Top=�$��99���� -    ��Name$ CB_Dcs_Versions��$
��Left=�$��
128,80,0,0q�$	��Top=�$	��120���� -   $ Lbl_IPs_Android��  Lbl_IPs_Android#���  $
��Left=�$��
62,132,0,0q�$	��Top=�$	��204��$��Center=��� -    ��Name$ LB_IPs_Android��$
��Left=�$	��179��$��
62,158,0,0q�$	��Top=�$	��204���� -    ��Name$ Txt_Ip��$
��Left=�$��23��$��271,160,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120��.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib     	 �� 
TargetType"	 ��
 �� 	Resources
   CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%Y   &	       !� $
 Left=�$ Center=�$
 None=� �� $��192.168.1.10��
������� �� Triggers P�  P� Property � �� �� Empty P� Value# Z . ���  �� Property � �� �� Value# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� -	   $ Btn_Add��  Btn_Add#���  $��289,188,76,299q�$ 0.4,-0.4&��� -
   $ 
Btn_Delete��  
Btn_Delete#���  $��289,223,76,264q�$ 0.4,-0.4&��� -   $ Lbl_Dcs_Port��  Lbl_Dcs_Port#���  $
��Left=�$��
58,345,0,0q�$	��Top=��� -   $ Txt_Dcs_Port��$
��Left=�$��23��$��146,345,0,0q�$
 Wrap=�$	��Top=�$	��120���� -   $ Lbl_Android_Port��  Lbl_Android_Port#���  $
��Left=�$��
58,376,0,0q�$	��Top=��� -   $ Txt_Android_Port��$
��Left=�$��23��$��146,379,0,0q�$
 Wrap=�$	��Top=�$	��120���� -   $ 	Btn_Apply�� 	 	Btn_Apply#��� 	 $��322,317,43,170q�$ 0.4,-0.4&��� -   $ Lbl_Ip�� 

 Lbl_Ip#��� 
 $
��Left=�$��
62,414,0,0q�$	��Top=�$��91��$��18(��� -   $ Lbl_Local_Ip��$�� ��$
��Left=�$��142,414,0,0q�$	��Top=�$	��249��$��18(��� -   $ 	Btn_Close��  	Btn_Close#���  $��322,460,43,27q�$ 0.4,-0.4&��� -   $ Lbl_Lang��  Lbl_Lang#���  $
��Left=�$��
18,466,0,0q�$	��Top=�$��70���� -   $ CB_Lang��$
��Left=�$��
82,468,0,0q�$	��Top=�$	��120��