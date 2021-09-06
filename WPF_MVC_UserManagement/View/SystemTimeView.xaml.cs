using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WPF_MVC_UserManagement.View
{
    /// <summary>
    /// SystemTimeView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SystemTimeView : UserControl
    {
        public SystemTimeView()
        {
            InitializeComponent();
            DateTimerInit();
        }

        public static DispatcherTimer Timer = new DispatcherTimer();

        private void DateTimerInit()
        {
            Timer.Start();
            Timer.Interval = TimeSpan.FromMilliseconds(1000);
            Timer.Tick += (o, e) =>
            {
                KSTDateTime.Content = DateTime.Now.ToString("yyyy. MM. dd. HH:mm:ss");
                UTCDateTime.Content = DateTime.UtcNow.ToString("yyyy. MM. dd. HH:mm:ss");
            };
        }
    }
}
