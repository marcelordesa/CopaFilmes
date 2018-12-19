using CupMovies.App.Models;
using CupMovies.App.Services;
using CupMovies.App.Views.Movies;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CupMovies.App
{
    public partial class App : Application
    {
        public const string UrlService = "http://192.168.0.58:45455/api/";

        public App()
        {
            InitializeComponent();
            DependencyService.Register<Services.Interfaces.IMovieService, MovieService>();
            MainPage = new NavigationPage(new MoviesView());
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
        }

        public static MovieCollectionModel ResultFinalCup
        {
            get
            {
                if (App.Current.Properties.ContainsKey("ResultFinalCup"))
                {
                    return (MovieCollectionModel)App.Current.Properties["ResultFinalCup"];
                }
                return new MovieCollectionModel();
            }
            set
            {
                App.Current.Properties["ResultFinalCup"] = value;
            }
        }
    }
}
