using System.ComponentModel.DataAnnotations;

namespace Lab0.Models
{
    public class Label
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        public string? Country { get; set; }

        public ICollection<AlbumModel>? Albums { get; set; }
    }
}