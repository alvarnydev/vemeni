using System;
using System.Collections.Generic;
using System.Text;

namespace Vemini
{
    public static class Constants
    {
        public static string AppName = "Vemini";

        // Google OAuth

        // For Google login, configure at https://console.developers.google.com/
        public static string GoogleiOSClientId = "1074178032762-3o49v1gson963nudv7k4onoj1neuln3h.apps.googleusercontent.com";
        public static string GoogleAndroidClientId = "1074178032762-37ega4jph0edpbfg45uph0t94nmqho26.apps.googleusercontent.com";

        // These values do not need changing
        public static string GoogleScope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
        public static string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string GoogleiOSRedirectUrl = "com.googleusercontent.apps.1074178032762-3o49v1gson963nudv7k4onoj1neuln3h:/oauth2redirect";
        public static string GoogleAndroidRedirectUrl = "com.googleusercontent.apps.1074178032762-37ega4jph0edpbfg45uph0t94nmqho26:/oauth2redirect";



        //-------------------------------------------------------------------------------------------------------
        // Facebook OAuth

        // For Facebook login, configure at https://developers.facebook.com
        public static string FacebookiOSClientId = "<insert IOS client ID here>";
        public static string FacebookAndroidClientId = "636575123871898";

        // These values do not need changing
        public static string FacebookScope = "email";
        public static string FacebookAuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
        public static string FacebookAccessTokenUrl = "https://www.facebook.com/connect/login_success.html";
        public static string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=email&access_token={accessToken}";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string FacebookiOSRedirectUrl = "<insert IOS redirect URL here>:/oauth2redirect";
        public static string FacebookAndroidRedirectUrl = "https://www.facebook.com/connect/login_success.html";
    }
}
