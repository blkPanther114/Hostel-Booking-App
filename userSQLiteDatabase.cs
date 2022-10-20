using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
namespace MobileAssigenment2
{

    public class userSQLiteDatabase
    {

        static SQLiteConnection database;
        public const string DBFileName = "userSQLite.db3";
        public string CurrentState;
        profilePage p = new profilePage();
        
        public userSQLiteDatabase()
        {
            try
            {
                database = DependencyService.Get<ISQLite>().GetConnection(DBFileName);
                database.CreateTable<User>();
                CurrentState = "Database created";

            }
            catch (SQLiteException ex)
            {
                CurrentState = ex.Message;
            }
        }

        public int saveUserAccount(User u)
        {
            if (u.ID > 0)
            {
                return database.Update(u);
            }
            else
            {
                return database.Insert(u);
            }
        }
       
        public List<User> getUserAccount()
         {           
             return database.Table<User>().ToList();
         }
       
         
        public bool checkUserNameDuplicate(string name)
        {
            
            var d = database.Table<User>();
            var duplicate= d.Where(x => x.username == name).FirstOrDefault();
             if (duplicate == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
         }

        

        public bool getUserNameAndPsssword(string name,string password) { 
        var d = database.Table<User>();
        var data1 = d.Where(x => x.username == name && x.password == password).FirstOrDefault(); //Linq Query  
            if(data1 == null)
            {
                return false;
            }
            else
            {
                return true;
            }
    }

        public string getCurrentUserName(int id)
        {

            var users = from User u in getUserAccount()
                        where u.ID == id
                        select u;

            var getName = users.FirstOrDefault();
            return getName.username;
        }

        public int getCurrentUserID(string userName)
        {

            var users = from User u in getUserAccount()
                        where u.username == userName
                        select u;

            var user = users.FirstOrDefault();
            return user.ID;
        }

        public User getCurrentUser(int id)
        {
            var users = from User u in getUserAccount()
                        where u.ID==id
                        select u;

            var user = users.FirstOrDefault();
            return user;
        }

        public User getCurrentUserByName(string name)
        {
            var users = from User u in getUserAccount()
                        where u.username == name
                        select u;

            var user = users.FirstOrDefault();
            return user;
        }

        public string getCurrentUserNameEmail(int id)
        {

            var users = from User u in getUserAccount()
                        where u.ID == id 
                        select u;

            var getName = users.FirstOrDefault();
            return getName.email;
        }

        public string getCurrentUserNameEmailByName(string username)
        {

            var users = from User u in getUserAccount()
                        where u.username == username
                        select u;

            var getName = users.FirstOrDefault();
            return getName.email;
        }
    }
}