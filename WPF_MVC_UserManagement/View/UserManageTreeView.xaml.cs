using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            MainWindow.userManageTreeController.CallDeleteClick();
        }

        private void TreeEditSave(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.Key == Key.Return)
            {
                MainWindow.userManageTreeController.CallTreeEditSave(textBox.Text);
            }
        }
    }
}
