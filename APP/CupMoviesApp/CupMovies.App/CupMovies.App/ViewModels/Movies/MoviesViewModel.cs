using CupMovies.App.Models;
using CupMovies.App.Services.Interfaces;
using CupMovies.App.ViewModels.Base;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CupMovies.App.ViewModels
{
    public class MoviesViewModel : BaseViewModel
    {
        private bool isResult;
        public bool IsResult
        {
            get { return this.isResult; }
            set
            {
                this.isResult = value;
                OnPropertyChanged(nameof(IsResult));
            }
        }

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

        private MovieModel selectedMovie;
        public MovieModel SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                this.selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
                if (this.MoviesSelected.Count < 8)
                {
                    var movie = this.MoviesSelected.Where(m => m.Id == value.Id).FirstOrDefault();
                    if (movie == null)
                    {
                        App.Current.MainPage.DisplayAlert("Movies",
                                                       $"Deseja realmente adicionar o filme {value.Titulo}?",
                                                       "Sim",
                                                       "Não").ContinueWith((arg) =>
                               {
                                   if (arg.Result)
                                   {
                                       this.MoviesSelected.Add(value);
                                   }
                               });
                    }
                    
                    if (movie != null)
                        App.Current.MainPage.DisplayAlert("Movies", "Filme já selecionado!", "Ok");
                }

                if (this.MoviesSelected.Count >= 8)
                    App.Current.MainPage.DisplayAlert("Movies", "Você já possui 8 filmes selecionados!", "Ok");

                this.CountSelectedMovies = this.MoviesSelected.Count;
                OnPropertyChanged(nameof(CountSelectedMovies));
                this.IsResult = this.MoviesSelected.Count.Equals(8)? true : false;
            }
        }

        public ICommand ResultCupCommand
        {
            get
            {
                return new Command(() =>
                {
                    GetResultMovies();
                });
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                return new Command(() =>
                {
                    this.CountSelectedMovies = 0;
                    this.MoviesSelected = new MovieCollectionModel();
                    App.ResultFinalCup = new MovieCollectionModel();
                    this.IsResult = false;
                });
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
            this.IsResult = false;
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

            if (this.Movies.Error)
            {
                this.IsLoading = false;
                await App.Current.MainPage.DisplayAlert("Movies", this.Movies.Message, "Ok");
                return;
            }

            this.CountTotalMovies = this.Movies.Count;
            this.IsLoading = false;
        }

        private async void GetResultMovies()
        {
            App.ResultFinalCup = this.MoviesSelected;
            await Application.Current.MainPage.Navigation.PushAsync(new Views.Movies.ResultCupMoviesView());
        }

        public void DeleteMovieSelected(MovieModel movie)
        {
            this.MoviesSelected.Remove(movie);
            this.CountSelectedMovies = this.MoviesSelected.Count;
        }
    }
}