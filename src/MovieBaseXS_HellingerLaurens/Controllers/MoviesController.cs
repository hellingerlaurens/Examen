using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBaseXS_HellingerLaurens.Entities;
using MovieBaseXS_HellingerLaurens.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieBaseXS_HellingerLaurens.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private MovieBaseContext db;

        public MoviesController(MovieBaseContext db)
        {
            this.db = db;
        }

        // GET: api/movies
        [HttpGet(Name = "GetMovies")]
        public IActionResult Get()
        {
            try
            {
                List<Movie> movies = db.Movie.Include(m => m.Director).ToList();
                return Ok(movies);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int movieId)
        {
            try
            {
                Movie oldMovie = db.Movie.FirstOrDefault(m => m.MovieId == movieId);
                if (oldMovie == null)
                {
                    return NotFound($"No movie found with id {movieId}");
                }
                else
                {
                    db.Movie.Remove(oldMovie);
                    db.SaveChanges();
                }
                return Ok("movie deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] MovieItemVM movie)
        {
            try
            {
                if (movie == null)
                {
                    return NotFound("no movie found");
                }
                else
                {
                    Movie newMovie = new Movie()
                    {
                        Description = movie.Description,
                        Director = db.Person.FirstOrDefault(p => p.LastName.Equals("Diesel")),
                        Year = movie.Year,
                        Title = movie.Title,
                        Stars = movie.Rating,
                        Genre = db.Genre.FirstOrDefault(g => g.GenreId == 1)
                    };
                    db.Movie.Add(newMovie);
                    db.SaveChanges();
                    //return Created(Url.Link("GetMovies"), new {id = newMovie.MovieId}), newMovie);
                    return Ok("movie added");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
