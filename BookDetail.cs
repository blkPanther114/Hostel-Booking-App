using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MobileAssigenment2
{
    public class BookDetail: INotifyPropertyChanged
    {

        [PrimaryKey, AutoIncrement]

        public int ID { get; set; }
        private int ownerID;
        private string bookName;
        private string bookType;
        public string borrowerID ;
        private string bookDescription;
        private string descriptionFile;
        private int bookViewd=0;
        private int bookLended=0;
        private string brorrowDate { get; set; }
        private string returnDate { get; set; }
        private string bookCover;
       public int ownersID
        {
            get
            {
                return ownerID;
            }

            set
            {
                if (ownerID != value)
                {
                    ownerID = value;
                    OnPropertyChanged("ownersName");
                }
            }
        }

        public string borrowersID
        {
            get
            {
                return borrowerID;
            }

            set
            {
                if (borrowerID != value)
                {
                    borrowerID = value;
                    OnPropertyChanged("borrowersID");
                }
            }
        }

        public string booksName
        {
            get
            {
                return bookName;
            }
            set
            {
                if(bookName !=value)
                {
                    bookName = value;
                    OnPropertyChanged("booksName");
                }
            }
        }

        public string booksType
        {
            get
            {
                return bookType;
            }
            set
            {
                if (bookType != value)
                {
                    bookType = value;
                    OnPropertyChanged("booksType");
                }
            }
        }

        public string getBrorrowedDate
        {

            get
            {
                return brorrowDate;
            }
            set
            {
                if(brorrowDate != value)
                {
                    brorrowDate = value;
                    OnPropertyChanged("ownersID");

                }
            }
        }

        public string getReturnedDate
        {

            get
            {
                return returnDate;
            }
            set
            {
                if (returnDate != value)
                {
                    returnDate = value;
                    OnPropertyChanged("getReturnedDate");

                }
            }
        }

        public string description
        {

            get
            {
                return bookDescription;
            }
            set
            {
                if (bookDescription != value)
                {
                    bookDescription = value;
                    OnPropertyChanged("description");

                }
            }
        }

        public string getDescriptionFile
        {

            get
            {
                return descriptionFile;
            }
            set
            {
                if (descriptionFile != value)
                {
                    descriptionFile = value;
                    OnPropertyChanged("getDescriptionFile");

                }
            }
        }

        public int booksViewed
        {
            get
            {
                return bookViewd;
            }
            set
            {   if (bookViewd != value)
                {
                    bookViewd = value;
                    OnPropertyChanged("booksViewed");
                }
            }
        }

        public int booksLended
        {
            get
            {
                return bookLended ;
            }
            set
            {
                if (bookLended != value)
                {
                    bookLended = value;
                    OnPropertyChanged("booksLended");
                }
            }

        }

        public string booksCover
        {
            get
            {
                return bookCover;
            }

            set
            {
                if (bookCover != value)
                {
                    bookCover = value;
                    OnPropertyChanged("booksCover");
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

