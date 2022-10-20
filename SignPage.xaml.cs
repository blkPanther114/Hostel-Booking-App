using System;
using System.Collections.Generic;
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
    public partial class SignPage : ContentPage
    {
        private MediaFile mediaFile;
        public User u ;
       
        public SignPage()
        {           
            InitializeComponent();
            this.imga.Source = ImageSource.FromResource("MobileAssigenment2.image.download.jpg");
        }

        private async void Sign_Clicked(object sender, EventArgs e) //register a new user
        {
            u = new User();
            
                if (cUserName.Text !=null && cUserName.Text.Length>=5 && cPassWord.Text != null && cPassWord.Text.Length >= 5 && cEmail.Text !=null && mediaFile !=null )
                {
                    if (App.Database.checkUserNameDuplicate(cUserName.Text) == true)
                    {
                    u.username = cUserName.Text;
                    u.password = cPassWord.Text;
                    u.email = cEmail.Text;
                   u.media = mediaFile.Path;
                    App.Database.saveUserAccount(u);
                    await DisplayAlert("", "register successfully", "OK");                   
                    await Navigation.PopModalAsync();
                     }
                else
                    {

                    await DisplayAlert("Attention", "user name has been taken", "OK");
                }
                }
            else
                {
                await DisplayAlert("Attention", "password or user name should at least 5 character long all fields need to be filled", "OK");
                
               }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)//quit register
        {
            await Navigation.PopModalAsync();
        }
        private async void pickPhoto_clicked(object sender, EventArgs e)//pick and load profile picture
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
                
                profilePhoto.Source = ImageSource.FromStream(() =>  { return mediaFile.GetStream(); });
                 
            }            
        }     
    }
}