﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vemini.AppNavigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ErrandView : ContentPage
	{
		public ErrandView ()
		{
			InitializeComponent ();
            errand_listview.ItemsSource = Enumerable.Range(0, 5).Select(n => $"item-{n}");
        }

        private async void MenuItem_OnClickedPlus(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateErrand());
        }
    }
}