using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_MVC_UserManagement.Model;
using WPF_MVC_UserManagement.View;

namespace WPF_MVC_UserManagement.Controller
{
    public class UserManageListController
    {
        private ObservableCollection<UserManageListModel> userList = new ObservableCollection<UserManageListModel>();
        private UserManageListModel editItem = null;
        private UserManageListModel tempItem = null;
        private UserManageTreeModel treeSelectedItem = null;

        private int lastPrimaryKey = 0;
        private string[] tableName = new string[3] { "userparentgroup", "usergroup", "users" };
        private string[] columnIdString = new string[3] { "parent_group_id", "group_id", "id" };

        public delegate void DelegateUserGroupList(ObservableCollection<UserManageListModel> userGroupListData);
        public event DelegateUserGroupList delegateUserGroupList;

        public void CallUserGroupList()
        {
            string selectUserQuery = string.Format("SELECT * FROM {0}", tableName[2]);
            DataSet userDataSet = MainWindow.DBManager.Select(selectUserQuery, tableName[2]);

            foreach (DataRow item in userDataSet.Tables[0].Rows)
            {
                string primaryKey = item["id"].ToString();
                string userName = item["user_name"].ToString();
                string userBirth = item["user_birth"].ToString();
                string userId = item["user_id"].ToString();
                string userPw = item["user_pw"].ToString();
                string userDepartment = item["user_department"].ToString();
                string userEmployeeNum = item["user_employee_num"].ToString();
                string userNumber = item["user_number"].ToString();
                string userGroupName = item["group_name"].ToString();
                string userGroupId = item["group_id"].ToString();
                string userParentGroupId = item["parent_group_idx"].ToString();
                //byte[] blob = (item["user_image"].ToString() != string.Empty) ? (byte[])item["user_image"] : null;

                //UserListCollection.Add(new UserManageListModel(primaryKey, userName, userBirth, userId, userPw, userDepartment, userEmployeeNum, userNumber, userGroupName, userGroupId, UserImageShow(blob)));
                userList.Add(new UserManageListModel(primaryKey, userName, userBirth, userId, userPw, userDepartment, userEmployeeNum, userNumber, userGroupName, userGroupId, userParentGroupId));
                lastPrimaryKey = int.Parse(primaryKey);
            }

            this.delegateUserGroupList?.Invoke(userList);
        }

