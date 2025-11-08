using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bus_ticket_booking.Models
{
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
