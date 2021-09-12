using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_MVC_UserManagement.Model
{
    public class UserManageListModel : NotifierCollection
    {
        public UserManageListModel(string primaryKey, string userName, string userBirth, string userId, string userPw, string userDepartment, string userEmployeeNum, string userNumber, string userGroupName, string userGroupId, string parentUserGroupId, BitmapImage profileImage)
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
            this.parentUserGroupId = parentUserGroupId;
            this.profileImage = profileImage;
        }

        public UserManageListModel()
        {
            this.isReadOnly = false;
            this.isTextBoxFocus = true;
            this.isSelectedVisibility = Visibility.Visible;
            this.isNormalModeVisibility = Visibility.Collapsed;
            this.isEditModeVisibility = Visibility.Visible;
            this.isUserGroupList = Visibility.Visible;
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

        private string tempUserId = string.Empty;
        public string TempUserId
        {
            get { return this.tempUserId; }
            set { this.tempUserId = value; NotifyCollection("TempUserId"); }
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

        private ImageSource profileImage;
        public ImageSource ProfileImage
        {
            get { return this.profileImage; }
            set { this.profileImage = value; NotifyCollection("ProfileImage"); }
        }

        private ObservableCollection<UserManageTreeModel> userGroupList = new ObservableCollection<UserManageTreeModel>();
        public ObservableCollection<UserManageTreeModel> UserGroupList
        {
            get { return this.userGroupList; }
            set { this.userGroupList = value; NotifyCollection("UserGroupList"); }
        }

        private UserManageTreeModel selectedComboBoxItem = null;
        public UserManageTreeModel SelectedComboBoxItem
        {
            get { return this.selectedComboBoxItem; }
            set { this.selectedComboBoxItem = value; NotifyCollection("SelectedComboBoxItem"); }
        }

        private string parentUserGroupId = string.Empty;
        public string ParentUserGroupId
        {
            get { return this.parentUserGroupId; }
            set { this.parentUserGroupId = value; NotifyCollection("ParentUserGroupId"); }
        }

        private bool isReadOnly = true;
        public bool IsReadOnly
        {
            get { return this.isReadOnly; }
            set { this.isReadOnly = value; NotifyCollection("IsReadOnly"); }
        }

        private bool isTextBoxFocus = false;
        public bool IsTextBoxFocus
        {
            get { return this.isTextBoxFocus; }
            set { this.isTextBoxFocus = value; NotifyCollection("IsTextBoxFocus"); }
        }

        private Visibility isSelectedVisibility = Visibility.Visible;
        public Visibility IsSelectedVisibility
        {
            get { return this.isSelectedVisibility; }
            set { this.isSelectedVisibility = value; NotifyCollection("IsSelectedVisibility"); }
        }

        private Visibility isNormalModeVisibility = Visibility.Visible;
        public Visibility IsNormalModeVisibility
        {
            get { return this.isNormalModeVisibility; }
            set { this.isNormalModeVisibility = value; NotifyCollection("IsNormalModeVisibility"); }
        }

        private Visibility isEditModeVisibility = Visibility.Collapsed;
        public Visibility IsEditModeVisibility
        {
            get { return this.isEditModeVisibility; }
            set { this.isEditModeVisibility = value; NotifyCollection("IsEditModeVisibility"); }
        }

        private Visibility isUserGroupList = Visibility.Collapsed;
        public Visibility IsUserGroupList
        {
            get { return this.isUserGroupList; }
            set { this.isUserGroupList = value; NotifyCollection("IsUserGroupList"); }
        }
    }
}
