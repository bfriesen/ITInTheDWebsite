using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITinTheDWebSite.DataMigrations
{
    [Migration(1)]
    public class InitialSchemaMigration : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("InitialSchema.sql");
        }

        public override void Down()
        {
            Delete.Table("__MigrationHistory");
            Delete.Table("14_Student");
            Delete.Table("14_Student_EducationLevel");
            Delete.Table("15_4_UserInfo");
            Delete.Table("AzzamAzizTable");
            Delete.Table("Faq");
            Delete.Table("ProspectiveAcademic");
            Delete.Table("ProspectiveCorporateSponsor");
            Delete.Table("ProspectiveStudent");
            Delete.Table("ProspectiveStudentResume");
            Delete.Table("ProspectiveStudentTranscript");
            Delete.Table("SiteAdmin");
            Delete.Table("UserImage");
            Delete.Table("webpages_Membership");
            Delete.Table("webpages_OAuthMembership");
            Delete.Table("webpages_UsersInRoles");
            Delete.Table("webpages_Roles");
            Delete.Table("UserProfile");
        }
    }
}
