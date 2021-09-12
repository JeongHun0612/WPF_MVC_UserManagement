using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WPF_MVC_UserManagement.Library;
using WPF_MVC_UserManagement.Model;

namespace WPF_MVC_UserManagement.View
{
    /// <summary>
    /// UserManageListView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserManageListView : UserControl
    {
        public UserManageListView()
        {
            InitializeComponent();

            ControllerInit();
            UserGroupListInit();
        }

        private void ControllerInit()
        {
            MainWindow.userManageListController.delegateUserGroupList += SendUserGroupList;
        }

        private void UserGroupListInit()
        {
            MainWindow.userManageListController.CallUserGroupList();
        }

        private void SendUserGroupList(ObservableCollection<UserManageListModel> userGroupListData)
        {
            userListBox.ItemsSource = userGroupListData;
        }

        private void AddUserClick(object sender, RoutedEventArgs e)
        {
            MainWindow.userManageListController.CallAddUserClick();
        }

        private void EditUserClick(object sender, RoutedEventArgs e)
        {
            SelectedItem(sender, e);
            UserManageListModel selectedItem = userListBox.SelectedItem as UserManageListModel;
            MainWindow.userManageListController.CallEditUserClick(selectedItem);
        }

        private void CancleClick(object sender, RoutedEventArgs e)
        {
            MainWindow.userManageListController.CallCancleClick();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            MainWindow.userManageListController.CallSaveClick();
        }

        private void DeleteUserClick(object sender, RoutedEventArgs e)
        {
            SelectedItem(sender, e);
            UserManageListModel selectedItem = userListBox.SelectedItem as UserManageListModel;
            MainWindow.userManageListController.CallDeleteUserClick(selectedItem);
        }

        private void ProfileEditClick(object sender, RoutedEventArgs e)
        {
            SelectedItem(sender, e);
            UserManageListModel selectedItem = userListBox.SelectedItem as UserManageListModel;
            MainWindow.userManageListController.CallProfileEditClick();
        }

        private void SelectedItem(object sender, RoutedEventArgs e)
        {
            ListBoxItem listBoxItem = VisualUpward.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject);
            if (listBoxItem != null)
            {
                listBoxItem.IsSelected = true;
                e.Handled = true;
            }
        }
    }
}
