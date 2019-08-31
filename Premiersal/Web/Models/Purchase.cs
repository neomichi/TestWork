using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Code
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