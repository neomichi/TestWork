using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class Seance : Entity
    {
        [Display(Name = "название фильма")]
        [Required]
        [MaxLength(160)]
        public string Title { get; set; }
        [Display(Name = "время начала")]     
        [DataType(DataType.DateTime)]
       
       //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
       //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }
        [Display(Name = "число мест")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "укажите число мест от 1")]
        public int QuantityPlaces { get; set; } = 0;
        [NotMapped]
        [Display(Name = "число занятых мест")]
        public int OccupiedPlace { get; set; } = 0;
        public virtual ICollection<SeanceSpectator> SeanceSpectators { get; set; }
       
        public Seance()
        {
            SeanceSpectators = new List<SeanceSpectator>();
        }

    }
}
