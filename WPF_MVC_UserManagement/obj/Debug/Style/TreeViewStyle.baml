   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 +  'TreeViewItem.TreeArrow.MouseOver.Stroke  �� Color ) %TreeViewItem.TreeArrow.MouseOver.Fill  ExpandCollapseToggleStyle �� 
TargetType �� Property �� Value �� 
TargetType F�Name ' #TreeViewToggleButtonBackgroundBrush �� Angle �� CenterY �� CenterX	 �� Triggers
 P� Property P� Value �� 
TargetName �� RelativeSource �� AncestorType  TreeViewForegroundBrush � RowDefinitions �Name ��Name J� SnapsToDevicePixels d�Name �� 	ClickMode d� 	IsChecked ��Name  TreeViewRectAngleBrush ��Name � ColumnDefinitions ��Name �� Value �� Binding ! TreeViewSelectForegroundBrush �� Triggers  TreeViewStyle ��Name  ScrollViewerControlTemplate�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %=  &	        &	 #     &	 F     7  7  7   7  'Q� p    7  7  7  7  &	 �
    �� 5      $  	#FF27C7F7��6F   �� 5      $  	#FFCCEEFB��6D   �� 5      " d�6.   .�5	   
   ��  � |� .  6&   �� 5
   
    � ��$ 20��6"   �� 5   
    � ��$
 Auto��6#   �� 5   
    � �� 5      �� 5      " d�6"   .R�5      �� 
������ $��Center=�66   $��Center=�6Q   ._�5      F� $ 
ExpandPath��6   "����        �     z � z     	8��  8�� z�5      �� 5   "   $	 135Q�62   $ 3Q�6>   $ 2Q�6J   5      5      	 5      P� 5      
 �@d�	IsChecked$
 TrueY�67   . �5      �� $ 
ExpandPath��6@    � z� 5   "   �� 5   &   $	 225Q�66   $ 3Q�6B   $ 2Q�6N   5      5      P� 5      
 �@J�IsMouseOver . . �5      �� $ 
ExpandPath��67   8   � ��6O   �� 5       $ 
ExpandPath��65   8   � ��6M   5!      5#      5%      5&      �� 5)      " Q�6*   .�5*   
   ��  � �� 67   �� *HorizontalContentAlignment+ �� " ���� 5+   
    � �� 65   �� *VerticalContentAlignment+ �� " ���� 5,   
    � ��8   6'   �� 5-   
    � ��$ 14(�6%   �� 5.   
    � ��$ /Fonts/#Roboto Light)�6'   �� 5/   
    � �� 50      �� 51      " Q�6"   .R�52      �  53      �� 54      �� 55      .+�57      � $ GridRow0��6,   $��0��6   .+�58      �� $ Bd��6%   #��z��61   #��z��6[   #��z��6�   $��0q�6�    . ._�59   "   � .+�5:   &   � $��Center=�60   $	��0,5q�6K   .+�5;   *   d� $ Expander��67   $ Press=�6I   8�� $��0,0,3,0q�6�    6[   �� *
IsExpanded+ �� *TemplatedParent+6�   �� 5<   *   $ 	GroupLine��64   $��2��6G   $y�	Collapsed=�6Q    . 8�� $��	0,-6,5,-6q�6�   �� 5=   *   $ PART_Header��6;   $��1��6P   $��Header��6`   $��Center=�6w   #��z��6�   # z 6�   5>   '   5?   #   5@      5A      � 5B      $��1��6    5C      �� 5D   "   $��19��63   $
��Auto �6A   �� 5E   "   $��* �63   .+�5G      �� $ 	ItemsHost��6-   $��2��6@   $��1��6T   $��1��6d   5H      5I      	 5J      �� 5K      $ 2��6E    6&   �� *
DepthCount+.a�5L      �� $ 	GroupLine��6;    � y�$ Visible=�6R   5M      P� 5N      
 �@Q�
IsExpanded .  . �5O      �� $ 	ItemsHost��6;    � y�$ 	Collapsed=�6R   5P      P� 5Q      
 �@��HasItems .  . �5R      �� $ Expander��6;    � y�$ Hidden=�6Q   5S      P� 5T      
 �@Q�
IsSelected . . �5U      �� 8   � ��6;   �� 5V       � ��$ /Fonts/#Roboto Bold)�6;   5W      5Y      5[       5\   
   P� 5]      
 �@%�IsVirtualizing . . �5^      ��  � �� 5_      �� 5`      .R�5a      $� 5b      5d      5e      5g      �� 5i      " R�6"   .�5j   
   ��  � ��$
 Auto=�6G   �� 5k   
    � ��$
 Auto=�6E   �� 5l   
    �@��PanningMode$
 Both=�65   �� 5m   
    �@��IsFlicksEnabled .  63   �� 5n   
    � ��$ Center=�65   �� 5o   
    � �� 5p      �� 5q      " R�6"   .R�5r      �� $ _tv_scrollviewer_��6'   #��z��6B   ��.  |�.  #��z��6�   #��z��6�   # z 6  #��z��6T  #���  6�  .��5s      �� 5t      	 5u      P� 5v      
 �@%�IsVirtualizing . . �5w      �� $ _tv_scrollviewer_��6A    � �� . 6`   5x      5z      5|       5}   
   P� 5~      
 �@%�IsVirtualizing . . �5      ��  � �� 5�      �� 5�      .R�5�      $� 5�      5�      5�      5�      5�      