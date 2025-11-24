using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Models
{
    public class AlbumModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Album name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Band name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Band name must be between 2 and 50 characters.")]
        public string Band { get; set; }

        public List<string>? Songs { get; set; }

        [Range(1, 200, ErrorMessage = "Chart position must be between 1 and 200.")]
        public int? ChartPosition { get; set; }

        [Required(ErrorMessage = "Release date is required.")]
        [DataType(DataType.Date)]
        public DateOnly ReleaseDate { get; set; }

        [Required(ErrorMessage = "Total duration is required.")]
        [DataType(DataType.Time)]
        public TimeSpan TotalDuration { get; set; }
    }
}