using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DataBoundApp1.Resources;
using DataBoundApp1.ViewModels;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;

namespace DataBoundApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        ObservableCollection<Message> messages;

        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;
            Color currentAccentColorHex =
            (Color)Application.Current.Resources["PhoneAccentColor"];

            SystemTray.SetBackgroundColor(this, Color.FromArgb(255, 202, 98, 89));
            SystemTray.SetForegroundColor(this, Colors.White);
            

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string parameter = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("parameter", out parameter))
            {
                messages = new ObservableCollection<Message>();

                foreach(Thread UserThreads in App.ViewModel.UserThreads)
                {
                    if (UserThreads.UserName == parameter)
                    {
                        if (UserThreads.ThreadMessages == null)
                        {
                            UserThreads.ThreadMessages = new ObservableCollection<Message>();
                        }

                        messages = UserThreads.ThreadMessages;
                        MessageBubble.ItemsSource = messages;
                        return;
                    }
                }

            }
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            //if (MainLongListSelector.SelectedItem == null)
            //    return;

            // Navigate to the new page
            //NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

            // Reset selected item to null (no selection)
            //MainLongListSelector.SelectedItem = null;
        }

        private void Pivot_Loaded_1(object sender, RoutedEventArgs e)
        {

        }


        private void Grid_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Grid myGrid = sender as Grid;
            Ellipse favEl = myGrid.FindName("elipse") as Ellipse;
            favEl.Fill = new SolidColorBrush((Color)Application.Current.Resources["PhoneAccentColor"]);
        }

        /// <summary>
        /// VerticalOffset, a private DP used to animate the scrollviewer
        /// </summary>
        private DependencyProperty VerticalOffsetProperty = DependencyProperty.Register("VerticalOffset",
          typeof(double), typeof(MainPage), new PropertyMetadata(0.0, OnVerticalOffsetChanged));

        private static void OnVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainPage app = d as MainPage;
            app.OnVerticalOffsetChanged(e);
        }

        private void OnVerticalOffsetChanged(DependencyPropertyChangedEventArgs e)
        {
            MainScroller.ScrollToVerticalOffset((double)e.NewValue);
        }

        private void ComposeMessage_LostFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
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

        var panel = (Grid) sender;
        panel.RenderTransform = translatTransform;

        Storyboard.SetTarget(animation, translatTransform);
        Storyboard.SetTargetProperty(animation, new PropertyPath("Y"));
        storyboard.Children.Add(animation);
        storyboard.Begin(); 
        }

        private void ComposeMessage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
        	if (e.Key == Key.Enter)
            {              
                messages.Add(new Message { Content = ComposeMessage.Text });
                ComposeMessage.Text = String.Empty;
                MainScroller.UpdateLayout();
                MainScroller.ScrollToVerticalOffset(MainScroller.ScrollableHeight);
                
            }
        }

        private void SendMessage_Click(object sender, EventArgs e)
        {
            messages.Add(new Message { Content = ComposeMessage.Text });
            ComposeMessage.Text = String.Empty;
            MainScroller.UpdateLayout();
            MainScroller.ScrollToVerticalOffset(MainScroller.ScrollableHeight);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}