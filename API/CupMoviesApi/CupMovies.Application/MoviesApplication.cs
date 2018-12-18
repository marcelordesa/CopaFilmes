using CupMovies.Domain.Contracts.Applications;
using CupMovies.Domain.Contracts.Infrastructure;
using CupMovies.Domain.Entities;
using CupMovies.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CupMovies.Application
{
    public class MoviesApplication : IApplication
    {
        private IContext Context;

        private Movie TieBreaker(MovieCollection movies)
        {
            movies.OrderBy();
            return movies.First();
        }

        private Movie GetRoundSeller(MovieCollection moviesRound)
        {
            moviesRound.OrderByDescendingNote();
            var movieFirst = moviesRound[0];
            var movieSecund = moviesRound[1];

            if (movieFirst.Nota == movieSecund.Nota)
                return TieBreaker(moviesRound);

            return movieFirst;
        }

        private MovieCollection GetFinalSeller(MovieCollection moviesRound)
        {
            moviesRound.OrderByDescendingNote();
            var movieFirst = moviesRound[0];
            var movieSecund = moviesRound[1];

            if (movieFirst.Nota == movieSecund.Nota)
            {
                moviesRound.OrderBy();
                return moviesRound;
            }

            return moviesRound;
        }

        private MovieCollection FirstStep(MovieCollection movies)
        {
            var moviesSecondStep = new MovieCollection();

            for(var i = 0; i < movies.Count/2; i++)
            {
                var moviesRound = new MovieCollection();
                var movieFirst = movies[i];
                moviesRound.Add(movieFirst);
                var movieLast = movies[movies.Count - (i + 1)];
                moviesRound.Add(movieLast);
                var winningMovie = GetRoundSeller(moviesRound);
                moviesSecondStep.Add(winningMovie);
            }

            return moviesSecondStep;
        }

        private MovieCollection SecondStep(MovieCollection movies)
        {
            var moviesFinalStep = new MovieCollection();
            var firstRound = new MovieCollection();
            var secondRound = new MovieCollection();
            firstRound.Add(movies[0]);
            firstRound.Add(movies[1]);
            secondRound.Add(movies[2]);
            secondRound.Add(movies[3]);
            var resultFirst = GetRoundSeller(firstRound);
            moviesFinalStep.Add(resultFirst);
            var resultSecond = GetRoundSeller(secondRound);
            moviesFinalStep.Add(resultSecond);

            return moviesFinalStep;
        }

        public async Task<MovieCollection> GetMovies()
        {
            try
            {
                this.Context = new MoviesContext();
                return await this.Context.GetMovies();
            }
            catch(Exception ex)
            {
                var movies = new MovieCollection();
                movies.Error = true;
                movies.Message = ex.Message;
                return movies;
            }
        }

        public MovieCollection GetResultCupMovies(MovieCollection movies)
        {
            try
            {
                movies.SortMoviesSelected();
                if (movies.Error)
                    return movies;

                var moviesSecondStep = FirstStep(movies);
                var moviesFinalStep = SecondStep(moviesSecondStep);

                return GetFinalSeller(moviesFinalStep);
            }
            catch(Exception ex)
            {
                var moviesException = new MovieCollection();
                moviesException.Error = true;
                moviesException.Message = ex.Message;
                return moviesException;
            }
        }
    }
}