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
	public partial class brorrowed : ContentPage
	{
        public int currentUserID;
        public viewBorrowedBook selectedBorrowedBook=new viewBorrowedBook();
		public brorrowed ()
		{
			InitializeComponent ();
            this.BindingContext = new BookDetail();
		}
            
        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.borrowedBookList.ItemsSource = App.databaseBook.getYourBorrowedBooks(currentUserID);
        }


        private async void bookSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem == null) return;

            BookDetail selectedBook = e.SelectedItem as BookDetail;        
            selectedBorrowedBook.setData(selectedBook);
            await Navigation.PushModalAsync(selectedBorrowedBook, true);

            ((ListView)sender).SelectedItem = null; // de-select the row
        }
    }
}