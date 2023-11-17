using WatchCart.Data.Watchcartchallenge;
using WatchCart.Model;

namespace WatchCart.BL
{
	public class Helper
	{
        public static Dictionary<int, int> PopulateCartSet(List<string> watches)
        {
            Dictionary<int, int> watchCart = new Dictionary<int, int>();

            foreach (string watch in watches)
            {
                int watchId = Convert.ToInt32(watch);
                if (watchCart.ContainsKey(watchId))
                {
                    watchCart[watchId] = watchCart[watchId] + 1;
                }
                else
                {
                    watchCart.Add(watchId, 1);
                }


            }
            return watchCart;
        }

        public static decimal CalculateWatchCost(KeyValuePair<int, int> item, WatchcartchallengeContext dbContext)
        {
            // Watch watch = watchLookup[item.Key];
            Watch watch = dbContext.Watches.Where(w => w.WatchId.Equals(item.Key)).FirstOrDefault();
            if (watch != null)
            {

                if (watch.DiscountId != null)
                {
                    Discount discount = dbContext.Discounts.Where(di => di.DiscountId == watch.DiscountId).FirstOrDefault();
                    int discountedWatches = item.Value / (int)discount.DiscountQuantity;
                    int regularWatches = item.Value % (int)discount.DiscountQuantity;
                    //adding discounted wathces plus regular price watches
                    return (discountedWatches * (decimal)discount.DiscountPrice) + (regularWatches * (decimal)watch.UnitPrice);
                }
                else
                {
                    return item.Value * (decimal)watch.UnitPrice;
                }
            }
            return 0;

        }
    }
}

