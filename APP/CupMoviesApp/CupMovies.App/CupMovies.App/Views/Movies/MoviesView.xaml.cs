using CupMovies.App.Models;
using CupMovies.App.ViewModels;
using System;
using System.Linq;
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
            var movie = (MovieModel)mi.CommandParameter;

            var _movie = ((MoviesViewModel)BindingContext).MoviesSelected.Where(m => m.Id == movie.Id).FirstOrDefault();

            if (_movie != null)
            {
                App.Current.MainPage.DisplayAlert("Movies",
                                   $"Deseja realmente excluir o filme {movie.Titulo} da lista?",
                                   "Sim",
                                   "Não").ContinueWith((arg) =>
                                   {
                                       if (arg.Result)
                                       {
                                           ((MoviesViewModel)BindingContext).DeleteMovieSelected(movie);
                                       }
                                   });
            }

            if (_movie == null)
                App.Current.MainPage.DisplayAlert("Movies", "Este filme não foi selecionado!", "Ok");
        }
    }
}