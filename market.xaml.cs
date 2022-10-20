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
	public partial class market : ContentPage
	{
       
        public int currentID;

        public borrowBook bb = new borrowBook();
      
      
        public market ()
		{
           
			InitializeComponent ();
            this.BindingContext = new BookDetail();
         
		}
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
                              
            this.marketBookList.ItemsSource = App.databaseBook.getMarketBooks(currentID);
        }

        private void searchClicked()
        {
            if (byName.Text != null)
            {
                this.marketBookList.ItemsSource = App.databaseBook.SearchBookByName(byName.Text,currentID); //get the matched book list using byName
            }
            else{
                this.marketBookList.ItemsSource = App.databaseBook.getMarketBooks(currentID);//get back the all book in market
            }
        }

        private async void bookSelected(object sender , SelectedItemChangedEventArgs e) //select the book for borrowing
        {
            if (e.SelectedItem == null) return;
            BookDetail selectedBook = e.SelectedItem as BookDetail;
            selectedBook.booksViewed = selectedBook.booksViewed + 1;
           
            App.databaseBook.saveBook(selectedBook);
            bb.SetData(selectedBook);             
            await Navigation.PushModalAsync(bb, true);
            ((ListView)sender).SelectedItem = null; // de-select the row

        }
           private void refreshClicked()
        {
            this.marketBookList.ItemsSource = App.databaseBook.getMarketBooks(currentID);
        }
    }

}