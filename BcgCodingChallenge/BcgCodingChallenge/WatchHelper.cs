using System;
using BcgCodingChallenge.Models;

namespace BcgCodingChallenge
{
	public class WatchHelper
	{
        public static readonly Dictionary<string, Watch> watchLookup = new Dictionary<string, Watch>();

        
        static WatchHelper()
        {
            //Runs this static method the first time class is referenced
            PopulateWatchLookup();
        }

        public static void PopulateWatchLookup()
        {
            //clear dictionary before adding metadata
            watchLookup.Clear();
            watchLookup.Add("001", new Watch() { WatchId = 001, WatchName = "Rolex", UnitPrice = 100, Discount = new Discount() { DiscountQuantity = 3, DiscountPrice = 200 } });
            watchLookup.Add("002", new Watch() { WatchId = 002, WatchName = "Michael Kors", UnitPrice = 80, Discount = new Discount() { DiscountQuantity = 2, DiscountPrice = 120 } });
            watchLookup.Add("003", new Watch() { WatchId = 003, WatchName = "Swatch", UnitPrice = 50 });
            watchLookup.Add("004", new Watch() { WatchId = 004, WatchName = "Casio", UnitPrice = 30 });
        }

        public static Dictionary<string, int> PopualateCart(List<string> watches)
		{
            Dictionary<string, int> watchCart = new Dictionary<string, int>();
            //maintain a cart to get watch and its corresponding count
            foreach (string watch in watches)
            {
                //check if watch is part of metadata
                if (watchCart.ContainsKey(watch))
                {
                    watchCart[watch] = watchCart[watch] + 1;
                }
                else
                {
                    watchCart.Add(watch, 1);
                }
            }
            return watchCart;
        }

        public static decimal CalculateWatchCost(KeyValuePair<string,int> item)
        {           
                Watch watch = watchLookup[item.Key];
                if (watch.Discount != null)
                {
                    int discountedWatches = item.Value / watch.Discount.DiscountQuantity;
                    int regularWatches = item.Value % watch.Discount.DiscountQuantity;
                    //adding discounted wathces plus regular price watches
                    return (discountedWatches * watch.Discount.DiscountPrice) + (regularWatches * watch.UnitPrice);
                }
                else
                {
                   return item.Value * watch.UnitPrice;
                }
        }
	}
}

