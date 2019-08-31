using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BrokereeSolution.Data.Model
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(160)]
        public string Text { get; set; }
      
    }
}
