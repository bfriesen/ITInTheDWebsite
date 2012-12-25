using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using ITinTheDWebSite.Models;
using System.IO;
using System.Data.SqlClient;
using System.Web.Security;

namespace ITinTheDWebSite.Helpers
{

    public static class DatabaseHelper
    {
        public static void AddUserToRole(string user, string role)
        {
            var roles = (SimpleRoleProvider)Roles.Provider;


            string[] usrs = new string[] { user };
            string[] r = new string[] { role };

            if (!roles.IsUserInRole(user, role)) roles.AddUsersToRoles(usrs, r);
        }
        //===========================================================================
        //     Get / Store Sponsor information
        //===========================================================================
        public static SponsorModel GetSponsorData(SponsorModel spons)
        {
            int UserId = WebSecurity.CurrentUserId;
            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var corporatesponsor = from r in context.ProspectiveCorporateSponsor
                                           where r.SponsorId == UserId
                                           select r;

                    if (corporatesponsor.Count() > 0)
                    {
                        spons.CompanyName = corporatesponsor.FirstOrDefault().CompanyName;
                        spons.CompanyAddress = corporatesponsor.FirstOrDefault().CompanyAddress;
                        spons.ContactName = corporatesponsor.FirstOrDefault().ContactName;
                        spons.Title = corporatesponsor.FirstOrDefault().Title;
                        spons.Telephone = corporatesponsor.FirstOrDefault().Telephone;
                        spons.EmailAddress = corporatesponsor.FirstOrDefault().EmailAddress;
                        spons.Reason = corporatesponsor.FirstOrDefault().Reason;
                        return (spons);
                    }
                    else
                    {
                        return (null);
                    }
                }
            }
            catch
            {
                return (null);
            }
        }

        public static bool StoreSponsorData(SponsorModel sponsor)
        {
            int UserId = WebSecurity.GetUserId(sponsor.EmailAddress);

            bool edit = false;

            try
            {
                ProspectiveCorporateSponsor CurrentSponsor;

                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var SponsorData = from r in context.ProspectiveCorporateSponsor
                                      where r.SponsorId == UserId
                                      select r;
                    if (SponsorData.Count() > 0 && UserId > 0)
                    {
                        CurrentSponsor = SponsorData.FirstOrDefault();
                        CurrentSponsor.CompanyAddress = sponsor.CompanyAddress;
                        CurrentSponsor.CompanyName = sponsor.CompanyName;
                        CurrentSponsor.ContactName = sponsor.ContactName;
                        CurrentSponsor.EmailAddress = sponsor.EmailAddress;
                        CurrentSponsor.Title = sponsor.Title;
                        CurrentSponsor.Telephone = sponsor.Telephone;
                        CurrentSponsor.Reason = sponsor.Reason + ' ';
                        CurrentSponsor.SponsorId = UserId;

                        edit = true;
                    }
                    else
                    {
                        CurrentSponsor = new ProspectiveCorporateSponsor();

                        CurrentSponsor.Status = (int)SponsorStatus.Initial;
                        CurrentSponsor.CompanyAddress = sponsor.CompanyAddress;
                        CurrentSponsor.CompanyName = sponsor.CompanyName;
                        CurrentSponsor.ContactName = sponsor.ContactName;
                        CurrentSponsor.EmailAddress = sponsor.EmailAddress;
                        CurrentSponsor.Title = sponsor.Title;
                        CurrentSponsor.Telephone = sponsor.Telephone;
                        CurrentSponsor.Reason = sponsor.Reason + ' ';
                        

                        context.AddToProspectiveCorporateSponsor(CurrentSponsor);
                    }

                    try
                    {
                        if (edit == false)
                        {
                            WebSecurity.CreateUserAndAccount(sponsor.EmailAddress, sponsor.Password);
                            WebSecurity.Login(sponsor.EmailAddress, sponsor.Password);

                            DatabaseHelper.AddUserToRole(sponsor.EmailAddress, "Sponsor");

                            CurrentSponsor.SponsorId = WebSecurity.GetUserId(sponsor.EmailAddress);
                        }

                        context.SaveChanges();

                        return true;
                    }
                    catch(Exception e)
                    {
                        string errorMessage = e.Message;

                        return false;
                    }

                }
            }

            catch(Exception ex)
            {
                string exMessage = ex.Message;

                return false;
            }
        }



        //===========================================================================
        //     Get / Store Academic information
        //===========================================================================
        public static AcademicModel GetAcademicdData(AcademicModel academic)
        {
            int UserId = WebSecurity.CurrentUserId;
            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var ExistingAcademic = from r in context.ProspectiveAcademic
                                           where r.AcademicId == UserId
                                           select r;

                    if (ExistingAcademic.Count() > 0)
                    {
                        academic.AcademyName = ExistingAcademic.FirstOrDefault().AcademyName;
                        academic.AcademyAddress = ExistingAcademic.FirstOrDefault().AcademyAddress;
                        academic.PrimaryContactName = ExistingAcademic.FirstOrDefault().PrimaryContactName;
                        academic.PrimaryTitle = ExistingAcademic.FirstOrDefault().PrimaryTitle;
                        academic.PrimaryTelephone = ExistingAcademic.FirstOrDefault().PrimaryTelephone;
                        academic.PrimaryEmailAddress = ExistingAcademic.FirstOrDefault().PrimaryEmailAddress;

                        academic.SecondaryContactName = ExistingAcademic.FirstOrDefault().SecondaryContactName;
                        academic.SecondaryTitle = ExistingAcademic.FirstOrDefault().SecondaryTitle;
                        academic.SecondaryTelephone = ExistingAcademic.FirstOrDefault().SecondaryTelephone;
                        academic.SecondaryEmailAddress = ExistingAcademic.FirstOrDefault().SecondaryEmailAddress;
                        return (academic);
                    }
                    else
                    {
                        return (null);
                    }
                }
            }
            catch
            {
                return (null);
            }
        }

        public static bool StoreAcademicData(AcademicModel academic)
        {
            int UserId = WebSecurity.CurrentUserId;

            bool edit = false;

            try
            {
                ProspectiveAcademic CurrentAcademic;

                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var AcademicData = from r in context.ProspectiveAcademic
                                       where r.AcademicId == UserId
                                       select r;
                    if (AcademicData.Count() > 0 && UserId > 0)
                    {
                        //add = false;
                        CurrentAcademic = AcademicData.FirstOrDefault();
                        CurrentAcademic.AcademyAddress = academic.AcademyAddress;
                        CurrentAcademic.AcademyName = academic.AcademyName;
                        CurrentAcademic.PrimaryContactName = academic.PrimaryContactName;
                        CurrentAcademic.PrimaryEmailAddress = academic.PrimaryEmailAddress;
                        CurrentAcademic.PrimaryTitle = academic.PrimaryTitle;
                        CurrentAcademic.PrimaryTelephone = academic.PrimaryTelephone;

                        CurrentAcademic.SecondaryContactName = academic.SecondaryContactName;
                        CurrentAcademic.SecondaryEmailAddress = academic.SecondaryEmailAddress;
                        CurrentAcademic.SecondaryTitle = academic.SecondaryTitle;
                        CurrentAcademic.SecondaryTelephone = academic.SecondaryTelephone;

                        CurrentAcademic.AcademicId = UserId;

                        edit = true;
                    }
                    else
                    {
                        CurrentAcademic = new ProspectiveAcademic();

                        CurrentAcademic.Status = (int)AcademicStatus.Initial;
                        CurrentAcademic.AcademyAddress = academic.AcademyAddress;
                        CurrentAcademic.AcademyName = academic.AcademyName;
                        CurrentAcademic.PrimaryContactName = academic.PrimaryContactName;
                        CurrentAcademic.PrimaryEmailAddress = academic.PrimaryEmailAddress;
                        CurrentAcademic.PrimaryTitle = academic.PrimaryTitle;
                        CurrentAcademic.PrimaryTelephone = academic.PrimaryTelephone;

                        CurrentAcademic.SecondaryContactName = academic.SecondaryContactName;
                        CurrentAcademic.SecondaryEmailAddress = academic.SecondaryEmailAddress;
                        CurrentAcademic.SecondaryTitle = academic.SecondaryTitle;
                        CurrentAcademic.SecondaryTelephone = academic.SecondaryTelephone;

                        context.AddToProspectiveAcademic(CurrentAcademic);
                    }

                    try
                    {
                        if (edit == false)
                        {
                            WebSecurity.CreateUserAndAccount(academic.PrimaryEmailAddress, academic.Password);
                            WebSecurity.Login(academic.PrimaryEmailAddress, academic.Password);

                            DatabaseHelper.AddUserToRole(academic.PrimaryEmailAddress, "Educator");

                            CurrentAcademic.AcademicId = WebSecurity.GetUserId(academic.PrimaryEmailAddress);
                        }

                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception ex) {
                       if (ex is SqlException) {
                          // Handle more specific SqlException exception here.
                           string mess = ex.Message;
                       }
 
                           return false;
                          // Handle generic ones here.

                    }

                }
            }

            catch
            {
                return false;
            }
        }

        //===========================================================================
        //     Get / Store Student information
        //===========================================================================
        public static ProspectModel GetProspectData(ProspectModel prospect)
        {
            int UserId = WebSecurity.CurrentUserId;
            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var ExistingProspect = from r in context.ProspectiveStudent
                                           where r.UserId == UserId
                                           select r;

                    if (ExistingProspect.Count() > 0)
                    {
                        prospect.Name = ExistingProspect.FirstOrDefault().Name;
                        prospect.Telephone = ExistingProspect.FirstOrDefault().Telephone;
                        prospect.EmailAddress = ExistingProspect.FirstOrDefault().EmailAddress;
                        prospect.DesiredCareerPath = ExistingProspect.FirstOrDefault().DesiredCareerPath;
                        prospect.Gender = ExistingProspect.FirstOrDefault().Gender;

                        return (prospect);
                    }
                    else
                    {
                        return (null);
                    }
                }
            }
            catch
            {
                return (null);
            }

        }

        public static bool StoreProspectData(ProspectModel prospect)
        {
            int UserId = WebSecurity.CurrentUserId;

            ProspectiveStudent CurrentStudent;

            bool edit = false;

            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var ProspectData = from r in context.ProspectiveStudent
                                       where r.UserId == UserId
                                       select r;
                    if (ProspectData.Count() > 0 && UserId > 0)
                    {
                        edit = true;

                        CurrentStudent = ProspectData.FirstOrDefault();
                        CurrentStudent.UserId = UserId;
                        CurrentStudent.Name = prospect.Name;
                        CurrentStudent.Telephone = prospect.Telephone;
                        CurrentStudent.EmailAddress = prospect.EmailAddress;
                        CurrentStudent.DesiredCareerPath = prospect.DesiredCareerPath;
                        CurrentStudent.Gender = prospect.Gender;

                        // Store a new resume file if user supplied one
                        if (prospect.ResumeFile != null && prospect.ResumeFile.ContentLength > 0)
                        {
                            ProspectiveStudentResume Resume = new ProspectiveStudentResume();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                prospect.ResumeFile.InputStream.CopyTo(ms);

                                Resume.FileContent = ms.ToArray();
                                Resume.FileName = Path.GetFileName(prospect.ResumeFile.FileName);
                                Resume.ContentType = prospect.ResumeFile.ContentType;
                                Resume.ContentLength = prospect.ResumeFile.ContentLength;

                                DatabaseHelper.UploadFile(Resume);
                            }
                        }

                        // store transcripts if supplied
                        if (prospect.TranscriptFile != null && prospect.TranscriptFile.ContentLength > 0)
                        {

                            ProspectiveStudentTranscript Transcript = new ProspectiveStudentTranscript();
                            using (MemoryStream ts = new MemoryStream())
                            {
                                prospect.TranscriptFile.InputStream.CopyTo(ts);

                                Transcript.FileContent = ts.ToArray();
                                Transcript.FileName = Path.GetFileName(prospect.TranscriptFile.FileName);
                                Transcript.ContentType = prospect.TranscriptFile.ContentType;
                                Transcript.ContentLength = prospect.TranscriptFile.ContentLength;

                                DatabaseHelper.UploadTRanscript(Transcript);

                                CurrentStudent.TranscriptUploaded = "Yes";
                            }
                        }

                    }
                    else
                    {
                        CurrentStudent = new ProspectiveStudent();

                        // Need to make sure the prospect supplied a resume file 
                        if (prospect.ResumeFile != null && prospect.ResumeFile.ContentLength > 0)
                        {
                            ProspectiveStudentResume Resume = new ProspectiveStudentResume();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                prospect.ResumeFile.InputStream.CopyTo(ms);

                                Resume.FileContent = ms.ToArray();
                                Resume.FileName = Path.GetFileName(prospect.ResumeFile.FileName);
                                Resume.ContentType = prospect.ResumeFile.ContentType;
                                Resume.ContentLength = prospect.ResumeFile.ContentLength;

                                DatabaseHelper.UploadFile(Resume);

                                CurrentStudent.ResumeUploaded = "Yes";

                                // store transcripts if supplied
                                if (prospect.TranscriptFile != null && prospect.TranscriptFile.ContentLength > 0)
                                {

                                    ProspectiveStudentTranscript Transcript = new ProspectiveStudentTranscript();
                                    using (MemoryStream ts = new MemoryStream())
                                    {
                                        prospect.TranscriptFile.InputStream.CopyTo(ts);

                                        Transcript.FileContent = ts.ToArray();
                                        Transcript.FileName = Path.GetFileName(prospect.TranscriptFile.FileName);
                                        Transcript.ContentType = prospect.TranscriptFile.ContentType;
                                        Transcript.ContentLength = prospect.TranscriptFile.ContentLength;

                                        DatabaseHelper.UploadTRanscript(Transcript);

                                        CurrentStudent.TranscriptUploaded = "Yes";
                                    }
                                }
                                else
                                {
                                    CurrentStudent.TranscriptUploaded = "No";
                                }


                                CurrentStudent.Status = (int)StudentStatus.Initial;
                                CurrentStudent.Name = prospect.Name;
                                CurrentStudent.Telephone = prospect.Telephone;
                                CurrentStudent.EmailAddress = prospect.EmailAddress;
                                CurrentStudent.DesiredCareerPath = prospect.DesiredCareerPath;
                                CurrentStudent.Gender = prospect.Gender;

                                context.AddToProspectiveStudent(CurrentStudent);
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                    DatabaseHelper.AddUserToRole(prospect.EmailAddress, "Student");

                    try
                    {
                        if (edit == true)
                        {
                            WebSecurity.CreateUserAndAccount(prospect.EmailAddress, prospect.Password);
                            WebSecurity.Login(prospect.EmailAddress, prospect.Password);

                            DatabaseHelper.AddUserToRole(prospect.EmailAddress, "Sponsor");

                            CurrentStudent.UserId = WebSecurity.GetUserId(prospect.EmailAddress);
                        }

                        context.SaveChanges();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
            catch
            {
                return false;
            }
        }

        public static bool UploadTRanscript(ProspectiveStudentTranscript f)
        {

            int UserId = WebSecurity.CurrentUserId;

            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {

                    var UserTranscript = from r in context.ProspectiveStudentTranscripts
                                         where r.UserId == UserId
                                         select r;
                    if (UserTranscript.Count() > 0)
                    {
                        ProspectiveStudentTranscript currentTranscript = UserTranscript.FirstOrDefault();

                        currentTranscript.UserId = UserId;
                        currentTranscript.FileContent = f.FileContent;
                        currentTranscript.FileName = f.FileName;
                        currentTranscript.ContentLength = f.ContentLength;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        f.UserId = UserId;
                        context.AddToProspectiveStudentTranscripts(f);
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

        public static bool UploadFile(ProspectiveStudentResume f)
        {

            int UserId = WebSecurity.CurrentUserId;

            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {

                    var UserResume = from r in context.ProspectiveStudentResumes
                                     where r.UserId == UserId
                                     select r;
                    if (UserResume.Count() > 0)
                    {
                        ProspectiveStudentResume currentResume = UserResume.FirstOrDefault();

                        currentResume.UserId = UserId;
                        currentResume.FileContent = f.FileContent;
                        currentResume.FileName = f.FileName;
                        currentResume.ContentLength = f.ContentLength;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        f.UserId = UserId;
                        context.AddToProspectiveStudentResumes(f);
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