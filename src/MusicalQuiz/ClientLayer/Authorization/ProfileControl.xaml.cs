using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ServiceLayer.Providers;

namespace ClientLayer.Authorization
{
    /// <summary>
    /// Interaction logic for ProfileControl.xaml
    /// </summary>
    public partial class ProfileControl : UserControl
    {
        private readonly bool _ignoreRatingUpdating;
        public ProfileControl()
        {
            InitializeComponent();
            Username.Text = $"Ви увійшли як {UsersProvider.CurrentUser?.Username ?? string.Empty}";
            _ignoreRatingUpdating = true;
            if (UsersProvider.CurrentUser != null)
                RatingBar.Value = UsersProvider.CurrentUser.AppRating ?? 0;
            _ignoreRatingUpdating = false;
        }

        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            UsersProvider.Logout();
            CloseWindow.Execute(null);
            ShowSnackBar.Invoke("Деякі функції недоступні для незалогованих користувачів.", "Ок");
        }

        private async void RatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            if (!_ignoreRatingUpdating)
            {
                await RatingProvider.RateProgram(e.NewValue);
                ShowSnackBar.Invoke("Ваш відгук дуже важливий для нас. Дякуємо!", "Ок");
            }
        }

        #region CloseWindow

        public ICommand CloseWindow
        {
            get => (ICommand)GetValue(CloseWindowProperty);
            set => SetValue(CloseWindowProperty, value);
        }

        public static readonly DependencyProperty CloseWindowProperty = DependencyProperty.Register(
            nameof(CloseWindow),
            typeof(ICommand),
            typeof(ProfileControl),
            new UIPropertyMetadata());

        #endregion

        #region ShowSnackBar

        public Action<string, string> ShowSnackBar
        {
            get => (Action<string, string>)GetValue(ShowSnackBarProperty);
            set => SetValue(ShowSnackBarProperty, value);
        }

        public static readonly DependencyProperty ShowSnackBarProperty = DependencyProperty.Register(
            nameof(ShowSnackBar),
            typeof(Action<string, string>),
            typeof(ProfileControl),
            new UIPropertyMetadata());

        #endregion

    }
}
