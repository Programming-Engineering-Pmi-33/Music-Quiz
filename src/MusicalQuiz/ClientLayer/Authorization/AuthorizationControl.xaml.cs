using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ServiceLayer.Exceptions.UsersProvider;
using ServiceLayer.Providers;

namespace ClientLayer.Authorization
{
    /// <summary>
    /// Interaction logic for AuthorizationControl.xaml
    /// </summary>
    public partial class AuthorizationControl : UserControl
    {
        public AuthorizationControl()
        {
            InitializeComponent();
        }

        private async void Login_OnClick(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Contains(' ') || LoginBox.Text.Length < 4 || PasswordBox.Password.Length <= 3)
            {
                LoginBox.Focus();
                PasswordBox.Focus();
                LoginButton.Focus();
                return;
            }
            try
            {
                await UsersProvider.Login(LoginBox.Text, PasswordBox.Password);
                CloseWindow.Execute(null);
                ShowSnackBar.Invoke($"Ви увійшли як {LoginBox.Text}", "Ок");
            }
            catch (UsersProviderException exception)
            {
                switch (exception)
                {
                    case UsersNotFoundException _:
                        LoginBox.BorderBrush = new SolidColorBrush(Colors.Red);
                        LoginValidationError.Text = "Користувач не існує.";
                        LoginValidationError.Visibility = Visibility.Visible;
                        break;
                    case WrongUsersDataException _:
                        PasswordBox.BorderBrush = new SolidColorBrush(Colors.Red);
                        PasswordValidationError.Text = "Хибний пароль.";
                        PasswordValidationError.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private async void Register_OnClick(object sender, RoutedEventArgs e)
        {
            if (RLoginBox.Text.Contains(' ') || RLoginBox.Text.Length < 4 || RPasswordBox.Password.Length <= 3 || RepeatPasswordBox.Password != RPasswordBox.Password)
            {
                RLoginBox.Focus();
                RPasswordBox.Focus();
                RepeatPasswordBox.Focus();
                RegisterButton.Focus();
                return;
            }

            try
            {
                await UsersProvider.Register(RLoginBox.Text, RPasswordBox.Password);
                CloseWindow.Execute(null);
                ShowSnackBar.Invoke($"Ви увійшли як {RLoginBox.Text}", "Ок");
            }
            catch (UsersProviderException exception)
            {
                if (exception is WrongUsersDataException)
                {
                    RLoginBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    RLoginValidationError.Text = "Користувач з таким іменем вже існує.";
                    RLoginValidationError.Visibility = Visibility.Visible;
                }
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
            typeof(AuthorizationControl),
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
            typeof(AuthorizationControl),
            new UIPropertyMetadata());

        #endregion
    }

    #region LoginValidator

    public class LoginValidator : FrameworkElement
    {
        public static readonly DependencyProperty BoxProperty = DependencyProperty.Register("Box", typeof(TextBox), typeof(LoginValidator), new PropertyMetadata(BoxChanged));

        public static readonly DependencyProperty BlockProperty = DependencyProperty.Register("Block", typeof(TextBlock), typeof(LoginValidator), new PropertyMetadata(BoxChanged));

        public TextBox Box
        {
            get => (TextBox)GetValue(BoxProperty);
            set => SetValue(BoxProperty, value);
        }

        public TextBlock Block
        {
            get => (TextBlock)GetValue(BlockProperty);
            set => SetValue(BlockProperty, value);
        }


        private static void BoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var lb = (LoginValidator)d;
            var brush = lb.Box.BorderBrush;
            lb.Box.LostFocus += async (obj, evt) =>
            {
                if (lb.Box.Text.Equals(string.Empty))
                {
                    lb.Box.BorderBrush = new SolidColorBrush(Colors.Red);
                    lb.Block.Text = "Поле не може бути порожнім.";
                    lb.Block.Visibility = Visibility.Visible;
                }
                else if (lb.Box.Text.Contains(' '))
                {
                    lb.Box.BorderBrush = new SolidColorBrush(Colors.Red);
                    lb.Block.Text = "Логін не може містити пробіли.";
                    lb.Block.Visibility = Visibility.Visible;
                }
                else if (lb.Box.Text.Length < 4)
                {
                    lb.Box.BorderBrush = new SolidColorBrush(Colors.Red);
                    lb.Block.Text = "Логін повинен містити мінімум 4 символи.";
                    lb.Block.Visibility = Visibility.Visible;
                }
                else if (lb.Box.Name == "RLoginBox" && await UsersProvider.DoesUserExists(lb.Box.Text))
                {
                    lb.Box.BorderBrush = new SolidColorBrush(Colors.Red);
                    lb.Block.Text = "Користувач з таким іменем вже існує.";
                    lb.Block.Visibility = Visibility.Visible;
                }
                else
                {
                    lb.Box.BorderBrush = brush;
                    lb.Block.Visibility = Visibility.Hidden;
                }
            };
        }
    }

    #endregion

    #region PasswordValidator

    public class PasswordValidator : FrameworkElement
    {
        public static readonly DependencyProperty BoxProperty = DependencyProperty.Register("Box", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box1Changed));

        public static readonly DependencyProperty BlockProperty = DependencyProperty.Register("Block", typeof(TextBlock), typeof(PasswordValidator), new PropertyMetadata(Box1Changed));

        public PasswordBox Box
        {
            get => (PasswordBox)GetValue(BoxProperty);
            set => SetValue(BoxProperty, value);
        }

        public TextBlock Block
        {
            get => (TextBlock)GetValue(BlockProperty);
            set => SetValue(BlockProperty, value);
        }


        private static void Box1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pv = (PasswordValidator)d;
            var brush = pv.Box.BorderBrush;
            pv.Box.LostFocus += (obj, evt) =>
            {
                if (pv.Box.Password.Length == 0)
                {
                    pv.Box.BorderBrush = new SolidColorBrush(Colors.Red);
                    pv.Block.Text = "Поле не може бути порожнім.";
                    pv.Block.Visibility = Visibility.Visible;
                }
                else if (pv.Box.Password.Length < 4)
                {
                    pv.Box.BorderBrush = new SolidColorBrush(Colors.Red);
                    pv.Block.Text = "Пароль повинен містити мінімум 4 символи.";
                    pv.Block.Visibility = Visibility.Visible;
                }
                else
                {
                    pv.Box.BorderBrush = brush;
                    pv.Block.Visibility = Visibility.Hidden;
                }
            };
        }
    }

