using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Data.Models
{
    public class SeanceSpectator:Entity
    {
        public Guid SeanceId { get; set; }
        public Seance Seance { get; set; }

        [Required]
        public int QuantityTickets { get; set; }
        [Required]
        public DateTime CreateIt { get; set; } = DateTime.Now;
        
    }
}
