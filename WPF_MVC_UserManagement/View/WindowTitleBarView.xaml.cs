using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_MVC_UserManagement.View
{
    /// <summary>
    /// WindowTitleBarView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowTitleBarView : UserControl
    {
        public WindowTitleBarView()
        {
            InitializeComponent();
        }

        private Point startPos;
        private readonly MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        private void TitleBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            startPos = Mouse.GetPosition(mainWindow);
        }

        private void TitleBarDragMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && (startPos.X != 0 || startPos.Y != 0))
            {
                Point position = Mouse.GetPosition(mainWindow);

                if (mainWindow.WindowState == WindowState.Maximized && (Math.Abs(startPos.Y - position.Y) > 2 || Math.Abs(startPos.X - position.X) > 2))
                {
                    Point point = mainWindow.PointToScreen(Mouse.GetPosition(null));

                    mainWindow.WindowState = WindowState.Normal;
                    minRectAngle.Visibility = Visibility.Visible;
                    maxRectAngle.Visibility = Visibility.Collapsed;

                    mainWindow.Left = point.X - mainWindow.ActualWidth / 2;
                    mainWindow.Top = 0;
                }

                mainWindow.DragMove();
            }
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            mainWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            if (mainWindow.WindowState == WindowState.Normal)
            {
                mainWindow.WindowState = WindowState.Maximized;
                minRectAngle.Visibility = Visibility.Collapsed;
                maxRectAngle.Visibility = Visibility.Visible;
            }
            else
            {
                mainWindow.WindowState = WindowState.Normal;
                minRectAngle.Visibility = Visibility.Visible;
                maxRectAngle.Visibility = Visibility.Collapsed;
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            MainWindow.DBManager.CloseMySqlConnection();
            SystemTimeView.Timer.Stop();
            Environment.Exit(0);
        }
    }
}
