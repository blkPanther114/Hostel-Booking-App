using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Plugin.Media;
using Plugin.Media.Abstractions;
namespace MobileAssigenment2
{
    public class User : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        private string uName;
        private string pWord;
        private string Email;
        private string mediaFile;
        private string LoginStatus;
        public string username
        {
            get
            {
                return uName;
            }

            set
            {
                if (uName != value)
                {
                    uName = value;
                    OnPropertyChanged("username");
                }
            }
        }
        public string password
        {

            get
            {

                return pWord;

            }
            set
            {
                if (pWord != value)
                {
                    pWord = value;
                    OnPropertyChanged("password");
                }
            }
        }

        public string email
        {
            get
            {
                return Email;
            }

            set
            {
                if (Email != value)
                {
                    Email = value;
                    OnPropertyChanged("email");
                }
            }
        }

        public string media
        {
            get
            {
                return mediaFile;
            }

            set
            {
                if (mediaFile != value)
                {
                    mediaFile = value;
                    OnPropertyChanged("media");
                }
            }
        }

        public string status
        {
            get
            {
                return LoginStatus;
            }

            set
            {
                if (LoginStatus != value)
                {
                    LoginStatus = value;
                    OnPropertyChanged("status");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}