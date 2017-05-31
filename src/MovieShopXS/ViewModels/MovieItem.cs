using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieShopXS.Models;

namespace MovieShopXS.ViewModels
{
    public enum RatingAction
    {
        Up,
        Down
    }

    public class MovieItemVM
    {
        [Required(ErrorMessage = "Id required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description required")]
        [Display(Name = "What happens?")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Jaar moet tussen 1900 en 2017 liggen")]
        [Range(1900, 2017)]
        [DefaultValue(2017)]
        public int Year { get; set; }


        [Required(ErrorMessage = "Rating moet tussen 0 en 5 liggen")]
        [Display(Name = "How much is this rated?")]
        [Range(0, 5)]
        public byte Rating { get; set; }

        [Display(Name = "The Director")]
        public string Director { get; set; }

        public Genre Genre { get; set; }

        public ICollection<MovieActorVM> Actors;
    }
}
