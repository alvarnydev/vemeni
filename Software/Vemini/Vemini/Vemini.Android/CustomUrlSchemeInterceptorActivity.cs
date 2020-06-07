using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Content.PM;
using Vemin;


namespace Vemini.Droid
{
    [Activity(Label = "CustomUrlSchemeInterceptorActivity", 
     NoHistory = true, LaunchMode = LaunchMode.SingleTop)]

    [IntentFilter(
        new[] { Intent.ActionView },

        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataSchemes = new[] { "com.googleusercontent.apps.1074178032762-37ega4jph0edpbfg45uph0t94nmqho26" },

        DataPath = "/oauth2redirect")]

    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Convert Android.Net.Url to Uri
            var uri = new Uri(Intent.Data.ToString());

            // Load redirectUrl page
            AuthenticationState.Authenticator.OnPageLoading(uri);

            var intent = new Intent(this, typeof(MainActivity));

            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);

            StartActivity(intent);
            this.Finish();

            return;
        }
    }
}