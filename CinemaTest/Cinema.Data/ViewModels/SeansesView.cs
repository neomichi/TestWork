using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Data.ViewModels
{
    public class SeansesView
    {
        [Required]
        public Guid Id { get; set; }
        [Display(Name = "название фильма")]
        [Required]
        [MaxLength(160)]
        public string Title { get; set; }
        [Display(Name = "время начала")]
        [Required(ErrorMessage = "формат (dd.MM.yyyy HH:mm)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime Start { get; set; }

        [Display(Name = "число мест cвободных")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "укажите число мест от 1")]
        public int FreePlaces { get; set; } = 0;
      
    } 
}