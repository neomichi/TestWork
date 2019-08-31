using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SharpDevTest.Data.Models;

namespace SharpDevTest.Data.Model
{
    public class HistoryLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; } 
        [Required]
        public Guid SenderId { get; set; }    
        [Required]
        public ApplicationUser Recipient { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int Summ { get; set; }
        public DateTime CreateDateIt { get; set; } = DateTime.Now;
    }
}
