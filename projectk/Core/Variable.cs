using projectk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Projectk
{
    public class Variable
    {
        public static int NumberOfArticleLoaded=20;
        public static bool AutoApprove = true;
        public static string ApiKey = "x9n1ufyyyj00odt";
        public static string ApiSecret = "vwevab5370evd3v";
        public static string UserToken = "uss2v2w38xlfvrb";
        public static string UserSecret = "apz9iqaoi3oa3un";
        [ThreadStatic]
        private static Random rnd;
        public static string WebFolder()
        {
            return HttpRuntime.AppDomainAppPath;
        }
        public static string MapPathFile()
        {
            return HttpRuntime.AppDomainAppPath + "files\\";

        }
        public static int GetRandomInteger()
        {
            if (rnd == null) rnd = new Random((int)DateTime.Now.Ticks);
            return Math.Abs(Convert.ToInt32(rnd.Next(int.MaxValue - 10000) + 10000));

        }

        public static int GetRandomInteger(int minValue, int maxValue)
        {
            if (rnd == null) rnd = new Random((int)DateTime.Now.Ticks);
            return Convert.ToInt32(rnd.Next(minValue, maxValue));
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }



        }
        public static projectk.Models.UserProfile GetUserByID(int id)
        {
            projectk.Models.UserProfile user = null;

            ProjectkContext db = new ProjectkContext();
            user = db.UserProfiles.Find(id);


            return user;
        }


    }
}