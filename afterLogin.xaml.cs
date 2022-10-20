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
	public partial class afterLogin : TabbedPage
    {

        public profilePage profile = new profilePage();
        public brorrowed brorrow = new brorrowed();
        public lended lend = new lended();
        public market m = new market();
        public int getUserID;
        public afterLogin ()
		{         
            InitializeComponent ();
		}
       
        //add all pages into afterlogin
       public void addPage()
        {
            tadmin.Children.Add(profile);
            tadmin.Children.Add(lend);
            tadmin.Children.Add(brorrow);
            tadmin.Children.Add(m);
        }

        //get the login user id
       public void getUserName()
        {
            lend.pForm.currentID = profile.currentID;
            lend.pForm.m.currentID= profile.currentID;
            lend.currentID = profile.currentID;           
            m.bb.currenLogInUserID= profile.currentID;
            brorrow.currentUserID= profile.currentID;
            m.currentID = profile.currentID;
            lend.modify.currentID= profile.currentID;
        }
        
        


    }
}