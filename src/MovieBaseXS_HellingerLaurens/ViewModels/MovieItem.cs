using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieBaseXS_HellingerLaurens.Entities;

namespace MovieBaseXS_HellingerLaurens.ViewModels
{
    public class MovieItemVM
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public byte Rating { get; set; }

        [Display(Name = "The Director")]
        public string Director { get; set; }

        public Genre Genre { get; set; }
        
    }
}
