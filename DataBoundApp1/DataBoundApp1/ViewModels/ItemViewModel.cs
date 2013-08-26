using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace DataBoundApp1.ViewModels
{
    public class Thread : INotifyPropertyChanged
    {
        private string _username;
        private string _location;
        private ObservableCollection<Message> threadMessages;
        private string _lastmessage;
        private BitmapImage _image;
        private DateTime _timestamp;

        public ObservableCollection<Message> ThreadMessages
        {
            get { return threadMessages; }
            set
            {
                threadMessages = value;
                OnPropertyChanged("ThreadMessages");
            }
        }
        private DateTime TimeStamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
                OnPropertyChanged("TimeStamp");
            }
        }
        public String LastMessage
        {
            get { return _lastmessage; }
            set
            {
                _lastmessage = value;
                OnPropertyChanged("LastMessage");
            }
        }

        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }


        public string UserName
        {
            get { return _username; }
            set
            {
                if (value == null) { return; }
                _username = value;
                OnPropertyChanged("UserName");
            }
        }

        public string Location
        {
            get { return _location; }
            set
            {
                if (value == null) { return; }
                _location = value;
                OnPropertyChanged("Location");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}