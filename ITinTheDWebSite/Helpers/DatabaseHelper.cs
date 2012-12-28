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
        public static RegisterModel GetAdminData(RegisterModel regAdmin)
        {
            int UserId = WebSecurity.CurrentUserId;

            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var currentAdmin = from r in context.SiteAdmin
                                       where r.UserId == UserId
                                       select r;

                    if (currentAdmin.Count() > 0)
                    {
                        regAdmin.Name = currentAdmin.FirstOrDefault().Name;
                        regAdmin.EmailAddress = currentAdmin.FirstOrDefault().EmailAddress;
                        regAdmin.CompanyName = currentAdmin.FirstOrDefault().Company;
                        regAdmin.Telephone = currentAdmin.FirstOrDefault().Telephone;
                        regAdmin.EmailAddress = currentAdmin.FirstOrDefault().EmailAddress;

                        return (regAdmin);
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

        public static bool StoreAdminData(RegisterModel regAdmin, ref bool edit)
        {
            int UserId = WebSecurity.GetUserId(regAdmin.EmailAddress);

            edit = false;

            try
            {
                SiteAdmin CurrentAdmin;

                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var AdminData = from r in context.SiteAdmin
                                    where r.UserId == UserId
                                    select r;
                    if (AdminData.Count() > 0 && UserId > 0)
                    {
                        CurrentAdmin = AdminData.FirstOrDefault();
                        CurrentAdmin.Company = regAdmin.CompanyName;
                        CurrentAdmin.EmailAddress = regAdmin.EmailAddress;
                        CurrentAdmin.Name = regAdmin.Name;
                        CurrentAdmin.Telephone = regAdmin.Telephone;
                        CurrentAdmin.UserId = UserId;

                        edit = true;
                    }
                    else
                    {
                        CurrentAdmin = new SiteAdmin();

                        CurrentAdmin.Company = regAdmin.CompanyName;
                        CurrentAdmin.Name = regAdmin.Name;
                        CurrentAdmin.EmailAddress = regAdmin.EmailAddress;
                        CurrentAdmin.Telephone = regAdmin.Telephone;

                        context.AddToSiteAdmin(CurrentAdmin);
                    }

                    try
                    {
                        if (edit == false)
                        {
                            WebSecurity.CreateUserAndAccount(regAdmin.EmailAddress, regAdmin.Password);

                            DatabaseHelper.AddUserToRole(regAdmin.EmailAddress, "Admin");

                            CurrentAdmin.UserId = WebSecurity.GetUserId(regAdmin.EmailAddress);
                        }

                        context.SaveChanges();

                        return true;
                    }
                    catch (Exception e)
                    {
                        string errorMessage = e.Message;

                        return false;
                    }

                }
            }

            catch (Exception ex)
            {
                string exMessage = ex.Message;

                return false;
            }
        }

        public static bool RemoveAdminData(int UserId)
        {
            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var AdminData = from r in context.SiteAdmin
                                    where r.UserId == UserId
                                    select r;

                    if (AdminData.Count() > 0 && UserId > 0)
                    {
                        context.DeleteObject(AdminData.FirstOrDefault());
                    }

                    else
                    {
                        return false;
                    }

                    try
                    {
                        context.SaveChanges();

                        return true;
                    }

                    catch (Exception e)
                    {
                        return false;
                    }

                }
            }

            catch (Exception ex)
            {
                string exMessage = ex.Message;

                return false;
            }
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

        public static bool StoreSponsorData(SponsorModel sponsor, ref bool edit)
        {
            int UserId = WebSecurity.GetUserId(sponsor.EmailAddress);

            edit = false;

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
                        CurrentSponsor.Reason = sponsor.Reason;
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
                        CurrentSponsor.Reason = sponsor.Reason;

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
                    catch (Exception e)
                    {
                        string errorMessage = e.Message;

                        return false;
                    }

                }
            }

            catch (Exception ex)
            {
                string exMessage = ex.Message;

                return false;
            }
        }

        public static bool RemoveSponsorData(int UserId)
        {
            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var SponsorData = from r in context.ProspectiveCorporateSponsor
                                      where r.SponsorId == UserId
                                      select r;

                    if (SponsorData.Count() > 0 && UserId > 0)
                    {
                        context.DeleteObject(SponsorData.FirstOrDefault());
                    }

                    else
                    {
                        return false;
                    }

                    try
                    {
                        context.SaveChanges();

                        return true;
                    }

                    catch (Exception e)
                    {
                        return false;
                    }

                }
            }

            catch (Exception ex)
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

        public static bool StoreAcademicData(AcademicModel academic, ref bool edit)
        {
            int UserId = WebSecurity.CurrentUserId;

            edit = false;

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
                    catch (Exception ex)
                    {
                        if (ex is SqlException)
                        {
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

        public static bool RemoveAcademicData(int UserId)
        {
            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var SponsorData = from r in context.ProspectiveAcademic
                                      where r.AcademicId == UserId
                                      select r;

                    if (SponsorData.Count() > 0 && UserId > 0)
                    {
                        context.DeleteObject(SponsorData.FirstOrDefault());
                    }

                    else
                    {
                        return false;
                    }

                    try
                    {
                        context.SaveChanges();

                        return true;
                    }

                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                string exMessage = ex.Message;

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
                        prospect.ResumeUploaded = ExistingProspect.FirstOrDefault().ResumeUploaded;
                        prospect.TranscriptUploaded = ExistingProspect.FirstOrDefault().TranscriptUploaded;

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

        public static bool StoreProspectData(ProspectModel prospect, ref bool edit)
        {
            int UserId = WebSecurity.CurrentUserId;

            ProspectiveStudent CurrentStudent;

            edit = false;

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

                                DatabaseHelper.UploadFile(Resume, prospect);

                                CurrentStudent.ResumeUploaded = "Yes";
                                prospect.ResumeUploaded = "Yes";
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

                                DatabaseHelper.UploadTranscript(Transcript, prospect);

                                CurrentStudent.TranscriptUploaded = "Yes";
                                prospect.TranscriptUploaded = "Yes";
                            }
                        }

                    }
                    else
                    {
                        CurrentStudent = new ProspectiveStudent();

                        CurrentStudent.Status = (int)StudentStatus.Initial;
                        CurrentStudent.Name = prospect.Name;
                        CurrentStudent.Telephone = prospect.Telephone;
                        CurrentStudent.EmailAddress = prospect.EmailAddress;
                        CurrentStudent.DesiredCareerPath = prospect.DesiredCareerPath;
                        CurrentStudent.Gender = prospect.Gender;

                        context.AddToProspectiveStudent(CurrentStudent);
                    }

                    try
                    {
                        if (edit == false)
                        {
                            WebSecurity.CreateUserAndAccount(prospect.EmailAddress, prospect.Password);
                            WebSecurity.Login(prospect.EmailAddress, prospect.Password);

                            DatabaseHelper.AddUserToRole(prospect.EmailAddress, "Student");

                            CurrentStudent.UserId = WebSecurity.GetUserId(prospect.EmailAddress);

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

                                    DatabaseHelper.UploadFile(Resume, prospect);

                                    CurrentStudent.ResumeUploaded = "Yes";
                                    prospect.ResumeUploaded = "Yes";
                                }
                            }

                            else
                            {
                                CurrentStudent.ResumeUploaded = "No";
                                prospect.ResumeUploaded = "No";
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

                                    DatabaseHelper.UploadTranscript(Transcript, prospect);

                                    CurrentStudent.TranscriptUploaded = "Yes";
                                    prospect.TranscriptUploaded = "Yes";
                                }
                            }
                            else
                            {
                                CurrentStudent.TranscriptUploaded = "No";
                                prospect.TranscriptUploaded = "No";
                            }
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

        public static bool RemoveProspectiveData(int UserId)
        {
            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var SponsorData = from r in context.ProspectiveStudent
                                      where r.UserId == UserId
                                      select r;

                    if (SponsorData.Count() > 0 && UserId > 0)
                    {
                        context.DeleteObject(SponsorData.FirstOrDefault());
                    }

                    else
                    {
                        return false;
                    }

                    try
                    {
                        context.SaveChanges();

                        return true;
                    }

                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                string exMessage = ex.Message;

                return false;
            }
        }

        public static bool UploadTranscript(ProspectiveStudentTranscript f, ProspectModel prospect)
        {

            int UserId = WebSecurity.GetUserId(prospect.EmailAddress);

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

        public static bool RemoveTranscript(int UserId)
        {
            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var UserTranscript = from r in context.ProspectiveStudentTranscripts
                                         where r.UserId == UserId
                                         select r;

                    if (UserTranscript.Count() > 0 && UserId > 0)
                    {
                        context.DeleteObject(UserTranscript.FirstOrDefault());
                    }

                    else
                    {
                        return false;
                    }

                    try
                    {
                        context.SaveChanges();

                        return true;
                    }

                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                string exMessage = ex.Message;

                return false;
            }
        }

        public static ProspectiveStudentTranscript GetTranscript(int UserId)
        {
            ProspectiveStudentTranscript f = new ProspectiveStudentTranscript();

            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {

                    var transcript = from r in context.ProspectiveStudentTranscripts
                                     where r.UserId == UserId
                                     select r;

                    if (transcript.Count() > 0)
                    {
                        return (transcript.FirstOrDefault());
                    }
                    else
                    {
                        return null;
                    }

                }
            }

            catch
            {
                return (null);
            }

        }

        public static bool UploadFile(ProspectiveStudentResume f, ProspectModel prospect)
        {
            int UserId = WebSecurity.GetUserId(prospect.EmailAddress);

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

        public static bool RemoveFile(int UserId)
        {
            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var UserResume = from r in context.ProspectiveStudentResumes
                                     where r.UserId == UserId
                                     select r;

                    if (UserResume.Count() > 0 && UserId > 0)
                    {
                        context.DeleteObject(UserResume.FirstOrDefault());
                    }

                    else
                    {
                        return false;
                    }

                    try
                    {
                        context.SaveChanges();

                        return true;
                    }

                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                string exMessage = ex.Message;

                return false;
            }
        }

        public static ProspectiveStudentResume GetResume(int UserId)
        {
            ProspectiveStudentResume f = new ProspectiveStudentResume();

            try
            {
                using (ITintheDTestTableEntities context = new ITintheDTestTableEntities())
                {
                    var resume = from r in context.ProspectiveStudentResumes
                                 where r.UserId == UserId
                                 select r;

                    if (resume.Count() > 0)
                    {
                        //return File(resume.FirstOrDefault().FileContent, resume.FirstOrDefault().ContentType);
                        return (resume.FirstOrDefault());
                    }
                    else
                    {
                        return null;
                    }

                }
            }

            catch
            {
                return (null);
            }
        }
    }

}