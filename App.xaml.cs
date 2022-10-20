using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobileAssigenment2
{
    public partial class App : Application
    {
        static userSQLiteDatabase database;
        static bookDataBase bDatabase;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());          
          
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public static userSQLiteDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new userSQLiteDatabase();
                }
                return database;
            }

        }

       public static bookDataBase databaseBook
        {
            get
            {
                if (bDatabase == null)
                {
                    bDatabase = new bookDataBase();
                }
                return bDatabase;
            }

        }
      
    }
}