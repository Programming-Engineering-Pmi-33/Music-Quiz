using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using ServiceLayer.Providers;

namespace ClientLayer.Authorization
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            if (UsersProvider.IsLoggedIn)
            {
                ProfileControl.Visibility = Visibility.Visible;
                AuthorizationControl.Visibility = Visibility.Collapsed;
            }
            if (!UsersProvider.IsLoggedIn)
            {
                ProfileControl.Visibility = Visibility.Collapsed;
                AuthorizationControl.Visibility = Visibility.Visible;
            }
        }

        #region ShowShackBar
        public Action<string, string> ShowSnackBar
        {
            get => (Action<string, string>)GetValue(ShowSnackBarProperty);
            set => SetValue(ShowSnackBarProperty, value);
        }

        public static readonly DependencyProperty ShowSnackBarProperty = DependencyProperty.Register(
            nameof(ShowSnackBar),
            typeof(Action<string, string>),
            typeof(AuthorizationWindow),
            new UIPropertyMetadata());

        public ICommand CloseWindow => new ActionCommand(Close);

        #endregion
    }
}
