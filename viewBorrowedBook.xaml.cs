using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAssigenment2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class viewBorrowedBook : ContentPage
	{
        public BookDetail selectedBook;
        
        public viewBorrowedBook ()
		{            
			InitializeComponent ();
		}
        public viewBorrowedBook(BookDetail book)
        {            
            selectedBook = book;
            InitializeComponent();

        }
        public void setData(BookDetail book)  //get the selected book details
        {
             selectedBook = book;
             images.Source = selectedBook.booksCover;
             bName.Text = selectedBook.booksName;
             bType.Text = selectedBook.booksType;
             bookDescription.Text = selectedBook.description;
             viewTime.Text = selectedBook.booksViewed.ToString();
             borrow.Text = selectedBook.booksLended.ToString();
             bOwner.Text =App.Database.getCurrentUserName(selectedBook.ownersID);
             startD.Text = selectedBook.getBrorrowedDate;
             endD.Text = selectedBook.getReturnedDate;
        }

        private async void returnClicked() //return a book to market
        {

            selectedBook.borrowersID =null;
            selectedBook.getBrorrowedDate = null;
            selectedBook.getReturnedDate = null;
            App.databaseBook.saveBook(selectedBook);
            await DisplayAlert("", "book have been returned", "ok");
            await Navigation.PopModalAsync();
        }
        private async void cancelClicked()
        {
            await Navigation.PopModalAsync();
        }
	}
}