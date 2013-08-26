using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DataBoundApp1.Resources;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Phone.UserData;

namespace DataBoundApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.UserThreads = new ObservableCollection<Thread>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<Thread> UserThreads { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            this.UserThreads.Add (new Thread {LastMessage = "Did you get my message?", UserName = "Nicholos Tyler" });
            this.UserThreads.Add(new Thread { LastMessage = "Fart Face", UserName = "Timothy Tyler" });
            this.UserThreads.Add(new Thread { LastMessage = "Wtf dude", UserName = "Sara Ostrowski" });
            this.UserThreads.Add(new Thread { LastMessage = "Make coffee fag", UserName = "Meghann Tyler" });
            this.UserThreads.Add(new Thread { LastMessage = "Blah", UserName = "Brittney Tyler" });
            IsDataLoaded = true;
        }

        void user_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}