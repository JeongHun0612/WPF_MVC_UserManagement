using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVC_UserManagement.Model
{
    public class UserManageTreeModel
    {
        public UserManageTreeModel(int depthCount, string primaryKey, string header)
        {
            this.depthCount = depthCount;
            this.primaryKey = primaryKey;
            this.header = header;
        }

        private int depthCount;
        public int DepthCount
        {
            get { return this.depthCount; }
            set { this.depthCount = value; }
        }

        private string primaryKey;
        public string PrimaryKey
        {
            get { return this.primaryKey; }
            set { this.primaryKey = value; }
        }

        private string header;
        public string Header
        {
            get { return this.header; }
            set { this.header = value; }
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
    }
}
