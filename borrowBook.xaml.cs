using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAssigenment2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class borrowBook : ContentPage
    {

        public BookDetail SelectedBook;
        public int currenLogInUserID;
        public DatePicker mindate;//initialse min data
        public DatePicker maxdate;
        public borrowBook()
        {

            InitializeComponent();
        }
        public borrowBook(BookDetail book)//constructor take book as argument
        {
            SelectedBook = book; 
            InitializeComponent();
        }
        public void SetData(BookDetail book) //fetch the book data
        {
            SelectedBook = book;
            images.Source = SelectedBook.booksCover;
            name.Text = SelectedBook.booksName;
            type.Text = SelectedBook.booksType;
            oname.Text = App.Database.getCurrentUserName(SelectedBook.ownersID);
            description.Text = SelectedBook.description;
            viewTimes.Text = SelectedBook.booksViewed.ToString();
            lendedTime.Text = SelectedBook.booksLended.ToString();
        }

        public async void cancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        public async void borrowClicked(object sender, EventArgs e) //borrow a book
        {
            if (App.databaseBook.getYourBorrowedBooks(currenLogInUserID).Count() >= 5)
            {
                await DisplayAlert("caution", "exceed the number of 5 books you can borrow", "ok");
            }
            else
            {
                if (startDate.ToString() != null && endDate.ToString() != null) //when date is selected 
                {
                    //get all detals about the selected book
                    var brorrowDate = startDate.Date.ToString();
                    var returnD = endDate.Date.ToString();
                    SelectedBook.borrowersID = currenLogInUserID.ToString();
                    SelectedBook.getBrorrowedDate = brorrowDate;
                    SelectedBook.getReturnedDate = returnD;
                    SelectedBook.booksLended = SelectedBook.booksLended + 1;
                    App.databaseBook.saveBook(SelectedBook);
                    await DisplayAlert("", "book borrowed successfully", "ok");               
                    await Navigation.PopModalAsync();
                    //send email to book owners when the book is being borrowed
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("lijiancc0724@gmail.com", "lee823781381");
                    smtp.EnableSsl = true;
                    MailMessage msg = new MailMessage();
                    msg.Subject = "Your book has been borrowed";
                    msg.Body = "dear:" + App.Database.getCurrentUserName(SelectedBook.ownersID) + " ,your book :" + SelectedBook.booksName + ",has been borrowed by ," + App.Database.getCurrentUserName(Convert.ToInt32(SelectedBook.borrowersID))
                         + ",from date :" + SelectedBook.getBrorrowedDate.ToString() + ",to date:" + SelectedBook.getReturnedDate.ToString();
                    string toAddress = App.Database.getCurrentUserNameEmail(SelectedBook.ownersID);
                    msg.To.Add(toAddress);
                    string fromAddress = "<lijiancc0724@gmail.com>";
                    msg.From = new MailAddress(fromAddress);
                    smtp.Send(msg);

                }
                else
                {
                    await DisplayAlert("", "please choose date", "ok");
                }
            }
        }

    }
}
               
        
    

        
	
