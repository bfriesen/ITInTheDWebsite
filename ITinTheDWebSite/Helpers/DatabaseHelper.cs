using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITinTheDWebSite.Helpers
{

    public static class DatabaseHelper
    {


        public static int GetIDfromUserName(string username)
        {

            using (ITintheDTestEntities context = new ITintheDTestEntities())
            {

                var user = from f in context.UserProfiles
                           where f.UserName.Equals(username)
                           select f;

                if (user.Count() > 0)
                {

                    return user.FirstOrDefault().UserId;
                }
                else
                {
                    return -1;
                    //throw new Exception("SOrry this user .... ");
                }

            }

        }

        public static bool UploadFile(File f, string username)
        {

            int UserID = GetIDfromUserName(username);

            try
            {
                using (ITintheDTestEntities context = new ITintheDTestEntities())
                {

                    var UserResume = from r in context.Files 
                                     where r.UserID == UserID 
                                     select r;
                    if (UserResume.Count() > 0)
                    {
                        File currentResume = UserResume.FirstOrDefault();
                        currentResume.UserID = UserID;
                        currentResume.FileContent = f.FileContent;
                        currentResume.FileName = f.FileName;
                        currentResume.ContentLength = f.ContentLength;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {

                        context.AddToFiles(f);
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static File GetResume(int id)
        {

            using (ITintheDTestEntities context = new ITintheDTestEntities())
            {

                var resume = from f in context.Files
                           //where f.FileID.Equals(id)
                           where f.FileID == id
                           select f;

                if (resume.Count() > 0)
                {

                    return resume.FirstOrDefault();
                }
                else
                {
                    return null;
                    //throw new Exception("SOrry this user .... ");
                }

            }

        }

    }

}