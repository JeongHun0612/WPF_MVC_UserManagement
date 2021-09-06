using System.Collections.ObjectModel;
using System.Windows;

namespace WPF_MVC_UserManagement.Model
{
    public class UserManageTreeModel : NotifierCollection
    {
        public UserManageTreeModel()
        {
        }

        public UserManageTreeModel(int depthCount, string primaryKey, string header)
        {
            this.depthCount = depthCount;
            this.primaryKey = primaryKey;
            this.header = header;
            this.isEdit = Visibility.Collapsed;
        }

        public UserManageTreeModel(int depthCount, string primaryKey, string header, Visibility isEdit)
        {
            this.depthCount = depthCount;
            this.primaryKey = primaryKey;
            this.header = header;
            this.isEdit = isEdit;
            this.isEditFocus = true;
        }

        private int depthCount;
        public int DepthCount
        {
            get { return this.depthCount; }
            set { this.depthCount = value; NotifyCollection("DepthCount"); }
        }

        private string primaryKey;
        public string PrimaryKey
        {
            get { return this.primaryKey; }
            set { this.primaryKey = value; NotifyCollection("PrimaryKey"); }
        }

        private string header;
        public string Header
        {
            get { return this.header; }
            set { this.header = value; NotifyCollection("Header"); }
        }

        private Visibility isEdit;
        public Visibility IsEdit
        {
            get { return this.isEdit; }
            set { this.isEdit = value; NotifyCollection("IsEdit"); }
        }

        private bool isEditFocus;
        public bool IsEditFocus
        {
            get { return this.isEditFocus; }
            set { this.isEditFocus = value; NotifyCollection("IsEditFocus"); }
        }

        private ObservableCollection<UserManageTreeModel> childGroupList;
        public ObservableCollection<UserManageTreeModel> ChildGroupList
        {
            get
            {
                if (this.childGroupList == null)
                {
                    this.childGroupList = new ObservableCollection<UserManageTreeModel>();
                }
                return this.childGroupList;
            }
            set { this.childGroupList = value; }
        }

        public class ParentUserGroup : NotifierCollection
        {
            public ParentUserGroup(string name)
            {
                this.Name = name;
                this.Children = new ObservableCollection<UserGroup>();
            }

            public string Name { get; set; }
            public ObservableCollection<UserGroup> Children { get; set; }
        }

        public class UserGroup : NotifierCollection
        {
            public UserGroup(string name)
            {
                this.Name = name;
                this.Children = new ObservableCollection<User>();
            }

            public string Name { get; set; }
            public ObservableCollection<User> Children { get; set; }
        }

        public class User : NotifierCollection
        {
            public User(string name)
            {
                this.Name = name;
            }

            public string Name { get; set; }
        }
    }
}
