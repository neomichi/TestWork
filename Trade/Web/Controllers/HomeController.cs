using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Web.Code;
using Web.Model;
using Web.ViewModel;

namespace Web.Controllers
{
    public class HomeController : Controller
    {

        readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.OrderBy(x => x.CreateAt).AsNoTracking().ToListAsync();
            var completeTrades = await _context.CompleteTrades.AsNoTracking().ToListAsync();
            
            var pkQueue = orders.Where(x=>x.ActionType == Constant.ActionType.Buy).ToList();
            var ppQueue = orders.Where(x =>x.ActionType == Constant.ActionType.Sell).ToList();

            do
            {
                var bestExchange =
                 (from pk in pkQueue
                  from pp in ppQueue
                  where pp.Price >= pk.Price
                  let u = Math.Min(pp.Amount, pk.Amount)
                  let t = u * pp.Price
                  orderby t descending
                  orderby pp.CreateAt
                  select new Calc
                  {
                      Pk = pk,
                      Pp = pp,
                      Summa = t,
                      AmountMin = u,
                  }).FirstOrDefault();

                if (bestExchange == null)
                {
                    break;
                }

                var completeTrade = new CompleteTrade()
                {
                    Amount = bestExchange.AmountMin,
                    Price = bestExchange.Summa/bestExchange.AmountMin,
                    BuyComment = bestExchange.Pk.Comment,
                    BuyTime = bestExchange.Pk.CreateAt,
                    SellComment = bestExchange.Pp.Comment,
                    SellTime = bestExchange.Pp.CreateAt,
                    CompleteAt = DateTime.Now
                };
                completeTrades.Add(completeTrade);
                _context.CompleteTrades.Add(completeTrade);


                ppQueue.Remove(bestExchange.Pp);
                pkQueue.Remove(bestExchange.Pk);

                if (bestExchange.AmountMin != bestExchange.Pp.Amount)
                {
                    bestExchange.Pp.Amount = bestExchange.Pp.Amount - bestExchange.AmountMin;
                    ppQueue.Add(bestExchange.Pp);
                    _context.Orders.Update(bestExchange.Pp);
                    _context.Orders.Remove(bestExchange.Pk);
                }
                if (bestExchange.AmountMin != bestExchange.Pk.Amount)
                {
                    bestExchange.Pk.Amount = bestExchange.Pk.Amount - bestExchange.AmountMin;
                    pkQueue.Add(bestExchange.Pk);
                    _context.Orders.Update(bestExchange.Pk);
                    _context.Orders.Remove(bestExchange.Pp);
                }
                _context.SaveChanges();

            } while (ppQueue.Count>0 || pkQueue.Count>0);
            

            var tradeView = new TradeView()
            {
                BuyOrders = pkQueue,
                SellOrders = ppQueue,
                CompleteTrades = completeTrades
            };
            

            return View(tradeView);
        }

        [HttpPost("AddOrder", Name = "AddOrder")]
        public IActionResult AddOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                order.CreateAt = DateTime.Now;
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}
