using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SharpDevTest.Data.Model;
using SharpDevTest.Data.Models;

namespace SharpDevTest.Web.ViewModels
{
    public class HistoryTransactionView
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public ApplicationUser FromUser { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Укажите значение больше 1")]
        [Display(Name = "Сумма")]
        public int Summ { get; set; }
        [Required(ErrorMessage = "Укажите кому")]
        [Display(Name ="Кому")]
        public Guid RecipientId { get; set; }

        public List<SelectListItem> ListItems { get; set; }

        public List<HistoryLog> HistoryLogs { get; set; }

    }
}
