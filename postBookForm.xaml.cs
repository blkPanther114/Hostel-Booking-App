using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PCLStorage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAssigenment2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class postBookForm : ContentPage
    {
        private MediaFile mediaFile;
        public market m = new market();
        public int currentID;
        generateRandomString generate = new generateRandomString();
        
        public postBookForm()
        {
            InitializeComponent();
        }
        private async void postClicked(object sender, EventArgs e)
        {
            string FileName = generate.generateFileKey();
            IFolder folder = FileSystem.Current.LocalStorage;
           
            // post a new book 
            if (bookName.Text != null && bookType.Text != null  && mediaFile !=null)//conditons all fields need to be filled
            {               
                    IFile file = await folder.CreateFileAsync(FileName, CreationCollisionOption.FailIfExists);
                    string content = description.Text;
                    await file.WriteAllTextAsync(content);
                    BookDetail aBookInfo = new BookDetail();
                    aBookInfo.booksName = bookName.Text;
                    aBookInfo.booksType = bookType.Text;
                    aBookInfo.description = description.Text;
                    aBookInfo.ownersID = currentID;
                    aBookInfo.getDescriptionFile = FileName;
                    aBookInfo.booksCover = mediaFile.Path;
                    App.databaseBook.saveBook(aBookInfo);
                    await Navigation.PopModalAsync();
                await DisplayAlert("", "post successfully", "ok");
             }
            else
            {
                await DisplayAlert("", "enter all fields", "ok");
            }
        }
        private async void exitClicked(object sender,EventArgs e) //quit posting newbook
        {
            await Navigation.PopModalAsync();
        }

        private async void load_Clicked(object sender, EventArgs e)//load picture 
        { 
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("", "on pickphoto is avaiable", "ok");
                return;
            }

            mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (mediaFile == null)
            {
                return;
            }
            else
            {
                string path = mediaFile.Path;

            loadBookCover.Source = ImageSource.FromStream(() => { return mediaFile.GetStream(); });

            }
        }

    }
}