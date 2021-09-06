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
        private ObservableCollection<UserManageTreeModel> parentGroupList = new ObservableCollection<UserManageTreeModel>();
        private UserManageTreeModel editItem = null;

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
            if (editItem == null)
            {
                UserManageTreeModel parentGroupNode = new UserManageTreeModel(0, string.Empty, string.Empty, Visibility.Visible);
                parentGroupList.Add(parentGroupNode);
                editItem = parentGroupNode;
            }
        }

        public void CallChildAddClick(UserManageTreeModel selectedItem)
        {
            if (editItem == null)
            {
                UserManageTreeModel userGroupNode = new UserManageTreeModel(1, string.Empty, string.Empty, Visibility.Visible);
                selectedItem.ChildGroupList.Add(userGroupNode);
                editItem = userGroupNode;
                editItem.ParentPrimaryKey = selectedItem.PrimaryKey;
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
                        string parentGroupDeleteQuery = string.Format("DELETE FROM {0} WHERE parent_group_id = '{1}'", "userparentgroup", selectedItem.PrimaryKey);
                        MainWindow.DBManger.MySqlQueryExecuter(parentGroupDeleteQuery);
                        break;
                    case 1:
                        foreach (UserManageTreeModel item in parentGroupList)
                        {
                            if (item.ChildGroupList.Remove(selectedItem))
                            {
                                string userGroupDeleteQuery = string.Format("DELETE FROM {0} WHERE group_id = '{1}'", "usergroup", selectedItem.PrimaryKey);
                                MainWindow.DBManger.MySqlQueryExecuter(userGroupDeleteQuery);
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void CallRenameClick(UserManageTreeModel selectedItem)
        {
            if (editItem == null)
            {
                selectedItem.InputHeader = selectedItem.Header;
                selectedItem.IsHeader = Visibility.Collapsed;
                selectedItem.IsEdit = Visibility.Visible;
                selectedItem.IsEditFocus = true;

                editItem = selectedItem;
            }
        }

        public void CallTreeEditSave(string inputHeader, UserManageTreeModel selectedItem)
        {
            // User Add Save
            if (editItem.PrimaryKey == string.Empty)
            {
                string tableName = string.Empty;
                string columnString = string.Empty;
                string insertQuery = string.Empty;
                string selectQuery = string.Empty;

                switch (editItem.DepthCount)
                {
                    case 0:
                        if(DuplicateNameCheck(inputHeader, parentGroupList))
                        {
                            tableName = "userparentgroup";
                            columnString = "parent_group_id";
                            insertQuery = string.Format("INSERT INTO {0}(parent_group_name) VALUES ('{1}')", tableName, inputHeader);
                            selectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_name = '{1}'", tableName, inputHeader);
                        }
                        else { return; }
                        break;


                    case 1:
                        if(DuplicateNameCheck(inputHeader, selectedItem.ChildGroupList))
                        {
                            tableName = "usergroup";
                            columnString = "group_id";
                            insertQuery = string.Format("INSERT INTO {0}(group_name, parent_group_id) VALUES ('{1}', '{2}')", tableName, inputHeader, selectedItem.PrimaryKey);
                            selectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_id = '{1}' AND group_name = '{2}'", tableName, selectedItem.PrimaryKey, inputHeader);
                        }
                        else { return; }
                        break;

                    default:
                        break;
       
                }

                if (MainWindow.DBManger.MySqlQueryExecuter(insertQuery))
                {
                    DataSet parentGroupDataSet = MainWindow.DBManger.Select(selectQuery, tableName);

                    editItem.PrimaryKey = parentGroupDataSet.Tables[0].Rows[0][columnString].ToString();
                    editItem.Header = inputHeader;
                    editItem.IsHeader = Visibility.Visible;
                    editItem.IsEdit = Visibility.Collapsed;
                    editItem = null;
                }
            }

            // User Rename Save
            else
            {
                if (inputHeader != editItem.Header)
                {
                    //DuplicateNameCheck(inputHeader, parentGroupList);
                    string updateQuery = (editItem.DepthCount == 0) ? string.Format("UPDATE {0} SET parent_group_name = '{1}' WHERE parent_group_id = '{2}'", "userparentgroup", inputHeader, editItem.PrimaryKey)
                                                                    : string.Format("UPDATE {0} SET group_name = '{1}' WHERE group_id = '{2}'", "usergroup", inputHeader, editItem.PrimaryKey);

                    MainWindow.DBManger.MySqlQueryExecuter(updateQuery);
                }

                editItem.Header = inputHeader;
                editItem.IsHeader = Visibility.Visible;
                editItem.IsEdit = Visibility.Collapsed;
                editItem = null;
            }
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

            if(inputHeader == string.Empty)
            {
                return messageBox.ShowMessageBox("이름을 입력해야 합니다.", 0);
            }
            else
            {
                foreach (UserManageTreeModel item in userGroupList)
                {
                    if (item.Header == inputHeader)
                    {
                        return messageBox.ShowMessageBox("동일한 이름이 있습니다.", 0);
                    }
                }
            }

            return true;
        }
    }
}
