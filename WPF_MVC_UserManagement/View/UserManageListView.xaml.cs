using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
    }
}
