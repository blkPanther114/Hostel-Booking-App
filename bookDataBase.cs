using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MobileAssigenment2
{
    public class bookDataBase
    {
        static SQLiteConnection bDatabase;
        public const string DbFileName = "books.db3";
        public string CurrentState;

        public bookDataBase()
        {
            try
            {
                bDatabase = DependencyService.Get<ISQLite>().GetConnection(DbFileName);
                bDatabase.CreateTable<BookDetail>();
                CurrentState = "Database created";
            }
            catch (SQLiteException ex)
            {
                CurrentState = ex.Message;
            }
        }

        //save or update 
        public int saveBook(BookDetail book)
        {
            if (book.ID > 0)
            {
                return bDatabase.Update(book);
            }
            else
            {
                return bDatabase.Insert(book);
            }
        }

        //delete
        public int deleteBook(BookDetail book)
        {
            return bDatabase.Delete(book);
        }

        //convert table book to list
        public List<BookDetail> GetBookList()
        {
            return bDatabase.Table<BookDetail>().ToList();
        }
       
       //get yout posted booked that has not been borrowed
        public List<BookDetail> getYourPostedBooks(int userID)
        {

            var d = from BookDetail book in GetBookList()
                    where book.ownersID==userID && book.borrowersID==null
                    select book;
            return d.ToList();

        }

    
        //get all other users' book
        public List<BookDetail> getMarketBooks(int userID)
        {
            var d = from BookDetail book in GetBookList()
                    where book.ownersID !=userID && book.borrowersID ==null
                    select book;
            return d.ToList();
        }

        //search book by some name 
        public List<BookDetail> SearchBookByName(string bName,int ID)
        {
            var d = from BookDetail book in getMarketBooks(ID)
                    where book.booksName.Contains(bName) || book.booksType.Contains(bName) || book.description.Contains(bName)
                    select book;
            return d.ToList();
        }

        //get the book you have borrowed
        public List<BookDetail> getYourBorrowedBooks(int ID)
        {
            var d = from BookDetail book in GetBookList()
                    where book.borrowersID == ID.ToString()
                    select book;
            return d.ToList();
        }

        //get your books that has not been borrowed
        public List<BookDetail> booksNotBorrowed(int ID)
        {
            var bookNotBorrowed = from BookDetail book in GetBookList()
                                  where book.ownersID == ID && book.borrowersID ==null
                                  select book;
            return bookNotBorrowed.ToList();
        }

        //get your being borrowed books
        public List<BookDetail> booksBorrowed(int  ID)
        {
            var bookBorrowed = from BookDetail book in GetBookList()
                               where book.ownersID == ID && book.borrowersID !=null
                               select book;
            return bookBorrowed.ToList();
        }


    }
}

