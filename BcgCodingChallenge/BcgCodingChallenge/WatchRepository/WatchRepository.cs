using System;
using BcgCodingChallenge.WatchHelper;

namespace BcgCodingChallenge.WatchRepository
{
    public class WatchRepository : IWatchRepository
    {
        private static readonly Dictionary<string, Watch> watchLookup = new Dictionary<string, Watch>();

        static WatchRepository()
        {
            PopulateWatchLookup();
        }

        public static void PopulateWatchLookup()
        {
            watchLookup.Clear();
            watchLookup.Add("001", new Watch() { WatchId = 001, WatchName = "Rolex", UnitPrice = 100, Discount = new Discount() { DiscountQuantity = 3, DiscountPrice = 200 } });
            watchLookup.Add("002", new Watch() { WatchId = 002, WatchName = "Michael Kors", UnitPrice = 80, Discount = new Discount() { DiscountQuantity = 2, DiscountPrice = 120 } });
            watchLookup.Add("003", new Watch() { WatchId = 003, WatchName = "Swatch", UnitPrice = 50 });
            watchLookup.Add("004", new Watch() { WatchId = 004, WatchName = "Casio", UnitPrice = 30 });
        }

        public bool GetTotalCostFromCart(List<string> watches, out string result)
        {
            double totalCost = 0;
            Dictionary<string, int> watchCart = new Dictionary<string, int>();
            try
            {

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

                foreach (KeyValuePair<string, int> item in watchCart)
                {
                    if (watchLookup != null)
                    {
                        if (watchLookup.ContainsKey(item.Key))
                        {
                            Watch watch = watchLookup[item.Key];
                            
                            if (watch.Discount != null)
                            {
                                int discountedWatches = item.Value / watch.Discount.DiscountQuantity;
                                int regularWatches = item.Value % watch.Discount.DiscountQuantity;
                                totalCost = totalCost + (discountedWatches * watch.Discount.DiscountPrice) + (regularWatches * watch.UnitPrice);
                            }
                            else
                            {
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
            catch (Exception ex)
            {
                result = ex.Message;
                return true;
            }

        }
    }
}

