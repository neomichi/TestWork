using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BrokereeSolution.Data.ViewModel
{
    public class ItemView
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(160)]
        public string Text { get; set; }

        public int Index { get; set; } = 0;
        public int Length { get; set; } = 0;

        public ActionType ActionType;
    }
}
