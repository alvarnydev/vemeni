using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vemini
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginProviderPage : ContentPage
	{
        public string ProviderName
        {
            get;
            set;
        }
        public LoginProviderPage()
        {
            InitializeComponent();
           
        }
    }
}