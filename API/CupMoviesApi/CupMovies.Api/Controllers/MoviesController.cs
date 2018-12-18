using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CupMovies.Application;
using CupMovies.Domain.Contracts.Applications;
using CupMovies.Domain.Entities;
using CupMovies.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupMovies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        IApplication application = ApplicationFactory.GetApplicationInstance<MoviesApplication>();

        [HttpGet]
        public async Task<MovieCollection> Get()
        {
            return await application.GetMovies();
        }

        [HttpPost]
        public MovieCollection PostMovies([FromBody] MovieCollection movies)
        {
            return application.GetResultCupMovies(movies);
        }
    }
}