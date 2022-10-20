using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAssigenment2
{
    public class generateRandomString
    {
        public string generateFileKey()//generate a random password for user if they click forget password
        {
            Char[] letters = "dh1234dwudguwuiioinlknce12334lmb".ToCharArray();
            Random r = new Random();
            string randomString = "";
            for(int i = 0; i < 12; i++)
            {
                randomString += letters[r.Next(0, 25)].ToString();
            }
            return randomString;
        }
    }
}
