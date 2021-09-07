using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVC_UserManagement.Model
{
    public class UserManageListModel : NotifierCollection
    {
        public UserManageListModel(string primaryKey, string userName, string userBirth, string userId, string userPw, string userDepartment, string userEmployeeNum, string userNumber, string userGroupName, string userGroupId)
        {
            this.primaryKey = primaryKey;
            this.userName = userName;
            this.userBirth = userBirth;
            this.userId = userId;
            this.userPw = userPw;
            this.userDepartment = userDepartment;
            this.userEmployeeNum = userEmployeeNum;
            this.userNumber = userNumber;
            this.userGroupName = userGroupName;
            this.userGroupId = userGroupId;
        }

        private string primaryKey = string.Empty;
        public string PrimaryKey
        {
            get { return this.primaryKey; }
            set { this.primaryKey = value; NotifyCollection("PrimaryKey"); }
        }

        private string userName = string.Empty;
        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; NotifyCollection("UserName"); }
        }

        private string userBirth = string.Empty;
        public string UserBirth
        {
            get { return this.userBirth; }
            set { this.userBirth = value; NotifyCollection("UserBirth"); }
        }

        private string userId = string.Empty;
        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; NotifyCollection("UserId"); }
        }

        private string userPw = string.Empty;
        public string UserPw
        {
            get { return this.userPw; }
            set { this.userPw = value; NotifyCollection("UserPw"); }
        }

        private string userDepartment = string.Empty;
        public string UserDepartment
        {
            get { return this.userDepartment; }
            set { this.userDepartment = value; NotifyCollection("UserDepartment"); }
        }

        private string userEmployeeNum = string.Empty;
        public string UserEmployeeNum
        {
            get { return this.userEmployeeNum; }
            set { this.userEmployeeNum = value; NotifyCollection("UserEmployeeNum"); }
        }

        private string userNumber = string.Empty;
        public string UserNumber
        {
            get { return this.userNumber; }
            set { this.userNumber = value; NotifyCollection("UserNumber"); }
        }

        private string userGroupName = string.Empty;
        public string UserGroupName
        {
            get { return this.userGroupName; }
            set { this.userGroupName = value; NotifyCollection("UserGroupName"); }
        }

        private string userGroupId = string.Empty;
        public string UserGroupId
        {
            get { return this.userGroupId; }
            set { this.userGroupId = value; NotifyCollection("UserGroupId"); }
        }
    }
}
