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
	public partial class lended : ContentPage
	{
        public postBookForm pForm = new postBookForm();
        public modifyYourBookPage modify=new modifyYourBookPage();
        public viewLendedBook booksBeBorrowed = new viewLendedBook();
        public int currentID;
        
        public lended()
        {
            InitializeComponent();
            this.BindingContext = new BookDetail();           
        }      
        private async void postAbook(object sender,EventArgs e)
        {                     
            await Navigation.PushModalAsync(pForm, true);
        }      
        protected  override void OnAppearing()
        {         
            this.lendedBook.ItemsSource = App.databaseBook.booksNotBorrowed(currentID);           
            this.booksLended.ItemsSource = App.databaseBook.booksBorrowed(currentID);
        }
      
        private async void lendeBookSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            BookDetail selectedBook = e.SelectedItem as BookDetail;                       
            modify.setData(selectedBook);
            await Navigation.PushModalAsync(modify, true);
            ((ListView)sender).SelectedItem = null; // de-select the row

        }

        private async void booksLendedSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            BookDetail selectedBook = e.SelectedItem as BookDetail;
            booksBeBorrowed.setData(selectedBook);               
            await Navigation.PushModalAsync(booksBeBorrowed, true);
            ((ListView)sender).SelectedItem = null; // de-select the row
        }

    }
}