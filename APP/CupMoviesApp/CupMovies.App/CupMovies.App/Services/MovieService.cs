using CupMovies.App.Models;
using CupMovies.App.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CupMovies.App.Services
{
    public class MovieService : IMovieService
    {
        public async Task<MovieCollectionModel> GetMovies()
        {
            HttpClient client;
            MovieCollectionModel movies = new MovieCollectionModel();

            try
            {
                client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;

                HttpResponseMessage response = null;
                var uri = new Uri(App.UrlService + "movies/");
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<MovieCollectionModel>(content);
                }
            }
            catch (Exception ex)
            {
                movies.Error = true;
                movies.Message = ex.Message.ToString();
            }

            return movies;
        }

        public async Task<MovieCollectionModel> PostMovies(MovieCollectionModel movies)
        {
            HttpClient client;
            MovieCollectionModel moviesResult = null;

            try
            {
                client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;

                var uri = new Uri(string.Format(App.UrlService + "PostMovies"));

                var json = JsonConvert.SerializeObject(movies);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    moviesResult = new MovieCollectionModel();
                    var result = await response.Content.ReadAsStringAsync();
                    moviesResult = JsonConvert.DeserializeObject<MovieCollectionModel>(result);
                }
            }
            catch (Exception ex)
            {
                moviesResult = new MovieCollectionModel{
                    Error = true,
                    Message = ex.Message
                };
            }

            return moviesResult;
        }
    }
}