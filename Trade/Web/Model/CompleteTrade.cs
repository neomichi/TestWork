using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class CompleteTrade
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Выполнено")]
        public DateTime CompleteAt { get; set; }
        [Display(Name = "дата покупки")]
        [Required]
        public DateTime BuyTime { get; set; }
        [Display(Name = "дата продажи")]
        [Required]
        public DateTime SellTime { get; set; }
        [Required]
        [Display(Name = "цена")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "количество")]
        public int Amount { get; set; }

        [Display(Name = "комментарий к покупке")]
        public string BuyComment { get; set; }
        [Display(Name = "комментарий к продаже")]
        public string SellComment { get; set; }
    }
}
