using System;
using System.Windows;
using ClientLayer.Authorization;

namespace ClientLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShutDown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            var authorizationWindow = new AuthorizationWindow {ShowSnackBar = SnackBarAction};
            authorizationWindow.ShowDialog();
        }

        #region SnackBar

        private void ShowSnackBar(string message, string closeMessage = "Ок")
        {
            SnackBar.MessageQueue?.Enqueue(
                message,
                closeMessage,
                _ => SnackBar.IsActive = false,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
        }

        public Action<string, string> SnackBarAction => ShowSnackBar;

        #endregion
    }
}
