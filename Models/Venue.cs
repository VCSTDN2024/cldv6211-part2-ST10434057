using System.ComponentModel.DataAnnotations;

namespace WebAppPart1ST10434057.Models
{
    public class Venue
    {
        public int VenueID { get; set; }

        [Required, StringLength(100)]
        public string VenueName { get; set; }

        [Required, StringLength(255)]
        public string Location { get; set; }

        [Range(1, 100000, ErrorMessage = "Capacity must be between 1 and 100,000.")]
        public int Capacity { get; set; }

        [Url]
        public string? ImageUrl { get; set; } // Nullable placeholder image URL
    }
}

