using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
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
        }

        private void ControllerInit()
        {
            MainWindow.userManageTreeController.delegateUserGroupTree += SendUserGroupTree;

        }

        //private void PopulateParentGroupTree()
        //{
        //    int index = 0;
        //    string parentGroupSelectQuery = string.Format("SELECT * FROM {0}", tableName[0]);
        //    DataSet parentGroupDataSet = MainWindowViewModel.manager.Select(parentGroupSelectQuery, tableName[0]);

        //    foreach (DataRow item in parentGroupDataSet.Tables[0].Rows)
        //    {
        //        string parentGroupHeader = item["parent_group_name"].ToString();
        //        string parentGroupPrimaryKey = item["parent_group_id"].ToString();
        //        UserManageTreeModel parentGroupNode = new UserManageTreeModel(index, 0, parentGroupPrimaryKey, string.Empty, parentGroupHeader, false, false);

        //        ParentGroupList.Add(parentGroupNode);
        //        UserGroupList.Add(new ComboBoxGroupListModel(parentGroupHeader, true, parentGroupPrimaryKey));
        //        PopulateUserGroupTree(parentGroupNode, parentGroupPrimaryKey);
        //        index++;
        //    }
        //}


        private void SendUserGroupTree(ObservableCollection<UserManageTreeModel> data)
        {
            UserTreeView.ItemsSource = data;
        }

        private void UserManageTreeViewLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.userManageTreeController.CallUserGroupTree();
        }
    }
}
