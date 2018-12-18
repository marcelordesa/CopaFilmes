using CupMovies.Domain.Contracts.Infrastructure;
using CupMovies.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CupMovies.Infrastructure
{
    public class MoviesContext : IContext
    {
        public async Task<MovieCollection> GetMovies()
        {
            HttpClient client;
            MovieCollection movies = null;

            try
            {
                client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;

                HttpResponseMessage response = null;
                var uri = new Uri("https://copadosfilmes.azurewebsites.net/api/filmes");
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<MovieCollection>(content);
                }
            }
            catch (Exception ex)
            {
                movies.Error = true;
                movies.Message = ex.Message.ToString();
            }

            return movies;
        }
    }
}