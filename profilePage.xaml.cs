using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAssigenment2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class profilePage : ContentPage
    {
        private MediaFile mediaFile;
        public int currentID;
        public profilePage()
        {

            InitializeComponent();
        }
        public void display()
        {

            this.profileUserName.Text = App.Database.getCurrentUserName(currentID); //get the login user;s anme
            this.userEmail.Text = App.Database.getCurrentUserNameEmail(currentID);//email 
            this.image.Source = ImageSource.FromFile(App.Database.getCurrentUser(currentID).media);//profile picture

        }
        private void editClicked()
        {

            save.IsVisible = true;
            edit.IsVisible = false;
            changeName.IsVisible = true;
            newEmail.IsVisible = true;
            newpassword.IsVisible = true;
            cancel.IsVisible = true;
            getImage.IsVisible = true;


        }
        private async void saveClicked(User u) //modify and save the profile edition
        {


            if (changeName.Text != null &&  changeName.Text.Length >= 5 && newEmail.Text!=null &&  newpassword.Text !=null && newpassword.Text.Length >=5 && mediaFile !=null)
            {
                if (App.Database.checkUserNameDuplicate(changeName.Text))
                {
                    u = App.Database.getCurrentUser(currentID);
                    u.username = changeName.Text;
                    profileUserName.Text = changeName.Text;
                    userEmail.Text = newEmail.Text;
                    u.password = newpassword.Text;
                    u.media = mediaFile.Path;
                    App.Database.saveUserAccount(u);
                    save.IsVisible = false;
                    edit.IsVisible = true;
                    changeName.IsVisible = false;
                    cancel.IsVisible = false;
                    newpassword.IsVisible = false;
                    newEmail.IsVisible = false;
                    getImage.IsVisible = false;
                   
                }
                else
                {
                    await DisplayAlert("", "name is taken", "ok");
                }
            }
            else
            {
                await DisplayAlert("", "enter all fields and at least 5 characters long", "ok");
            }
        }
        private void cancelClicked()
        {          
            edit.IsVisible = true;
            changeName.IsVisible = false;
            cancel.IsVisible = false;
            newEmail.IsVisible = false;
            newpassword.IsVisible = false;
            save.IsVisible = false;
            getImage.IsVisible = false;
            
        }

        private async void logout_Clicked() //log out of the current user
        {
            App.Database.getCurrentUser(currentID).status= null ;
            App.Current.MainPage = new MainPage();
            await Navigation.PopModalAsync();
        }

        private async void pickImageClicked(object sender, EventArgs e) //pick and load image
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

                image.Source = ImageSource.FromStream(() => { return mediaFile.GetStream(); });

            }
        }
    }
}