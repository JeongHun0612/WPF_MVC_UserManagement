using System.Windows;
using WPF_MVC_UserManagement.Controller;

namespace WPF_MVC_UserManagement
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DBManager.Initialize();
            InitializeComponent();
        }

        public static MySQLManager DBManager = new MySQLManager();
        public static UserManageTreeController userManageTreeController = new UserManageTreeController();
        public static UserManageListController userManageListController = new UserManageListController();
        public static LogListController logListController = new LogListController();
    }
}
