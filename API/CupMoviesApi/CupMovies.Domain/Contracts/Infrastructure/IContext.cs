using CupMovies.Domain.Entities;
using System.Threading.Tasks;

namespace CupMovies.Domain.Contracts.Infrastructure
{
    public interface IContext
    {
        Task<MovieCollection> GetMovies();
    }
}