using projectk.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Projectk
{
    public class Pageview
    {

        public static System.Collections.Hashtable A;

        private static System.Timers.Timer myTimer;
        public static void IsRunning()
        {
            myTimer = new System.Timers.Timer(1 * 10 * 1000);
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(MyTimer_Elapsed);

            myTimer.AutoReset = true;
            myTimer.Enabled = true;

            myTimer.Start();
        }
        private static void MyTimer_Elapsed(Object myObject, EventArgs myEventArgs)
        {
            myTimer.Stop();
            ProjectkContext db = new ProjectkContext();

            foreach (DictionaryEntry entry in A)
            {
                int articleID = int.Parse(entry.Key.ToString());


                Article o = db.Articles.Where(a => a.ID == articleID).FirstOrDefault();
                if (o != null)
                {
                    o.PageView += int.Parse(entry.Value.ToString());
                    //update like for user
                    long numofLike = db.Articles.Where(a => a.UserName == o.UserName).Sum(a => a.Like);
                    if (numofLike == 0) numofLike = 10;
                    UserProfile u = db.UserProfiles.Where(a => a.UserName == o.UserName).FirstOrDefault();
                    if (u != null)
                        u.TotalLike = unchecked((int)numofLike);
                }
            }
            db.SaveChanges();
            A.Clear();
            myTimer.Start();
        }
        public static int ReadFile()
        {
            StreamReader sr = new StreamReader("D:/pv.txt");
            var a = sr.ReadLine();
            sr.Close();
            return int.Parse(a);

        }
        public static void WriteFile(int value)
        {
            StreamWriter sw = new StreamWriter("D:/pv.txt");
            sw.WriteLine(value);
            sw.Close();
        }

    }
}