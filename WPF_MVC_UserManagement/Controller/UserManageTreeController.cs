using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVC_UserManagement.Model;

namespace WPF_MVC_UserManagement.Controller
{
    public class UserManageTreeController
    {
        public delegate void DelegateUserGroupTree(ObservableCollection<UserManageTreeModel> data);
        public event DelegateUserGroupTree delegateUserGroupTree;

        public void CallUserGroupTree()
        {
            ObservableCollection<UserManageTreeModel> parentGroupList = new ObservableCollection<UserManageTreeModel>();

            string[] tableName = new string[3] { "userparentgroup", "usergroup", "users" };

            // ParentUserGroupSelect -------------------------------------------------------------------------------------------------------------------------------
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
                    string userGroupHeader = userGroup["group_name"].ToString();
                    string userGroupPrimaryKey = userGroup["group_id"].ToString();
                    UserManageTreeModel userGroupNode = new UserManageTreeModel(1, userGroupPrimaryKey, userGroupHeader);
                    parentGroupNode.ChildGroupList.Add(userGroupNode);

                    // UserSelect ------------------------------------------------------------------------------------------------------------------------------------
                    string userSelectQuery = string.Format("SELECT * FROM {0} WHERE group_id = '{1}'", tableName[2], userGroupPrimaryKey);
                    DataSet userDataSet = MainWindow.DBManger.Select(userSelectQuery, tableName[2]);
                    foreach (DataRow item in userDataSet.Tables[0].Rows)
                    {
                        string primaryKey = item["id"].ToString();
                        string userName = item["user_name"].ToString();
                        UserManageTreeModel userNode = new UserManageTreeModel(2, primaryKey, userName);
                        userGroupNode.ChildGroupList.Add(userNode);
                    }
                }
            }

            this.delegateUserGroupTree?.Invoke(parentGroupList);
        }
    }
}
