using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using DataBoundApp1.ViewModels;

namespace DataBoundApp1
{
    public partial class ChatSelect : PhoneApplicationPage
    {
        bool SidebarState = false;

        public ChatSelect()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
            //SystemTray.SetBackgroundColor(this, Color.FromArgb(255, 65, 102, 145));
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Border_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Border border = sender as Border;
            Thread UsersMessages = border.DataContext as Thread;
            this.NavigationService.Navigate(new Uri("/MainPage.xaml?parameter=" + UsersMessages.UserName, UriKind.Relative));
        }

        private void settingsPane_Click(object sender, EventArgs e)
        {
            if (SidebarState == false)
            {
                VisualStateManager.GoToState(this, "SettingsOpenState", true);
                SidebarState = true;
            }
            else
            {
                VisualStateManager.GoToState(this, "SettingsClosedState", true);
                SidebarState = false;
            }
            
        }



    }
}