using CupMovies.Application;
using CupMovies.Domain.Contracts.Applications;
using CupMovies.Domain.Entities;
using CupMovies.Factory;
using System.Linq;
using Xunit;

namespace CupMovies.Tests
{
    public class MoviesTest
    {
        IApplication application = ApplicationFactory.GetApplicationInstance<MoviesApplication>();

        [Fact]
        public async void TestGetMovies()
        {
            var movies = await application.GetMovies();
            Assert.NotEmpty(movies);
        }

        [Fact]
        public async void TestGetResultCupMovies()
        {
            var moviesAll = await application.GetMovies();
            var movies = new MovieCollection {
                new Movie{
                    Id = "tt3606756",
                    Titulo = "Os Incríveis 2",
                    Ano = 2018,
                    Nota = 8.5
                },
                new Movie {
                     Id = "tt4881806",
                    Titulo = "Jurassic World: Reino Ameaçado",
                    Ano = 2018,
                    Nota = 6.7
                },
                new Movie{
                    Id = "tt5164214",
                    Titulo = "Oito Mulheres e um Segredo",
                    Ano = 2018,
                    Nota = 6.3
                },
                new Movie {
                    Id = "tt7784604",
                    Titulo = "Hereditário",
                    Ano = 2018,
                    Nota = 7.8
                },
                new Movie {
                    Id = "tt4154756",
                    Titulo = "Vingadores: Guerra Infinita",
                    Ano = 2018,
                    Nota = 8.8
                },
                new Movie {
                    Id = "tt5463162",
                    Titulo = "Deadpool 2",
                    Ano = 2018,
                    Nota = 8.1
                },
                new Movie {
                    Id = "tt3778644",
                    Titulo = "Han Solo: Uma História Star Wars",
                    Ano = 2018,
                    Nota = 7.2
                },
                new Movie {
                    Id = "tt3501632",
                    Titulo = "Thor: Ragnarok",
                    Ano = 2017,
                    Nota = 7.9
                }
            };

            var movieTest = moviesAll.Where(m => m.Id == "tt4154756").FirstOrDefault();
            var moviesResult = application.GetResultCupMovies(movies);
            var movieChampion = moviesResult.FirstOrDefault();
            Assert.Equal(movieChampion.Id, movieTest.Id);
        }
    }
}