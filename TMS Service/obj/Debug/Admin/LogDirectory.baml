   M S B A M L   `   `   `  ����F  BTMS Service, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:TMS_Service.AdminTMS_Service.Admin  5   	   $    TMS_Service.Admin.LogDirectory   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5	   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5
   	   1localclr-namespace:TMS_Service.Admin     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ Set Log Directory��5   	   $	��150��6#   $	��450��60   .��5      � .+�5   
   �� $��10q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Directory : ��6   $��25��6/   $
��Auto��6;   $	��Top=�6H   �� 5      -   5         ��Name$ directoryPath��5      $��25��61   $	��350��6=   $	��Top=�6I   5      �� 5      -   5        $ directoryErr��5      $��25��6,   $	��350��68   $	��Top=�6D   $��Center=�6\   $��0q�6y   
���<�� �� 
IsReadOnly . 6�   �� 5      $ 
Horizontal=�6   .+�5      �� -   5         ��Name$
 open��5      $��Open Log��6'   $��25��6:   $��75��6F   $
��Left=�6Q   $��	50,0,95,0q�6l   6   �� 5      -   5        $ create��5      $��Create��6)   $��25��6:   $��75��6F   $��Right=�6Q   $��	95,0,50,0q�6m   6�   5      5      5      5      