using Android.App;
using Android.Widget;

namespace Vemini.Droid
{
    public class Toast_Android : Toast
    {
        public void LongAlert(string message)
        {
            Android.Widget.Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
        public void ShortAlert(string message)
        {
            Android.Widget.Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}