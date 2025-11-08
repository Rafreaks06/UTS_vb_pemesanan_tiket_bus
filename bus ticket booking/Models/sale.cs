using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bus_ticket_booking.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Required]
        [ForeignKey("Passenger")]
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        [Required]
        [ForeignKey("Bus")]
        public int BusId { get; set; }
        public Bus Bus { get; set; }

        [Required]
        public DateTime SaleDate { get; set; } = DateTime.Now;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
    }
}
