using System;
using System.Text;

namespace WPF_MVC_UserManagement.Controller
{
    public class LogListController
    {
        private StringBuilder stringBuilder = new StringBuilder();

        public delegate void DelegateLogList(string logListData);
        public event DelegateLogList delegateLogList;

        public void CallLogList(string data)
        {
            this.delegateLogList?.Invoke(ConcatDateTime(data));
        }

        private string ConcatDateTime(string data)
        {
            this.stringBuilder.Clear();
            this.stringBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            this.stringBuilder.Append(" ");
            this.stringBuilder.Append(data);
            this.stringBuilder.Append("\n");

            return this.stringBuilder.ToString();
        }
    }
}
