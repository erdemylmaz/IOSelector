   M S B A M L   `   `   `  ����E  AIOSelector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:IOSelector
IOSelector  5   	       IOSelector.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:IOSelector     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��48 �6   �� 5      .+�5   
   �� $��0��6   ���#aaa._�5      ]� $��Center=�6   $
��Left=�63   .+�5      �� $��Type: ��6   $��	12, 0,0,0q�6)   �� 5      -   5         ��Name$ type_combobox��5   8   $��96��6    �� SelectedIndex$ 0��6&   .@�5      �� -   5        $��combobox_osai��5   #   .��68   OSAI6>   �� 5      -   5        $��combobox_isac��5   #   .��68   ISAC6>   5      �� 5      $��
Language: ��6   $��	12, 0,0,0q�6-   �� 5      -   5        $ language_combobox��5   8   $��96��6   $ 0��6&   .@�5      �� -   5        $��combobox_eng��5   #   .��67   	English6@   �� 5      -   5        $��combobox_turkish��5   #   .��6;   Turkce6C   5      �� 5      -   5        $��Save Preferences��5      $��12, 0, 0, 0q�6L   $
��4, 0q�6a   6r   �� 5      -   5        $��Create IO File��5      $��12, 0, 0, 0q�6G   $
��4, 0q�6\   6m   5       5!      � 5#   
   $��1��6    � ColumnDefinitions 5$      �� 5%      �� 5&      .+�5*      �� $��0��6   $
��Auto=�6+   .��5+      �� -	   5         ��Name$ input_settings_area��5+      5,      5-      �� 5/      $��1��6   $
��Auto=�6+   .��50      �� -
   5        $ output_settings_area��50      51      52      53      54      55      