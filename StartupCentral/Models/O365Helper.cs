using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartupCentral.Models
{
    public class O365Helper
    {
        public static string ClientId => ConfigurationManager.AppSettings["ClientID"];
        public static string ClientSecret => ConfigurationManager.AppSettings["ClientSecret"];

        public static string AzureADAuthority = @"https://login.microsoftonline.com/common";
        public static string LogoutAuthority = @"https://login.microsoftonline.com/common/oauth2/logout?post_logout_redirect_uri=";
        public static string O365UnifiedAPIResource = @"https://graph.microsoft.com/";

        public static string SendMessageUrl = @"https://graph.microsoft.com/v1.0/me/microsoft.graph.sendmail";
        public static string GetMeUrl = @"https://graph.microsoft.com/v1.0/me";

    }
}