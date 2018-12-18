using CupMovies.Domain.Entities;
using System.Threading.Tasks;

namespace CupMovies.Domain.Contracts.Applications
{
    public interface IApplication
    {
        Task<MovieCollection> GetMovies();
        MovieCollection GetResultCupMovies(MovieCollection movies);
    }
}