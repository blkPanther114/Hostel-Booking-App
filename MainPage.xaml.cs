using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileAssigenment2
{
    public partial class MainPage : ContentPage
    {
        public afterLogin af = new afterLogin() ;
        public int currentUserID;


        public MainPage()
        {
            InitializeComponent();
            this.img.Source = ImageSource.FromResource("MobileAssigenment2.image.download.jpg");
           
        }
        private async void sClicked(object sender, EventArgs e)//sign up page
        {
            SignPage s = new SignPage();
            await Navigation.PushModalAsync(s,true);                                                               
        }
                        
        
        private async void rClicked(object sender,EventArgs e)//login user
        {
           
            if (App.Database.getUserNameAndPsssword(e1.Text,e2.Text) ==true  && App.Database.getCurrentUserByName(e1.Text).status ==null)//when user is not login and entry is not empty
            {
                af.profile.currentID = App.Database.getCurrentUserID(e1.Text);//get the current login user id
                App.Database.getCurrentUserByName(e1.Text).status ="logedIn";
                af.profile.display();
                af.getUserName();               
                af.addPage();
              
                await Navigation.PushModalAsync(af,true);             
            }
            else
            {
                await DisplayAlert("Attention", "wrong name or password", "ok");
            }          
         }

        public string RandomString(int size, bool lowerCase) //generate random 6 length password
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private void retrivePassword() 
        {
            uName.IsVisible = true;
            getPass.IsVisible = true;
            getPassword.IsVisible = false;

        }
    
        private async void retrivePassword_Clicked()//send the generated password to user email
        {
            if (App.Database.checkUserNameDuplicate(uName.Text) == false)
            {
                User u = App.Database.getCurrentUserByName(uName.Text);
                string oneTimePassWord = RandomString(6, false);
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("lijiancc0724@gmail.com", "lee823781381");
                    smtp.EnableSsl = true;
                    MailMessage msg = new MailMessage();
                    msg.Subject = "verfication code to activate account.";
                    msg.Body = "dear:" + uName.Text + " ,your one time password is :" + oneTimePassWord + "\n thank you.";
                    string toAddress = u.email;
                    msg.To.Add(toAddress);
                    string fromAddress = "<lijiancc0724@gmail.com>";
                    msg.From = new MailAddress(fromAddress);
                    
                    u.password = oneTimePassWord;
                    App.Database.saveUserAccount(u);
                uName.IsVisible = false;
                getPass.IsVisible = false;
                getPassword.IsVisible = true;
                try
                {
                    smtp.Send(msg);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("", ex.ToString(), "ok");
                }

                await DisplayAlert("", "check email for password", "ok");
                
            }
            else
            {
                await DisplayAlert("", "wrong user name", "ok");
            }

        }
    }   
   
}

