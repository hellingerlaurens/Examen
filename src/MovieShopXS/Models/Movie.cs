using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopXS.Models
{
    public class Movie
    {
        public Movie()
        {
            MovieActor = new HashSet<MovieActor>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public byte Stars { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MovieActor> MovieActor { get; set; }
        public virtual Person Director { get; set; }
        public virtual Genre Genre { get; set; }
    }

    public partial class Person
    {
        public Person()
        {
            Movie = new HashSet<Movie>();
            MovieActor = new HashSet<MovieActor>();
        }

        public int PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<Movie> Movie { get; set; }
        public virtual ICollection<MovieActor> MovieActor { get; set; }
    }

    public partial class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public virtual Person Actor { get; set; }
        public virtual Movie Movie { get; set; }
    }
    public partial class Genre
    {
        public Genre()
        {
            Movie = new HashSet<Movie>();
        }

        public int GenreId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Movie> Movie { get; set; }
    }

}