    #endregion

    #region RepeatPasswordValidator

    public class RepeatPasswordValidator : FrameworkElement
    {
        private static readonly IDictionary<PasswordBox, Brush> PasswordBoxes = new Dictionary<PasswordBox, Brush>();

        public static readonly DependencyProperty Box1Property = DependencyProperty.Register("Box1", typeof(PasswordBox), typeof(RepeatPasswordValidator), new PropertyMetadata(Box1Changed));
        public static readonly DependencyProperty Box2Property = DependencyProperty.Register("Box2", typeof(PasswordBox), typeof(RepeatPasswordValidator), new PropertyMetadata(Box2Changed));

        public static readonly DependencyProperty BlockProperty = DependencyProperty.Register("Block", typeof(TextBlock), typeof(RepeatPasswordValidator), null);

        public PasswordBox Box1
        {
            get => (PasswordBox)GetValue(Box1Property);
            set => SetValue(Box1Property, value);
        }
        public PasswordBox Box2
        {
            get => (PasswordBox)GetValue(Box2Property);
            set => SetValue(Box2Property, value);
        }
        public TextBlock Block
        {
            get => (TextBlock)GetValue(BlockProperty);
            set => SetValue(BlockProperty, value);
        }

        private static void Box1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pv = (RepeatPasswordValidator)d;
            PasswordBoxes[pv.Box1] = pv.Box1.BorderBrush;
            pv.Box1.LostFocus += (obj, evt) =>
            {
                if (pv.Box1.Password != pv.Box2.Password && pv.Box2.Password.Length != 0)
                {
                    pv.Box2.BorderBrush = new SolidColorBrush(Colors.Red);
                    pv.Block.Text = "Паролі не співпадають.";
                    pv.Block.Visibility = Visibility.Visible;
                }
                else
                {
                    pv.Box2.BorderBrush = PasswordBoxes[pv.Box2];
                    pv.Block.Visibility = Visibility.Hidden;
                }
            };

        }

        private static void Box2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pv = (RepeatPasswordValidator)d;
            PasswordBoxes[pv.Box2] = pv.Box2.BorderBrush;
            pv.Box2.LostFocus += (obj, evt) =>
            {
                if (pv.Box1.Password != pv.Box2.Password && pv.Box1.Password.Length != 0)
                {
                    pv.Box2.BorderBrush = new SolidColorBrush(Colors.Red);
                    pv.Block.Text = "Паролі не співпадають.";
                    pv.Block.Visibility = Visibility.Visible;
                }
                else
                {
                    pv.Box2.BorderBrush = PasswordBoxes[pv.Box2];
                    pv.Block.Visibility = Visibility.Hidden;
                }
            };
        }
    }

    #endregion
}
