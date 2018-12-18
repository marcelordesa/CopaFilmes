using CupMovies.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CupMovies.App.Services.Interfaces
{
    public interface IMovieService
    {
        Task<MovieCollectionModel> GetMovies();
        Task<MovieCollectionModel> PostMovies(MovieCollectionModel movies);
    }
}