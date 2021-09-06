using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Windows;
using WPF_MVC_UserManagement.Model;
using WPF_MVC_UserManagement.View;

namespace WPF_MVC_UserManagement.Controller
{
    public class UserManageTreeController
    {
        ObservableCollection<UserManageTreeModel> parentGroupList = new ObservableCollection<UserManageTreeModel>();
        private UserManageTreeModel editItem = new UserManageTreeModel();

        public delegate void DelegateUserGroupTree(ObservableCollection<UserManageTreeModel> userGroupTreeData);
        public event DelegateUserGroupTree delegateUserGroupTree;

        public void CallUserGroupTree()
        {
            string[] tableName = new string[3] { "userparentgroup", "usergroup", "users" };

            // ParentUserGroupSelect --------------------------------------------------------------------------------------------------------------------------------
            string parentGroupSelectQuery = string.Format("SELECT * FROM {0}", tableName[0]);
            DataSet parentGroupDataSet = MainWindow.DBManger.Select(parentGroupSelectQuery, tableName[0]);
            foreach (DataRow parentUserGroup in parentGroupDataSet.Tables[0].Rows)
            {
                string parentGroupPrimaryKey = parentUserGroup["parent_group_id"].ToString();
                string parentGroupHeader = parentUserGroup["parent_group_name"].ToString();
                UserManageTreeModel parentGroupNode = new UserManageTreeModel(0, parentGroupPrimaryKey, parentGroupHeader);
                parentGroupList.Add(parentGroupNode);

                // UserGroupSelect ----------------------------------------------------------------------------------------------------------------------------------
                string userGroupSelectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_id = '{1}'", tableName[1], parentGroupPrimaryKey);
                DataSet userGroupDataSet = MainWindow.DBManger.Select(userGroupSelectQuery, tableName[1]);
                foreach (DataRow userGroup in userGroupDataSet.Tables[0].Rows)
                {
                    string userGroupPrimaryKey = userGroup["group_id"].ToString();
                    string userGroupHeader = userGroup["group_name"].ToString();
                    UserManageTreeModel userGroupNode = new UserManageTreeModel(1, userGroupPrimaryKey, userGroupHeader);
                    parentGroupNode.ChildGroupList.Add(userGroupNode);

                    // UserSelect -----------------------------------------------------------------------------------------------------------------------------------
                    string userSelectQuery = string.Format("SELECT * FROM {0} WHERE group_id = '{1}'", tableName[2], userGroupPrimaryKey);
                    DataSet userDataSet = MainWindow.DBManger.Select(userSelectQuery, tableName[2]);
                    foreach (DataRow user in userDataSet.Tables[0].Rows)
                    {
                        string primaryKey = user["id"].ToString();
                        string userName = user["user_name"].ToString();
                        UserManageTreeModel userNode = new UserManageTreeModel(2, primaryKey, userName);
                        userGroupNode.ChildGroupList.Add(userNode);
                    }
                }
            }

            this.delegateUserGroupTree?.Invoke(parentGroupList);
        }

        public void CallRootAddClick()
        {
            UserManageTreeModel parentGroupNode = new UserManageTreeModel(0, string.Empty, string.Empty, Visibility.Visible);
            parentGroupList.Add(parentGroupNode);
            editItem = parentGroupNode;
        }

        public void CallTreeEditCancle()
        {
            parentGroupList.Remove(editItem);
        }

        public void CallDeleteClick(UserManageTreeModel selectedItem)
        {
            WarningMessageBoxView messageBox = new WarningMessageBoxView();

            if (messageBox.ShowMessageBox("모든 내용이 영구적으로 삭제됩니다.", 1))
            {
                switch (selectedItem.DepthCount)
                {
                    case 0:
                        parentGroupList.Remove(selectedItem);
                        break;
                    case 1:

                        break;
                    default:
                        break;
                }
            }
        }

        public void CallTreeEditSave(string inputHeader)
        {
            string tableName = "userparentgroup";
            string parentGroupNameSelectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_name = '{1}'", tableName, inputHeader);
            DataSet parentGroupNameDataSet = MainWindow.DBManger.Select(parentGroupNameSelectQuery, tableName);
            if (parentGroupNameDataSet.Tables[0].Rows.Count > 0)
            {
                WarningMessageBoxView messageBox = new WarningMessageBoxView();
                messageBox.ShowMessageBox("동일한 이름이 있습니다.", 0);
            }
            else
            {
                string parentGroupInsertQuery = string.Format("INSERT INTO {0}(parent_group_name) VALUES ('{1}')", tableName, inputHeader);
                if (MainWindow.DBManger.MySqlQueryExecuter(parentGroupInsertQuery))
                {
                    string parentGroupSelectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_name = '{1}'", tableName, inputHeader);
                    DataSet parentGroupDataSet = MainWindow.DBManger.Select(parentGroupSelectQuery, tableName);
                    string primaryKey = parentGroupDataSet.Tables[0].Rows[0]["parent_group_id"].ToString();

                    editItem.PrimaryKey = primaryKey;
                    editItem.Header = inputHeader;
                    editItem.IsEdit = Visibility.Collapsed;
                }
            }
        }
    }
}
