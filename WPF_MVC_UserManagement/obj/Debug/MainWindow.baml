   M S B A M L   `   `   `  ����Q  MWPF_MVC_UserManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:WPF_MVC_UserManagementWPF_MVC_UserManagement  5   	   I)clr-namespace:WPF_MVC_UserManagement.ViewWPF_MVC_UserManagement.View  5   	   '    !WPF_MVC_UserManagement.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:WPF_MVC_UserManagement     5   	   :view)clr-namespace:WPF_MVC_UserManagement.View     5   	     � WindowStyle$
  None=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	      MainBackgroundBrush#��[  5   	    � Title$ 
MainWindow��5   	   $
��1080��6   $
��1920��6*   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��830 �6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��90 �6   �� 5      $��* �6   .+�5   
   4   .WPF_MVC_UserManagement.View.WindowTitleBarView  $��0��6"   $��2��6/   /   )WPF_MVC_UserManagement.View.TitleNameView  5   
   $��1��6   $��0��6*   0   *WPF_MVC_UserManagement.View.SystemTimeView  5!   
   $��1��6   $��1��6+   � 5#   
   $��2��6   $��2��6   .+�5$      ��  �� TabStripPlacement$
 Left=�6     ContentBackgroundBrush#��[ 62   ��6g   Z� .@�5%      �� $��Config File��6     TabItemConfigFileStyle#���  6/   $��/Fonts/#Roboto)�6`   $��
0,75,0,-75q�6|   .��5&      �  5'      �� 5(      $	��350 �6/   �� 5)      $��5 �6/   �� 5*      $��* �6/    5,      �� 5-      $��* �6,   �� 5.      $	��165 �6,   .+�51      4   .WPF_MVC_UserManagement.View.UserManageTreeView  $��0��62   $��2��6B   � 52      $��1��6   $��2��6/   #��[  6@   $��0, -2q�6r   4   .WPF_MVC_UserManagement.View.UserManageListView  53      $��2��62   $��0��6B   -   'WPF_MVC_UserManagement.View.LogListView  54      $��2��6+   $��1��6;   55      56      �� 57      $��System Config��6     TabItemSystemConfigStyle#���  61   $��/Fonts/#Roboto)�6d   $��
0,75,0,-75q�6�   58      �� 59      $��User Manage��6     TabItemUserManageStyle#���  6/   $��/Fonts/#Roboto)�6`   $��
0,75,0,-75q�6}   5:      5;      5<      5=      5>      