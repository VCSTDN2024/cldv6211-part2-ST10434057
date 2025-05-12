using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppPart1ST10434057.Models
{
    public class Booking
    {
        public int BookingID { get; set; }

        [Required, StringLength(100)]
        public string CustomerName { get; set; }

        [Required, StringLength(255)]
        public string ContactDetails { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [ForeignKey("Event")]
        public int EventID { get; set; }

        public virtual Event Event { get; set; }

        [ForeignKey("Venue")]
        public int VenueID { get; set; }

        public virtual Venue Venue { get; set; }
    }
}
