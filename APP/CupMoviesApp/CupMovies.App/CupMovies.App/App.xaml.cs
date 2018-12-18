using CupMovies.App.Services;
using CupMovies.App.Views.Movies;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CupMovies.App
{
    public partial class App : Application
    {
        public const string UrlService = "http://localhost:54617/api/";

        public App()
        {
            InitializeComponent();
            DependencyService.Register<Services.Interfaces.IMovieService, MovieService>();
            MainPage = new MoviesView();
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
    }
}
