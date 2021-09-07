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

namespace WPF_MVC_UserManagement.Controller
{
    public class UserManageListController
    {
        private ObservableCollection<UserManageListModel> userList = new ObservableCollection<UserManageListModel>();

        private int lastPrimaryKey = 0;
        private string[] tableName = new string[3] { "userparentgroup", "usergroup", "users" };
        private string[] columnIdString = new string[3] { "parent_group_id", "group_id", "id" };

        public delegate void DelegateUserGroupList(ObservableCollection<UserManageListModel> userGroupListData);
        public event DelegateUserGroupList delegateUserGroupList;

        public void CallUserGroupList()
        {
            string selectUserQuery = string.Format("SELECT * FROM {0}", tableName[2]);
            DataSet userDataSet = MainWindow.DBManger.Select(selectUserQuery, tableName[2]);

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

        public void CallSelectedItemChanged(int depthCount, UserManageTreeModel selectedItem)
        {
            foreach (UserManageListModel item in userList)
            {
                if(item.IsSelectedItem == Visibility.Visible)
                {
                    item.IsSelectedItem = Visibility.Collapsed;
                }
            }

            switch (depthCount)
            {
                case 0:
                    foreach(UserManageListModel item in userList)
                    {
                        if(item.ParentUserGroupId == selectedItem.PrimaryKey)
                        {
                            item.IsSelectedItem = Visibility.Visible;
                        }
                    }
                    break;
                case 1:
                    foreach (UserManageListModel item in userList)
                    {
                        if (item.UserGroupId == selectedItem.PrimaryKey)
                        {
                            item.IsSelectedItem = Visibility.Visible;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
