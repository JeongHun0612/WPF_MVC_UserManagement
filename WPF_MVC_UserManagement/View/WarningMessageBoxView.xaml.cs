using System.Windows;

namespace WPF_MVC_UserManagement.View
{
    /// <summary>
    /// WarningMessageBoxView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WarningMessageBoxView : Window
    {
        private bool isResult = false;

        public WarningMessageBoxView()
        {
            InitializeComponent();
        }

        public bool ShowMessageBox(string messageBoxText, int messageBoxMode)
        {
            messageBoxLabel.Content = messageBoxText;

            switch (messageBoxMode)
            {
                case 0:
                    closeButton.Focus();
                    warningMode.Visibility = Visibility.Visible;
                    messageBoxWarningIcon.Visibility = Visibility.Visible;
                    messageBoxDangerIcon.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    dangerMode.Visibility = Visibility.Visible;
                    messageBoxDangerIcon.Visibility = Visibility.Visible;
                    messageBoxWarningIcon.Visibility = Visibility.Collapsed;
                    break;
            }

            this.ShowDialog();

            return isResult;
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            isResult = true;
            this.Close();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
