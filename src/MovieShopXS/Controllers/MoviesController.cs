using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Cli.Utils;
using MovieShopXS.Models;
using MovieShopXS.ViewModels;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopXS.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class MoviesController : Controller
    {
        private HttpClient client;

        public MoviesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2018");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            HttpResponseMessage response = client.GetAsync("/api/movies").Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<Movie> data = JsonConvert.DeserializeObject<List<Movie>>(stringData);
            List<MovieItemVM> movies = data.Select(m => new MovieItemVM()
            {
                Actors = m.MovieActor.Select(a => new MovieActorVM() {Name = a.Actor.FirstName + " " + a.Actor.LastName}).ToList(),
                Description = m.Description,
                Director = m.Director.FirstName + " " + m.Director.LastName,
                Id = m.MovieId,
                Rating = m.Stars,
                Title = m.Title,
                Year = m.Year
            }).ToList();
            return View(movies);
        }

        [Route("[action]/{id}/")]
        public IActionResult Delete(int movieId)
        {

            string stringData = JsonConvert.SerializeObject(movieId);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");


            client.DeleteAsync("api/movies?id=" + movieId);
            //return Content(response.Content.ReadAsStringAsync().Result.ToString());
            return Redirect("Index");

        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult AddMovie()
        {
            return View(new MovieItemVM());
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult AddMovie(MovieItemVM movieItem)
        {
            if (ModelState.IsValid)
            {
                string stringData = JsonConvert.SerializeObject(movieItem);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");


                HttpResponseMessage response = client.PostAsync("/api/movies", contentData).Result;
                var content = Content(response.Content.ReadAsStringAsync().Result.ToString());

                //Call api with productvm
                return RedirectToAction("Index");
            }
            return View(movieItem);
        }
    }
}
