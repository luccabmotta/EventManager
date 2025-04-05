using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public string? Phone {  get; set; }

        public required string Email { get; set; }

        public List<int> GenreIds { get; set; } = new();
    }
}
