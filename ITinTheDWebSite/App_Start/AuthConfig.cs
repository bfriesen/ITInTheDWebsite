using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using ITinTheDWebSite.Models;

namespace ITinTheDWebSite
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

           // OAuthWebSecurity.RegisterMicrosoftClient(
             //   clientId: "",
               // clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(
             consumerKey: "axQTsvuVpt0Ohg4DjIlw",
               consumerSecret: "Lmet9tOamYYd0VRoLScxHKGza5CK7Rwcn0KwTrMYBgg");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "496184330412486",
                appSecret: "cd7b3eb20af8bd204d469a76388e7599");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
