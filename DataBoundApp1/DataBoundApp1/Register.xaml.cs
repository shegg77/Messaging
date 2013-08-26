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

namespace DataBoundApp1
{
    public partial class Register : PhoneApplicationPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private void stackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            this.Storyboard1.Begin();
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordHint.Visibility = System.Windows.Visibility.Collapsed;

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

        private void txtbLogin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/SignIn.xaml", UriKind.Relative));

        }
    }
}