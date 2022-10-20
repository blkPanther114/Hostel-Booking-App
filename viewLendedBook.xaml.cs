using Plugin.Messaging;
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
	public partial class viewLendedBook : ContentPage
	{
        public BookDetail bookBeingBorrowed;
		public viewLendedBook ()
		{
			InitializeComponent ();
		}
        public viewLendedBook(BookDetail book)
        {
            bookBeingBorrowed = book;
            InitializeComponent();
        }
        public void setData(BookDetail book)//get the book detials
        {
            bookBeingBorrowed = book;
            images.Source = bookBeingBorrowed.booksCover;
            name.Text = bookBeingBorrowed.booksName;
            type.Text = bookBeingBorrowed.booksType;
            Bername.Text = App.Database.getCurrentUserName(Convert.ToInt32(bookBeingBorrowed.borrowersID));
            email.Text= App.Database.getCurrentUserNameEmail(Convert.ToInt32(bookBeingBorrowed.borrowersID));
            view.Text = bookBeingBorrowed.booksViewed.ToString();
            borrow.Text = bookBeingBorrowed.booksLended.ToString();
            getDescription.Text = bookBeingBorrowed.description;
            startDate.Text = bookBeingBorrowed.getBrorrowedDate.ToString();
            returnDate.Text = bookBeingBorrowed.getReturnedDate.ToString();
        }
        private  async void okClicked(object sender,EventArgs e)
        {

            await Navigation.PopModalAsync();
        }
        
        private  async void emailClicked(object sender,EventArgs e)//send emial to borrower
        {
            var emailTask = CrossMessaging.Current.EmailMessenger;
            if (emailTask.CanSendEmail)
            {                             
                var email = new EmailMessageBuilder()
                  .To(App.Database.getCurrentUserNameEmail(Convert.ToInt32(bookBeingBorrowed.borrowersID)))
                  .Subject("Xamarin Messaging Plugin")
                  .Body("Please return the book on time!")
                  .Build();
                emailTask.SendEmail(email);
                await DisplayAlert("", "email sent", "ok");
            }
        }       
    }
}