        public void CallSelectedItemChanged(UserManageTreeModel selectedItem)
        {
            treeSelectedItem = selectedItem;
            // userList 초기화
            UserManageListModel removeItem = null;
            foreach (UserManageListModel item in userList)
            {
                if (item.IsSelectedVisibility == Visibility.Visible)
                {
                    if (item.PrimaryKey == string.Empty)
                    {
                        removeItem = item;
                    }
                    else
                    {
                        item.IsSelectedVisibility = Visibility.Collapsed;
                    }
                }
            }
            if (removeItem != null)
            {
                userList.Remove(removeItem);
            }

            // userList Selected
            switch (selectedItem.DepthCount)
            {
                case 0:
                    foreach (UserManageListModel item in userList)
                    {
                        if (item.ParentUserGroupId == selectedItem.PrimaryKey)
                        {
                            item.IsSelectedVisibility = Visibility.Visible;
                        }
                    }
                    break;
                case 1:
                    foreach (UserManageListModel item in userList)
                    {
                        if (item.UserGroupId == selectedItem.PrimaryKey)
                        {
                            item.IsSelectedVisibility = Visibility.Visible;
                        }
                    }
                    break;
                case 2:
                    foreach (UserManageListModel item in userList)
                    {
                        if (item.PrimaryKey == selectedItem.PrimaryKey)
                        {
                            item.IsSelectedVisibility = Visibility.Visible;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void CallAddUserClick()
        {
            editItem = new UserManageListModel();
            userList.Insert(0, editItem);

            if (treeSelectedItem != null && treeSelectedItem.ChildGroupList.Count > 0)
            {
                editItem.UserGroupList = MainWindow.userManageTreeController.comboBoxGroupList;
                switch (treeSelectedItem.DepthCount)
                {
                    case 0:
                        editItem.SelectedComboBoxItem = treeSelectedItem.ChildGroupList[0];
                        break;
                    case 1:
                        editItem.SelectedComboBoxItem = treeSelectedItem;
                        break;
                    default:
                        break;
                }
            }
        }

        public void CallEditUserClick(UserManageListModel selectedItem)
        {
            if(editItem == null)
            {
                editItem = selectedItem;
                tempItem = new UserManageListModel();
                TempItemChanged(tempItem, editItem);

                // ComboBox 초기화
                editItem.UserGroupList = MainWindow.userManageTreeController.comboBoxGroupList;
                switch (treeSelectedItem.DepthCount)
                {
                    case 0:
                        foreach (UserManageTreeModel item in treeSelectedItem.ChildGroupList)
                        {
                            if (item.PrimaryKey == editItem.UserGroupId)
                            {
                                editItem.SelectedComboBoxItem = item;
                            }
                        }
                        break;
                    case 1:
                        editItem.SelectedComboBoxItem = treeSelectedItem;
                        break;
                    default:
                        break;
                }

                editItem.IsReadOnly = false;
                editItem.IsEditModeVisibility = Visibility.Visible;
                editItem.IsUserGroupList = Visibility.Visible;
            }
        }

        public void CallCancleClick(UserManageListModel selectedItem)
        {
            if (editItem.PrimaryKey == string.Empty)
            {
                userList.Remove(editItem);
            }
            else
            {
                TempItemChanged(editItem, tempItem);

                editItem.IsReadOnly = true;
                editItem.IsEditModeVisibility = Visibility.Collapsed;
                editItem.IsUserGroupList = Visibility.Collapsed;
                editItem = null;
            }
        }

        public void CallSaveClick(UserManageListModel selectedItem)
        {
            WarningMessageBoxView messageBox = new WarningMessageBoxView();

            if (editItem.PrimaryKey == string.Empty)
            {
                string userSelectQuery = string.Format("SELECT * FROM {0} WHERE user_id = '{1}'", tableName[2], editItem.UserId);
                DataSet userDataSet = MainWindow.DBManager.Select(userSelectQuery, tableName[2]);

                if (editItem.UserName != string.Empty && editItem.UserBirth != string.Empty && editItem.UserId != string.Empty && editItem.UserPw != string.Empty && editItem.UserDepartment != string.Empty && editItem.UserEmployeeNum != string.Empty && editItem.UserNumber != string.Empty && editItem.SelectedComboBoxItem != null)
                {
                    if (userDataSet.Tables[0].Rows.Count > 0)
                    {
                        messageBox.ShowMessageBox("이미 사용중인 아이디입니다.", 0);
                    }

                    else
                    {
                        editItem.IsReadOnly = true;
                        editItem.IsNormalModeVisibility = Visibility.Visible;
                        editItem.IsEditModeVisibility = Visibility.Collapsed;
                        editItem.IsUserGroupList = Visibility.Collapsed;
                        editItem = null;
                    }
                }
                else
                {
                    messageBox.ShowMessageBox("값을 모두 입력해주세요.", 0);
                }
            }
        }

        public void CallDeleteUserClick(UserManageListModel selectedItem)
        {
            WarningMessageBoxView messageBox = new WarningMessageBoxView();

            if (messageBox.ShowMessageBox("모든 내용이 영구적으로 삭제됩니다.", 1))
            {
                string userDeleteQuery = string.Format("DELETE FROM {0} WHERE id = '{1}'", tableName[2], selectedItem.PrimaryKey);
                MainWindow.DBManager.MySqlQueryExecuter(userDeleteQuery);
                userList.Remove(selectedItem);
            }
        }

        private void TempItemChanged(UserManageListModel takeItem, UserManageListModel giveItem)
        {
            takeItem.UserName = giveItem.UserName;
            takeItem.UserBirth = giveItem.UserBirth;
            takeItem.UserId = giveItem.UserId;
            takeItem.UserDepartment = giveItem.UserDepartment;
            takeItem.UserEmployeeNum = giveItem.UserEmployeeNum;
            takeItem.UserNumber = giveItem.UserNumber;
        }
    }
}
