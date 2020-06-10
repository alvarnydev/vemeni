using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Vemini
{
    public partial class App : Application
    {
        //Starting Point
        public App()
        {
            InitializeComponent();

            //Navigation kind of page
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.CornflowerBlue
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            base.OnResume();

           // Xamarin.Essentials.Platform.OnResume();
        }
    }
}
