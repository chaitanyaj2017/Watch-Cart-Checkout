using System;
using BcgCodingChallenge.WatchHelper;

namespace BcgCodingChallenge.WatchRepository
{
	public class WatchRepository:IWatchRepository
	{
		//private readonly Dictionary<string, Watch> watchLookup = new Dictionary<string, Watch>();

  //      public WatchRepository()
  //      {

  //      }

  //      public void PopulateWatchLookup()
  //      {

  //      }
        public bool GetTotalCostFromCart(List<string> watches,out string result)
        {
            
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
                        if (watch.Discount != null)
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
            if (totalCost == 0)
            {
                result = "All watches added to the cart are invalid";
                return true;
            }
            //return Json(new { price = totalCost });
            result = totalCost.ToString();
            return false;
        }
    
    }
}

