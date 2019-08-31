using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Code;
using Web.Model;

namespace Web.ViewModel
{
    public class TradeView
    {
        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Количество")]
        public int Amount { get; set; }
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        [Required]
        public Constant.ActionType ActionType { get; set; }
        public IEnumerable<Order> BuyOrders { get; set; }
        public IEnumerable<Order> SellOrders { get; set; }

        public IEnumerable<CompleteTrade> CompleteTrades { get; set; }
    }
}
