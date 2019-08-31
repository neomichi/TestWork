using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Code;

namespace Web.Model
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Количество")]
        public int Amount { get; set; }
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
        [Required]
        [Display(Name = "Время и дата")]
        public DateTime CreateAt { get; set; }
        [Required]
        public Constant.ActionType ActionType { get; set; }

    }
}
