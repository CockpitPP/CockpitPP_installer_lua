   M S B A M L   `   `   `  ����  CockpitPP_installer_lua(    "CockpitPP_installer_lua.MainWindow   5	   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5
   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          �Name$  MainWindows��5	   	    � Title$  ��5   	   $	��550��6   $	��450��6   $	��450��6+   $	��550��6:   $	��550��6J   $	��450��6Z   .��5      � $��0,2,-8,0q�6   .+�5   
   �� -   5         ��Name$ 	Lbl_Title��5       	  Title#���   6#   $
��Left=�6E   $��	10,10,0,0q�6`   $	��Top=�6s   $	��430��6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�    J� RenderTransformOrigin$ 	0.581,-12&�6�   �� 5   
   -   5         ��Name$ 
Btn_Update��5        
Btn_Update#���  6%   $��289,77,76,410q�6L   $ 0.4,-0.4&�6c   6�   �� 5   
   -   5        $ Lbl_Dcs_Versions��5        Lbl_Dcs_Versions#���  6*   $
��Left=�6W   $��	24,79,0,0q�6r   $	��Top=�6�   $��99��6�   �� 5   
   -   5         ��Name$ CB_Dcs_Versions��5      $
��Left=�6,   $��
128,80,0,0q�6G   $	��Top=�6[   $	��120��6s   6   �� 5   
   -   5        $ Lbl_IPs_Android��5        Lbl_IPs_Android#���  6)   $
��Left=�6U   $��
62,132,0,0q�6p   $	��Top=�6�   $	��204��6�   $��Center=�6�   �� 5   
   -   5         ��Name$ LB_IPs_Android��5      $
��Left=�6*   $	��179��6E   $��
62,158,0,0q�6R   $	��Top=�6f   $	��204��6~   �� 5   
   -   5         ��Name$ Txt_Ip��5      $
��Left=�6"   $��23��6=   $��271,160,0,0q�6I    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��120��6�   .��6�   ��5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   -   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6-   	 �� 
TargetType"	 ��6   
 �� 	Resources
 5         CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%�   &	       !� 5!      $
 Left=�6=   $ Center=�6O   $
 None=�6c    5"      �� 5#   "   $��192.168.1.10��6(   
�������6?   5%      5&       �� Triggers 5'      P� 5(       P� Property � �� �� Empty P� Value# Z 62   . �5)      ��  �� Property � �� �� Value# [ 6;   5*      P� 5+       � �� 62   Z� . �5,      ��  � ��# [ 6;   5-      P� 5.       �@J�IsKeyboardFocused . . �5/      ��  � ��
 �����6;   50      52      .��53      54      �� 56   
   -	   5        $ Btn_Add��56        Btn_Add#���  6"   $��289,188,76,299q�6F   $ 0.4,-0.4&�6^   6   �� 57   
   -
   5        $ 
Btn_Delete��57        
Btn_Delete#���  6%   $��289,223,76,264q�6L   $ 0.4,-0.4&�6d   6�   �� 59   
   -   5        $ Lbl_Dcs_Port��59        Lbl_Dcs_Port#���  6&   $
��Left=�6O   $��
58,345,0,0q�6j   $	��Top=�6~   �� 5:   
   -   5        $ Txt_Dcs_Port��5:      $
��Left=�6(   $��23��6C   $��146,345,0,0q�6O   $
 Wrap=�6d   $	��Top=�6x   $	��120��6�   6�   �� 5<   
   -   5        $ Lbl_Android_Port��5<        Lbl_Android_Port#���  6*   $
��Left=�6W   $��
58,376,0,0q�6r   $	��Top=�6�   �� 5=   
   -   5        $ Txt_Android_Port��5=      $
��Left=�6,   $��23��6G   $��146,379,0,0q�6S   $
 Wrap=�6h   $	��Top=�6|   $	��120��6�   6�   �� 5?   
   -   5        $ 	Btn_Apply��5?       	 	Btn_Apply#��� 	 6$   $��322,317,43,170q�6J   $ 0.4,-0.4&�6b   6�   �� 5A   
   -   5        $ Lbl_Ip��5A       

 Lbl_Ip#��� 
 6    $
��Left=�6C   $��
62,414,0,0q�6^   $	��Top=�6r   $��91��6�   $��18(�6�   �� 5B   
   -   5        $ Lbl_Local_Ip��5B      $�� ��6&   $
��Left=�61   $��142,414,0,0q�6L   $	��Top=�6a   $	��249��6y   $��18(�6�   �� 5D   
   -   5        $ 	Btn_Close��5D        	Btn_Close#���  6$   $��322,460,43,27q�6J   $ 0.4,-0.4&�6a   6�   �� 5F   
   -   5        $ Lbl_Lang��5F        Lbl_Lang#���  6"   $
��Left=�6G   $��
18,466,0,0q�6b   $	��Top=�6v   $��70��6�   �� 5G   
   -   5        $ CB_Lang��5G      $
��Left=�6$   $��
82,468,0,0q�6?   $	��Top=�6S   $	��120��6k   6w   5J      5K      