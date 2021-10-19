using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVC_UserManagement.Model
{
    public class LogListModel : NotifierCollection
    {
        public LogListModel(string level, string content)
        {
            this.level = level;
            this.content = content;
        }

        private string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string Time
        {
            get { return this.time; }
            set { this.time = value; NotifyCollection("Time"); }
        }

        private string level = string.Empty;
        public string Level
        {
            get { return this.level; }
            set { this.level = value; NotifyCollection("Level"); }
        }

        private string content = string.Empty;
        public string Content
        {
            get { return this.content; }
            set { this.content = value; NotifyCollection("Content"); }
        }
    }
}
