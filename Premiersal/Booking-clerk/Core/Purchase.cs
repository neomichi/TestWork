using System;
using System.ComponentModel.DataAnnotations;

namespace Booking_clerk.Core
{
    public class Purchase

    {
        public int Id { get; set; }
        [Required]
        public int Film { get; set; }
        [Required]
        public int Tikets { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}