using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model;
using Web.ViewModel;

namespace Web.Code
{
    public class Helper
    {
        public static Calc FindBestExchange(List<Order> pkQueue,List<Order> ppQueue)
        {
           return (from pk in pkQueue
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

            
        }
    }
}
