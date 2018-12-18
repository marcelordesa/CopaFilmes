using System.Collections.Generic;
using System.Linq;

namespace CupMovies.Domain.Entities
{
    public class MovieCollection : List<Movie>
    {
        public bool Error { get; set; }
        public string Message { get; set; }

        public new void Add(Movie movie)
        {
            if (!this.Contains(movie))
                base.Add(movie);
        }

        public new bool Contains(Movie movie)
        {
            foreach (var c in this)
            {
                if (c.Id == movie.Id)
                    return true;
            }
            return false;
        }

        public bool Remove(string id)
        {
            foreach (var movie in this)
            {
                if (movie.Id == id)
                    return this.Remove(movie);
            }
            return false;
        }

        public bool Exists(string id)
        {
            foreach (var movie in this)
            {
                if (movie.Id == id)
                    return true;
            }
            return false;
        }

        public void AddRange(List<Movie> collection)
        {
            foreach (var movie in collection)
            {
                if (!this.Exists(movie.Id))
                    this.Add(movie);
            }
        }

        public void OrderBy()
        {
            var movies = this.ToList();
            movies = movies.OrderBy(c => c.Titulo).ToList();
            this.Clear();
            this.AddRange(movies);
        }

        public void OrderByDescendingNote()
        {
            var movies = this.ToList();
            movies = movies.OrderByDescending(c => c.Nota).ToList();
            this.Clear();
            this.AddRange(movies);
        }

        public void SortMoviesSelected()
        {
            if (this.Count != 8)
            {
                this.Error = true;
                this.Message = "São permitidos 8 filmes para a copa!";
                return;
            }

            this.OrderBy();
        }
    }
}