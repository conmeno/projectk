using projectk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Projectk
{
    public class Utility
    {
       
       
        public static string GetUsername(Stream input)
        {
            return "";
        }
        public static int GetUserIDByUsername(string Username)
        {
            int rs=0;
            ProjectkContext db = new ProjectkContext();
            List<UserProfile> listu = db.UserProfiles.Where(a => a.UserName ==HttpContext.Current.User.Identity.Name).ToList();


        
            if (listu.Count > 0)
            {
                rs = listu[0].UserId;
            }
            return rs;
        }

    }
}