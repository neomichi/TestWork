using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SharpDevTest.Data;

using SharpDevTest.Web.Models;
using SharpDevTest.Web.ViewModels;

namespace SharpDevTest.Web.Controllers
{


    public class HomeController : Controller
    {

        private readonly MyDbContext _context;
        public HomeController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Finance()
        {
            var currentuser = _context.Users
                .Single(x => x.Email.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase));


            var items = _context.Users
                .Where(x => !x.Email.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase))
                .AsNoTracking()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString("N"),
                    Text = string.Format("{0} {1}", x.FirstName, x.LastName)
                }).ToList();

            var historyLog = _context.HistorysLog
                .Where(x => x.SenderId == currentuser.Id)
                .OrderByDescending(x => x.CreateDateIt).ToList();

            var transactionView = new HistoryTransactionView
            {
                FromUser = currentuser,
                ListItems = items,
                HistoryLogs = historyLog
            };

            return View(transactionView);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Finance(HistoryTransactionView historyTransaction)
        {
            if (ModelState.IsValid) return NotFound();

            var sender = _context.Users
                .FirstOrDefault(x => x.Id == historyTransaction.Id && x.Purse >= historyTransaction.Summ);

            var recipient = _context.Users
                .FirstOrDefault(x => x.Id == historyTransaction.RecipientId);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.HistorysLog.Add(new Data.Model.HistoryLog()
                    {
                        SenderId = sender.Id,
                        Recipient = recipient,
                        CreateDateIt = DateTime.Now,
                        Summ = historyTransaction.Summ
                    });
                    sender.Purse = sender.Purse - historyTransaction.Summ;
                    recipient.Purse = recipient.Purse + historyTransaction.Summ;
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }
            return RedirectToAction("Finance");
        }

        [Authorize]
        [Route("autocomplete")]
        public IActionResult Autocomplete(string query)
        {
            var listNames = _context.Users
                .Where(x => !x.Email.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase))
                .AsNoTracking()
                .Select(x => new
                {
                    Key = x.Id.ToString("N"),
                    Value = string.Format("{0} {1}", x.FirstName, x.LastName)
                });



            //  var Allnames= listNames.Select(x => string.Format("{0:N},{1}{2}", x.Id, x.FirstName, x.LastName))
            //    .ToList();

            //var aa = Allnames.Select(x => new
            //{
            //    Word = x,
            //    Len = Code.LCS(x, query).Length

            //}).Where(x => x.Len > 0).OrderByDescending(x => x.Len).Select(x =>Guid.Parse(x.Word.Substring(0,32))).ToList();

            //var final = listNames.Where(x => aa.Contains(x.Id)).Select(x => new
            //{
            //    Id = x.Id.ToString("N"),
            //    Title = string.Format("{0} {1}", x.FirstName, x.LastName)
            //}).ToList();



            return Json(listNames);
        }


        [HttpPost]
        public IActionResult GetHistoryLog()
        {
            var currentUser = _context.Users
             .Single(x => x.Email.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase));

            var data = _context.HistorysLog
                .Where(t => t.SenderId == currentUser.Id)
                .AsNoTracking().OrderByDescending(x => x.CreateDateIt)
                .Select(t => new
             {
                t.Id,
                RecipientId=t.Recipient.Id,
                RecipientFio = string.Format("{0} {1}", t.Recipient.FirstName, t.Recipient.LastName),
                t.Summ,
                CreatedDate = t.CreateDateIt.ToString("dd.MM.yyyy HH:mm")
            }).ToArray();
            return Json(data);
        }

        [HttpPost]
        public IActionResult RepeatTransactionId(Guid id)
        {
            var transaction = _context.HistorysLog
                .Include(x=>x.Recipient)
                .AsNoTracking().SingleOrDefault(x => x.Id == id);
            var Id = transaction.Recipient.Id.ToString("N");
            if (transaction != null)
            {
                return Json(new { Id, transaction.Summ });
            }
            return NotFound();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
