using CupMovies.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CupMovies.App.Views.Movies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoviesView : ContentPage
	{
		public MoviesView ()
		{
			InitializeComponent ();
            this.BindingContext = new MoviesViewModel();
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
        }
    }
}