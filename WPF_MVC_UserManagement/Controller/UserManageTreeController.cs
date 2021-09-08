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
        public ObservableCollection<UserManageTreeModel> parentGroupList = new ObservableCollection<UserManageTreeModel>();
        public ObservableCollection<UserManageTreeModel> comboBoxGroupList = new ObservableCollection<UserManageTreeModel>();
        private UserManageTreeModel editItem = null;

        private string[] tableName = new string[3] { "userparentgroup", "usergroup", "users" };
        private string[] columnIdString = new string[3] { "parent_group_id", "group_id", "id" };

        public delegate void DelegateUserGroupTree(ObservableCollection<UserManageTreeModel> userGroupTreeData);
        public event DelegateUserGroupTree delegateUserGroupTree;

        public void CallUserGroupTree()
        {
            // ParentUserGroupSelect --------------------------------------------------------------------------------------------------------------------------------
            string parentGroupSelectQuery = string.Format("SELECT * FROM {0}", tableName[0]);
            DataSet parentGroupDataSet = MainWindow.DBManager.Select(parentGroupSelectQuery, tableName[0]);
            foreach (DataRow parentUserGroup in parentGroupDataSet.Tables[0].Rows)
            {
                string parentGroupPrimaryKey = parentUserGroup["parent_group_id"].ToString();
                string parentGroupHeader = parentUserGroup["parent_group_name"].ToString();
                UserManageTreeModel parentGroupNode = new UserManageTreeModel(0, parentGroupPrimaryKey, parentGroupHeader);
                parentGroupList.Add(parentGroupNode);
                comboBoxGroupList.Add(parentGroupNode);

                // UserGroupSelect ----------------------------------------------------------------------------------------------------------------------------------
                string userGroupSelectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_id = '{1}'", tableName[1], parentGroupPrimaryKey);
                DataSet userGroupDataSet = MainWindow.DBManager.Select(userGroupSelectQuery, tableName[1]);
                foreach (DataRow userGroup in userGroupDataSet.Tables[0].Rows)
                {
                    string userGroupPrimaryKey = userGroup["group_id"].ToString();
                    string userGroupHeader = userGroup["group_name"].ToString();
                    UserManageTreeModel userGroupNode = new UserManageTreeModel(1, userGroupPrimaryKey, parentGroupPrimaryKey, userGroupHeader);
                    parentGroupNode.ChildGroupList.Add(userGroupNode);
                    comboBoxGroupList.Add(userGroupNode);

                    // UserSelect -----------------------------------------------------------------------------------------------------------------------------------
                    string userSelectQuery = string.Format("SELECT * FROM {0} WHERE group_id = '{1}'", tableName[2], userGroupPrimaryKey);
                    DataSet userDataSet = MainWindow.DBManager.Select(userSelectQuery, tableName[2]);
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
            if (editItem == null)
            {
                editItem = new UserManageTreeModel(0);
                parentGroupList.Add(editItem);
            }
        }

        public void CallChildAddClick(UserManageTreeModel selectedItem)
        {
            if (editItem == null)
            {
                switch (selectedItem.DepthCount)
                {
                    case 0:
                        editItem = new UserManageTreeModel(1);
                        editItem.ParentPrimaryKey = selectedItem.PrimaryKey;
                        selectedItem.ChildGroupList.Add(editItem);
                        break;
                    case 1:
                        MainWindow.userManageListController.CallAddUserClick();
                        break;
                }
            }
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
                        foreach (UserManageTreeModel item in parentGroupList)
                        {
                            if (item.PrimaryKey == selectedItem.ParentPrimaryKey)
                            {
                                item.ChildGroupList.Remove(selectedItem);
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }

                string deleteQuery = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", tableName[selectedItem.DepthCount], columnIdString[selectedItem.DepthCount], selectedItem.PrimaryKey);
                MainWindow.DBManager.MySqlQueryExecuter(deleteQuery);
            }
        }

        public void CallRenameClick(UserManageTreeModel selectedItem)
        {
            if (editItem == null)
            {
                editItem = selectedItem;

                editItem.InputHeader = selectedItem.Header;
                editItem.IsHeader = Visibility.Collapsed;
                editItem.IsEdit = Visibility.Visible;
                editItem.IsEditFocus = true;
            }
        }

        public void CallTreeEditSave(UserManageTreeModel selectedItem)
        {
            // User Add Save
            if (editItem.PrimaryKey == string.Empty)
            {
                string insertQuery = string.Empty;
                string selectQuery = string.Empty;

                switch (editItem.DepthCount)
                {
                    case 0:
                        if (DuplicateNameCheck(editItem.InputHeader, parentGroupList) == false) { return; }
                        insertQuery = string.Format("INSERT INTO {0}(parent_group_name) VALUES ('{1}')", tableName[0], editItem.InputHeader);
                        selectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_name = '{1}'", tableName[0], editItem.InputHeader);
                        break;


                    case 1:
                        if (DuplicateNameCheck(editItem.InputHeader, selectedItem.ChildGroupList) == false) { return; }
                        insertQuery = string.Format("INSERT INTO {0}(group_name, parent_group_id) VALUES ('{1}', '{2}')", tableName[1], editItem.InputHeader, selectedItem.PrimaryKey);
                        selectQuery = string.Format("SELECT * FROM {0} WHERE group_name = '{1}' AND parent_group_id = '{2}'", tableName[1], editItem.InputHeader, selectedItem.PrimaryKey);
                        editItem.ParentPrimaryKey = selectedItem.PrimaryKey;
                        break;

                    default:
                        break;
                }

                MainWindow.DBManager.MySqlQueryExecuter(insertQuery);
                DataSet parentGroupDataSet = MainWindow.DBManager.Select(selectQuery, tableName[editItem.DepthCount]);
                editItem.PrimaryKey = parentGroupDataSet.Tables[0].Rows[0][columnIdString[editItem.DepthCount]].ToString();
            }

            // User Rename Save
            else
            {
                string updateQuery = string.Empty;

                if (editItem.Header != editItem.InputHeader)
                {
                    switch (editItem.DepthCount)
                    {
                        case 0:
                            if (DuplicateNameCheck(editItem.InputHeader, parentGroupList) == false) { return; }
                            updateQuery = string.Format("UPDATE {0} SET parent_group_name = '{1}' WHERE parent_group_id = '{2}'", tableName[0], editItem.InputHeader, editItem.PrimaryKey);
                            break;

                        case 1:
                            ObservableCollection<UserManageTreeModel> userGroupList = new ObservableCollection<UserManageTreeModel>();
                            foreach (UserManageTreeModel item in parentGroupList)
                            {
                                if (item.PrimaryKey == editItem.ParentPrimaryKey)
                                {
                                    userGroupList = item.ChildGroupList;
                                    break;
                                }
                            }

                            if (DuplicateNameCheck(editItem.InputHeader, userGroupList) == false) { return; }
                            updateQuery = string.Format("UPDATE {0} SET group_name = '{1}' WHERE group_id = '{2}'", tableName[1], editItem.InputHeader, editItem.PrimaryKey);
                            break;

                        default:
                            break;
                    }

                    MainWindow.DBManager.MySqlQueryExecuter(updateQuery);
                }
            }

            editItem.Header = editItem.InputHeader;
            editItem.IsHeader = Visibility.Visible;
            editItem.IsEdit = Visibility.Collapsed;
            editItem.IsEditFocus = false;
            editItem = null;
        }

        public void CallTreeEditCancle()
        {
            // User Add Cancle
            if (editItem.PrimaryKey == string.Empty)
            {
                switch (editItem.DepthCount)
                {
                    case 0:
                        parentGroupList.Remove(editItem);
                        break;
                    case 1:
                        foreach (UserManageTreeModel item in parentGroupList)
                        {
                            if (item.ChildGroupList.Remove(editItem))
                            {
                                break;
                            }
                        }
                        break;
                }
            }

            // User Rename Cancle
            else
            {
                editItem.IsHeader = Visibility.Visible;
                editItem.IsEdit = Visibility.Collapsed;
                editItem.IsEditFocus = false;
            }

            editItem = null;
        }

        private bool DuplicateNameCheck(string inputHeader, ObservableCollection<UserManageTreeModel> userGroupList)
        {
            WarningMessageBoxView messageBox = new WarningMessageBoxView();

            if (inputHeader == string.Empty)
            {
                return messageBox.ShowMessageBox("이름을 입력해야 합니다.", 0);
            }
            else
            {
                foreach (UserManageTreeModel item in userGroupList)
                {
                    if (item.Header == inputHeader)
                    {
                        editItem.IsEditFocus = false;
                        return messageBox.ShowMessageBox("동일한 이름이 있습니다.", 0);
                    }
                }
            }

            return true;
        }
    }
}
