using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_MVC_UserManagement.Model;

namespace WPF_MVC_UserManagement.View
{
    /// <summary>
    /// LogListView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LogListView : UserControl
    {
        public LogListView()
        {
            InitializeComponent();
            ControllerInit();
            LogListInit();
        }

        private void ControllerInit()
        {
            MainWindow.logListController.delegateLogList += SendLogList;
        }

        private void LogListInit()
        {
            MainWindow.logListController.CallLogList();
        }

        private void SendLogList(ObservableCollection<LogListModel> logListData)
        {
            DataGridLog.ItemsSource = logListData;
        }

        private void DataGridLog_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.OriginalSource is ScrollViewer scrollViewer && e.ExtentHeightChange > 0.0)
            {
                scrollViewer.ScrollToEnd();
            }
        }
    }
}
