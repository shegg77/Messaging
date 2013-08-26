using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Data;
using System.Globalization;
using Buddy;

namespace DataBoundApp1
{
    public partial class SignIn : PhoneApplicationPage
    {
        ProgressIndicator prog;

        public SignIn()
        {
            InitializeComponent();
            prog = new ProgressIndicator();
            SystemTray.SetProgressIndicator(this, prog);
        }

        private void stackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var storyboard = new Storyboard();
            var animation = new DoubleAnimation
            {
                From = 900,
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(500))
            };

            var translatTransform = new TranslateTransform
            {
                Y = 900
            };

            var panel = (StackPanel)sender;
            panel.RenderTransform = translatTransform;

            Storyboard.SetTarget(animation, translatTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Y"));
            storyboard.Children.Add(animation);
            storyboard.Begin();
            
            while (storyboard.GetCurrentState() == ClockState.Active)
            {
            }
            
            this.Storyboard2.Begin();
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password.Length > 0)
            {
                passwordHint.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
                passwordHint.Visibility = System.Windows.Visibility.Visible;
        }

        private void passwordHint_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordHint.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordHint.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void txtbRegister_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Register.xaml", UriKind.Relative));
        }

        public async void SignUserIn(string userName = null, string password = null, string token = null)
        {
            try
            {
                if (userName != null)
                {
                    prog.Text = "Logging you in...";
                    App.User = await ServiceManager.Client.LoginAsync(userName, password);
                    //ServiceManager.EncryptPasscode(password, App.User.Token);
                }
                else
                {
                    App.User = await ServiceManager.Client.LoginAsync(token);
                    //ServiceManager.EncryptPasscode(null, App.User.Token);
                }
            }
            catch (BuddyServiceException ex)
            {
                if (ex.Error == "SecurityFailedBadUserName" || ex.Error == "SecurityFailedBadUserNameOrPassword")
                {
                    MessageBox.Show("Unknown username or password.");
                    //txtEmail.BorderBrush = new SolidColorBrush(Color.FromArgb(225, 220, 84, 82));
                    //txtEmail.Focus();
                    //txtEmail.SelectAll();
                }
                else if (ex.Error == "UserNameAlreadyInUse")
                {
                    MessageBox.Show("Username already in use");
                    //txtEmail.BorderBrush = new SolidColorBrush(Color.FromArgb(225, 220, 84, 82));
                    //txtEmail.Focus();
                    //txtEmail.SelectAll();
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Error);
                    //txtEmail.BorderBrush = new SolidColorBrush(Color.FromArgb(225, 220, 84, 82));
                    //txtEmail.Focus();
                    //txtEmail.SelectAll();
                }
            }
            finally
            {
                //ServiceManager.EncryptPasscode(password, App.User.Token);
                NavigationService.Navigate(new Uri("/ChatSelect.xaml", UriKind.Relative));
            }


        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Terminate();
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            SignUserIn(txtUserName.Text, txtPassword.Password);
        }


    }
}