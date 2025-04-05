using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppPart1ST10434057.Models
{
    public class Event
    {
        public int EventID { get; set; }

        [Required, StringLength(150)]
        public required string EventName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [ForeignKey("Venue")]
        public int? VenueID { get; set; } // Nullable, as events can exist without a venue

        public virtual Venue? Venue { get; set; } // Navigation property with lazy loading

        [Url]
        public string? ImageUrl { get; set; } // Nullable for optional image
    }
}
