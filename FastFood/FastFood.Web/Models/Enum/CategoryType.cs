using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FastFood.Web.Models.Enum
{
    public enum CategoryType 
    {
        [DisplayName("Напитки")]
        [Description("Напитки")]
        Drink=10,
        [DisplayName("Еда")]
        [Description("Еда")]
        Food=20,
        [DisplayName("Добавки к еде")]
        [Description("Добавки к еде")]
        AdditionFood=40,
        [Description("Добавки к напиткам")]
        [DisplayName("Добавки к напиткам")]
        AdditionDrink =30,
        [Description("Комплекс")]
        [DisplayName("Комплекс")]
        ComplexFod = 50

    }
}
