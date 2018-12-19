using CupMovies.App.Services.Interfaces;
using CupMovies.App.ViewModels.Base;

namespace CupMovies.App.ViewModels
{
    public class ResultCupMoviesViewModel : BaseViewModel
    {
        private string firstMovie;
        public string FirstMovie
        {
            get { return firstMovie; }
            set
            {
                this.firstMovie = value;
                OnPropertyChanged(nameof(FirstMovie));
            }
        }

        private string secondMovie;
        public string SecondMovie
        {
            get { return secondMovie; }
            set
            {
                this.secondMovie = value;
                OnPropertyChanged(nameof(SecondMovie));
            }
        }

        public readonly IMovieService MovieService;

        public ResultCupMoviesViewModel()
        {
            this.FirstMovie = string.Empty;
            this.SecondMovie = string.Empty;
            this.MovieService = Xamarin.Forms.DependencyService.Get<IMovieService>();
        }

        public async void GetResultMovies()
        {
            this.IsLoading = true;
            var movies = await this.MovieService.PostMovies(App.ResultFinalCup);

            if (movies.Error)
            {
                this.IsLoading = false;
                await App.Current.MainPage.DisplayAlert("Movies", movies.Message, "Ok");
                return;
            }

            this.FirstMovie = movies[0].Titulo;
            this.SecondMovie = movies[1].Titulo;
            this.IsLoading = false;
        }
    }
}
