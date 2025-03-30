using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public required string Location { get; set; }

        public List<int> ArtistIds { get; set; } = new();
    }
}
