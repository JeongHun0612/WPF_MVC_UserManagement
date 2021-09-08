using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVC_UserManagement.Model
{
    public class ComboBoxGroupListModel : NotifierCollection
    {
        public ComboBoxGroupListModel(string groupName, bool isHeader, string groupId)
        {
            this.groupName = groupName;
            this.isHeader = isHeader;
            this.groupId = groupId;
        }

        public ComboBoxGroupListModel(string groupName, bool isHeader, string groupId, string parentGroupId)
        {
            this.groupName = groupName;
            this.isHeader = isHeader;
            this.groupId = groupId;
            this.parentGroupId = parentGroupId;
        }

        private string groupName = string.Empty;
        public string GroupName
        {
            get { return this.groupName; }
            set { this.groupName = value; NotifyCollection("GroupName"); }
        }

        private bool isHeader = false;
        public bool IsHeader
        {
            get { return this.isHeader; }
            set { this.isHeader = value; NotifyCollection("IsHeader"); }
        }

        private string groupId = string.Empty;
        public string GroupId
        {
            get { return this.groupId; }
            set { this.groupId = value; NotifyCollection("GroupId"); }
        }

        private string parentGroupId = string.Empty;
        public string ParentGroupId
        {
            get { return this.parentGroupId; }
            set { this.parentGroupId = value; NotifyCollection("ParentGroupId"); }
        }
    }
}
