using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPF_MVC_UserManagement.Library;
using WPF_MVC_UserManagement.Model;

namespace WPF_MVC_UserManagement.View
{
    /// <summary>
    /// UserManageTreeView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserManageTreeView : UserControl
    {
        private TextBox testTextBox = new TextBox();
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
            UserManageTreeModel selectedItem = userTreeView.SelectedItem as UserManageTreeModel;
            TreeViewItem item = (TreeViewItem)userTreeView.ItemContainerGenerator.ContainerFromItem(selectedItem);
            if (item != null)
            {
                item.IsExpanded = true;
            }

            MainWindow.userManageTreeController.CallChildAddClick(selectedItem);
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            UserManageTreeModel selectedItem = userTreeView.SelectedItem as UserManageTreeModel;
            MainWindow.userManageTreeController.CallDeleteClick(selectedItem);
        }

        private void RenameClick(object sender, RoutedEventArgs e)
        {
            UserManageTreeModel selectedItem = userTreeView.SelectedItem as UserManageTreeModel;
            MainWindow.userManageTreeController.CallRenameClick(selectedItem);
        }

        private void TreeSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UserManageTreeModel selectedItem = userTreeView.SelectedItem as UserManageTreeModel;
            MainWindow.userManageListController.CallSelectedItemChanged(selectedItem);
        }

        private void TreeEditKeyDown(object sender, KeyEventArgs e)
        {
            UserManageTreeModel selectedItem = userTreeView.SelectedItem as UserManageTreeModel;

            if (e.Key == Key.Return)
            {
                MainWindow.userManageTreeController.CallTreeEditSave(selectedItem);

            }

            if (e.Key == Key.Escape)
            {
                MainWindow.userManageTreeController.CallTreeEditCancle();
            }
        }

        private void TreeEditLostFocus(object sender, RoutedEventArgs e)
        {
            UserManageTreeModel selectedItem = userTreeView.SelectedItem as UserManageTreeModel;
            TextBox textBox = sender as TextBox;
            testTextBox = textBox;

            if (textBox.Visibility == Visibility.Visible)
            {
                MainWindow.userManageTreeController.CallTreeEditSave(selectedItem);
            }

            //textBox.Focus();
        }

        private void TextBlock_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpward.VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject);
            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                e.Handled = true;
            }
        }
    }
}
