using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bus_ticket_booking.Models
{
    public class Bus
    {
        [Key]
        public int BusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string BusName { get; set; }

        [Required]
        public string FromCity { get; set; }

        [Required]
        public string ToCity { get; set; }

        [Required]
        public decimal TicketPrice { get; set; }

        [Required]
        public int TotalSeats { get; set; }

        public int AvailableSeats { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
