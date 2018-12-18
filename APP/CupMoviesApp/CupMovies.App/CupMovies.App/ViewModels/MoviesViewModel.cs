using CupMovies.App.Models;
using CupMovies.App.Services.Interfaces;
using CupMovies.App.ViewModels.Base;

namespace CupMovies.App.ViewModels
{
    public class MoviesViewModel : BaseViewModel
    {
        private MovieCollectionModel movies;
        public MovieCollectionModel Movies
        {
            get { return this.movies; }
            set
            {
                this.movies = value;
                OnPropertyChanged(nameof(Movies));
            }
        }

        private MovieCollectionModel moviesSelected;
        public MovieCollectionModel MoviesSelected
        {
            get { return this.moviesSelected; }
            set
            {
                this.moviesSelected = value;
                OnPropertyChanged(nameof(MoviesSelected));
            }
        }

        private int countSelectedMovies;
        public int CountSelectedMovies
        {
            get { return this.countSelectedMovies; }
            set
            {
                this.countSelectedMovies = value;
                OnPropertyChanged(nameof(CountSelectedMovies));
            }
        }

        private int countTotalMovies;
        public int CountTotalMovies
        {
            get { return this.countTotalMovies; }
            set
            {
                this.countTotalMovies = value;
                OnPropertyChanged(nameof(CountTotalMovies));
            }
        }

        public readonly IMovieService MovieService;

        public MoviesViewModel()
        {
            this.IsLoading = false;
            this.MovieService = Xamarin.Forms.DependencyService.Get<IMovieService>();
            this.CountSelectedMovies = 0;
            this.countTotalMovies = 0;
            this.Movies = new MovieCollectionModel();
            this.MoviesSelected = new MovieCollectionModel();
            this.GetMovies();
        }

        private async void GetMovies()
        {
            this.IsLoading = true;
            this.Movies = await this.MovieService.GetMovies();
            this.CountTotalMovies = this.Movies.Count;
            this.IsLoading = false;
        }
    }
}