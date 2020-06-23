using System;
using System.Collections.Generic;
using System.Text;

/*
 * Author: Benedikt Blank with the help of https://github.com/CuriousDrive/PublicProjects/tree/master/OAuthNativeFlow
 * Implemented: 07.06.20
 * Constants for connecting and communicating with the google and facebook api 
 */
namespace Vemini
{
    public static class Constants
    {
        
        public static string AppName = "Vemini";

        //Database Constants
        public const string VeminiUrl = "https://vergissmeinnicht.f2.htw-berlin.de/";
        public const string VeminiJobsUrl = "https://vergissmeinnicht.f2.htw-berlin.de/api/jobs/";
        public const string VeminiUsersUrl = "https://vergissmeinnicht.f2.htw-berlin.de/api/users/";

        // Google OAuth

        // For Google login
        public static string GoogleiOSClientId = "1074178032762-3o49v1gson963nudv7k4onoj1neuln3h.apps.googleusercontent.com";
        public static string GoogleAndroidClientId = "1074178032762-37ega4jph0edpbfg45uph0t94nmqho26.apps.googleusercontent.com";

        // Google Constants
        public static string GoogleScope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
        public static string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Where the User gets redirected to after loggin in
        public static string GoogleiOSRedirectUrl = "com.googleusercontent.apps.1074178032762-3o49v1gson963nudv7k4onoj1neuln3h:/oauth2redirect";
        public static string GoogleAndroidRedirectUrl = "com.googleusercontent.apps.1074178032762-37ega4jph0edpbfg45uph0t94nmqho26:/oauth2redirect";



        //-------------------------------------------------------------------------------------------------------
        // Facebook OAuth

        // For Facebook login
        public static string FacebookiOSClientId = "<insert IOS client ID here>";
        public static string FacebookAndroidClientId = "636575123871898";

        // Facebook Constants
        public static string FacebookScope = "email";
        public static string FacebookAuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
        public static string FacebookAccessTokenUrl = "https://www.facebook.com/connect/login_success.html";
        public static string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=email&access_token={accessToken}";

        // Where  User gets redirected to after loggin in 
        public static string FacebookiOSRedirectUrl = "<insert IOS redirect URL here>:/oauth2redirect";
        public static string FacebookAndroidRedirectUrl = "636575123871898://authorize";
    }
}
