using System;
using System.Collections.ObjectModel;
using System.Text;
using WPF_MVC_UserManagement.Model;

namespace WPF_MVC_UserManagement.Controller
{
    public class LogListController
    {
        private ObservableCollection<LogListModel> logList = new ObservableCollection<LogListModel>();

        public delegate void DelegateLogList(ObservableCollection<LogListModel> logListData);
        public event DelegateLogList delegateLogList;

        public void CallLogList()
        {
            logList.Add(new LogListModel("[추가]", "유저 추가"));
            logList.Add(new LogListModel("[삭제]", "유저 삭제"));
            logList.Add(new LogListModel("[추가]", "유저 추가"));
            logList.Add(new LogListModel("[삭제]", "유저 삭제"));
            logList.Add(new LogListModel("[편집]", "유저 편집"));
            logList.Add(new LogListModel("[삭제]", "유저 삭제"));
            logList.Add(new LogListModel("[편집]", "유저 편집"));
            logList.Add(new LogListModel("[추가]", "유저 추가"));
            logList.Add(new LogListModel("[삭제]", "유저 삭제"));
            logList.Add(new LogListModel("[추가]", "유저 추가"));
            this.delegateLogList?.Invoke(logList);
        }

        public void CallAddLogList(string level, string content)
        {
            logList.Add(new LogListModel(level, content));
        }

        //private string ConcatDateTime(string data)
        //{
        //    this.stringBuilder.Clear();
        //    this.stringBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //    this.stringBuilder.Append(" ");
        //    this.stringBuilder.Append(data);
        //    this.stringBuilder.Append("\n");

        //    return this.stringBuilder.ToString();
        //}
    }
}
