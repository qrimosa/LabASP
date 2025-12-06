using System;
using System.ComponentModel.DataAnnotations;

namespace Lab07.Models.Movies
{
    public class MovieModel
    {
        [Required]
        public string? Title { get; set; }

        public string? Overview { get; set; }
        
        [DataType(DataType.Date)]
        public DateOnly? ReleaseDate { get; set; }

        [Display(Name = "Production Company")]
        public int? CompanyId { get; set; }   // ← was int, make it int?
    }

}