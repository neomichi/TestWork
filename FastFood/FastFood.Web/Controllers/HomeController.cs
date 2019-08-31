using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FastFood.Web.Models;
using FastFood.Web.Models.Enum;
using FastFood.Web.Code;
using FastFood.Web.ViewModels;
using FastFood.Web.ModelView;

namespace FastFood.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var fastFoodProducts = new List<FastFoodView>()
            {

                new FastFoodView () {CategoryType=CategoryType.Drink, Name ="Вода",Filter=Helper.GetSumm(new IBaseProduct[] {new Water()})},
                new FastFoodView () {CategoryType=CategoryType.AdditionDrink, Name ="Молоко",Filter=Helper.GetSumm(new IBaseProduct[] {new Milk()})},
                new FastFoodView () {CategoryType=CategoryType.Drink, Name ="Espresso",Filter=Helper.GetSumm(new IBaseProduct[] {new Espresso()})},
                new FastFoodView () {CategoryType=CategoryType.Drink, Name ="Cappuccino",Filter=Helper.GetSumm(new IBaseProduct[] {new Water(),new Milk(),new Espresso()})},
                new FastFoodView () {CategoryType=CategoryType.Drink, Name ="Latte",Filter=Helper.GetSumm(new IBaseProduct[] {new Milk(),new Espresso()})},
                new FastFoodView () {CategoryType=CategoryType.Drink, Name ="Черный чай",Filter=Helper.GetSumm(new IBaseProduct[] {new Tea(),new Water()})},
                new FastFoodView () {CategoryType=CategoryType.Drink, Name ="Зеленый чай",Filter=Helper.GetSumm(new IBaseProduct[] {new Tea(),new Water()})},
                new FastFoodView () {CategoryType=CategoryType.AdditionDrink, Name ="Сахар",Filter=Helper.GetSumm(new IBaseProduct[] {new Sugar()})},
                new FastFoodView () {CategoryType=CategoryType.AdditionDrink, Name ="Сироп",Filter=Helper.GetSumm(new IBaseProduct[] {new Syrup()})},
                new FastFoodView () {CategoryType=CategoryType.Food, Name ="Хлеб",Filter=Helper.GetSumm(new IBaseProduct[] {new Bread()})},
                new FastFoodView () {CategoryType=CategoryType.Food, Name ="Булочка",Filter=Helper.GetSumm(new IBaseProduct[] {new Roll()})},
                new FastFoodView () {CategoryType=CategoryType.Food, Name ="Чипсы",Filter=Helper.GetSumm(new IBaseProduct[] {new Chips()})},
                new FastFoodView () {CategoryType=CategoryType.Food, Name ="Печень",Filter=Helper.GetSumm(new IBaseProduct[] {new Cookies()})},
                new FastFoodView () {CategoryType=CategoryType.AdditionFood, Name ="Ветчина",Filter=Helper.GetSumm(new IBaseProduct[] {new Roll()})},
                new FastFoodView () {CategoryType=CategoryType.AdditionFood, Name ="Сыр",Filter=Helper.GetSumm(new IBaseProduct[] {new Cheese()})},
                new FastFoodView () {CategoryType=CategoryType.AdditionFood, Name ="Джем",Filter=Helper.GetSumm(new IBaseProduct[] {new Jam()})},
                new FastFoodView () {CategoryType=CategoryType.ComplexFod, Name ="Комплекс (Черный чай c cахаром хлебомом и сыром",Filter=Helper.GetSumm(new IBaseProduct[] { new Tea(), new Water(),new Bread(),new Cheese()})},
            };
            var final = fastFoodProducts.GroupBy(x => x.CategoryType).Select(x => new FinalView
            {
                CategoryTitle = x.Key.Description(),
                CategoryType = x.Key,
                foodList = x.ToList(),
            }).OrderBy(x=>(int)x.CategoryType).ToList();
            return View(final);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
