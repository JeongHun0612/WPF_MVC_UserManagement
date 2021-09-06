﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPF_MVC_UserManagement.Model;

namespace WPF_MVC_UserManagement.View
{
    /// <summary>
    /// UserManageTreeView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserManageTreeView : UserControl
    {
        public UserManageTreeView()
        {
            InitializeComponent();

            ControllerInit();
            UserGroupListInit();
        }

        private void ControllerInit()
        {
            MainWindow.userManageTreeController.delegateUserGroupTree += SendUserGroupTree;
        }

        private void UserGroupListInit()
        {
            MainWindow.userManageTreeController.CallUserGroupTree();
        }

        private void SendUserGroupTree(ObservableCollection<UserManageTreeModel> userGroupTreeData)
        {
            userTreeView.ItemsSource = userGroupTreeData;
        }

        private void RootAddClick(object sender, RoutedEventArgs e)
        {
            MainWindow.userManageTreeController.CallRootAddClick();
        }

        private void ChildAddClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ChildAdd Click");
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            UserManageTreeModel selectedItem = userTreeView.SelectedItem as UserManageTreeModel;
            MainWindow.userManageTreeController.CallDeleteClick(selectedItem);
        }

        private void TreeEditKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == Key.Return)
            {
                MainWindow.userManageTreeController.CallTreeEditSave(textBox.Text);
            }

            if (e.Key == Key.Escape)
            {
                MainWindow.userManageTreeController.CallTreeEditCancle();
            }
        }

        private void TextBlock_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                e.Handled = true;
            }
        }

        static T VisualUpwardSearch<T>(DependencyObject source) where T : DependencyObject
        {
            DependencyObject returnVal = source;

            while (returnVal != null && !(returnVal is T))
            {
                DependencyObject tempReturnVal = null;
                if (returnVal is Visual || returnVal is Visual3D)
                {
                    tempReturnVal = VisualTreeHelper.GetParent(returnVal);
                }
                if (tempReturnVal == null)
                {
                    returnVal = LogicalTreeHelper.GetParent(returnVal);
                }
                else returnVal = tempReturnVal;
            }

            return returnVal as T;
        }
    }
}
