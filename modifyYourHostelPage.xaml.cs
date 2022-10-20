using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAssigenment2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class modifyYourBookPage : ContentPage
    {
      
        public BookDetail thisBook;
        public int currentID;

        public modifyYourBookPage()
        {
            InitializeComponent();
        }
        public modifyYourBookPage(BookDetail book)
        {
            thisBook = book;
            InitializeComponent();
        }
        public void setData(BookDetail book) //fecth the data for the seletced book
        {
            thisBook = book;
            loadBookCover.Source = thisBook.booksCover;
            name.Text = thisBook.booksName;
            type.Text = thisBook.booksType;
            getDescription.Text = thisBook.description;
            view.Text = thisBook.booksViewed.ToString();
            borrow.Text = thisBook.booksLended.ToString();

        }

        private void modifyClicked()
        {
            newBookName.IsVisible = true;
            newBookType.IsVisible = true;
            modifyButton.IsVisible = false;
            saveChange.IsVisible = true;          
            modifyDescription.IsVisible = true;
            backButton.IsVisible = true;
        }
        private async void saveClicked(object sender, EventArgs e)  //modiy your not being borrowed book
        {
          
            if (newBookName.Text != null && newBookType.Text != null && modifyDescription.Text !=null ) //conditon all fields need to be filled
            {
                string FileName = thisBook.getDescriptionFile;
                IFolder folder = FileSystem.Current.LocalStorage;
                IFile file = await folder.GetFileAsync(FileName);
                string content = modifyDescription.Text; //description usingg editor
                await file.WriteAllTextAsync(content);
                thisBook.booksName = newBookName.Text;
                thisBook.booksType = newBookType.Text;
                thisBook.description = modifyDescription.Text; 
                App.databaseBook.saveBook(thisBook);
                await Navigation.PopModalAsync();
                saveChange.IsVisible = false;
                modifyButton.IsVisible = true;
                newBookName.IsVisible = false;
                newBookType.IsVisible = false;
                modifyDescription.IsVisible = false;            
                newBookName.Text = "";
                newBookType.Text = "";
                modifyDescription.Text = "";
                backButton.IsVisible = true;
            }
                             
            else
            {
                await DisplayAlert("", "fill all fields", "ok");
            }
        }
       

        private async void cancelClicked()
        {

            newBookName.IsVisible = false;
            newBookType.IsVisible = false;
            saveChange.IsVisible = false;
            modifyDescription.IsVisible = false;
            modifyButton.IsVisible = true;
            backButton.IsVisible = true;
            await Navigation.PopModalAsync();
        }
        private async void deleteClicked() //detele book
        {
            App.databaseBook.deleteBook(thisBook);
            await DisplayAlert("", "book deleted", "ok");
            await Navigation.PopModalAsync();
        }
    }
}