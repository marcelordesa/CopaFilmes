using CupMovies.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CupMovies.App.Views.Movies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultCupMoviesView : ContentPage
	{
		public ResultCupMoviesView ()
		{
			InitializeComponent ();
            this.BindingContext = new ResultCupMoviesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((ResultCupMoviesViewModel)BindingContext).GetResultMovies();
        }
    }
}