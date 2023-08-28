using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BcgCodingChallenge.WatchHelper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BcgCodingChallenge.Controllers
{
    [Route("api/[controller]")]
    public class WatchController : Controller
    {
        [HttpGet("checkout")]
        public ActionResult GetTotalCost(List<string> watches)
        {
            /*
             [
            "001",
            "002",
            "001",
            "004",
            "003",
            "001",
            "001",
            "001",
            "007",
            "002",
            "002",
            ]
             */

            double totalCost = 0;
            Dictionary<string, int> watchCart = new Dictionary<string, int>();
            foreach (string watch in watches)
            {
                if (watchCart.ContainsKey(watch))
                {
                    watchCart[watch] = watchCart[watch] + 1;
                }
                else
                {
                    watchCart.Add(watch, 1);
                }
            }

            Dictionary<string, Watch> watchLookup = new Dictionary<string, Watch>();
            //#TODO - separate this logic
            watchLookup.Add("001", new Watch() { WatchId = 001, WatchName = "Rolex", UnitPrice = 100, Discount = new Discount() { DiscountQuantity = 3, DiscountPrice = 200 } });
            watchLookup.Add("002", new Watch() { WatchId = 002, WatchName = "Michael Kors", UnitPrice = 80, Discount = new Discount() { DiscountQuantity = 2, DiscountPrice = 120 } });
            watchLookup.Add("003", new Watch() { WatchId = 003, WatchName = "Swatch", UnitPrice = 50 });
            watchLookup.Add("004", new Watch() { WatchId = 004, WatchName = "Casio", UnitPrice = 30 });

            foreach (KeyValuePair<string, int> item in watchCart)
            {
                if (watchLookup != null)
                {
                    if (watchLookup.ContainsKey(item.Key))
                    {
                        Watch watch = watchLookup[item.Key];
                        //if (watchLookup[item.Key].Discount != null)
                        if(watch.Discount!=null)
                        {
                            //Watch w = watchLookup[item.Key];
                            //int q = item.Value / watchLookup[item.Key].Discount.DiscountQuantity;
                            int q = item.Value / watch.Discount.DiscountQuantity;
                            int r = item.Value % watch.Discount.DiscountQuantity;
                            totalCost = totalCost + (q * watch.Discount.DiscountPrice) + (r * watch.UnitPrice);
                        }
                        else
                        {
                            //totalCost = totalCost + item.Value * watchLookup[item.Key].UnitPrice;
                            totalCost = totalCost + item.Value * watch.UnitPrice;
                        }
                    }
                }

            }
            return null;
        }
    }
}

