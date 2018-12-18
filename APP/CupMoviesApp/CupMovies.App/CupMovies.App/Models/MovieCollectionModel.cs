using System.Collections.Generic;

namespace CupMovies.App.Models
{
    public class MovieCollectionModel : List<MovieModel>
    {
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}