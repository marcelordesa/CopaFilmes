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
    [ApiController]
    public class MoviesController : ControllerBase
    {
        IApplication application = ApplicationFactory.GetApplicationInstance<MoviesApplication>();

        [Route("api/Movies")]
        [HttpGet]
        public async Task<MovieCollection> Get()
        {
            return await application.GetMovies();
        }

        [Route("api/Movies/PostMovies")]
        [HttpPost]
        public ActionResult<MovieCollection> PostMovies([FromBody] MovieCollection movies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return application.GetResultCupMovies(movies);
        }
    }
}