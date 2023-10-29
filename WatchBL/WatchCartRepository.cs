
using WatchCart.Data.Watchcartchallenge;
using WatchCart.IBLL;
using WatchCart.Model;

namespace WatchCart.BL
{
	public class WatchCartRepository:IWatchCart
	{
        //public bool GetTotalCostFromCart(List<string> watches, out string result)
        //{
        //    result = "";
        //    return false;
        //}
        WatchcartchallengeContext dbContext = new WatchcartchallengeContext();
        public bool GetTotalCostFromCart(List<string> watches, out string result)
        {
            decimal totalCost = 0;
            try
            {
                if (watches is null || watches.Count == 0)
                {
                    result = "Watch cart is empty. Please add some items to the cart";
                    return true;
                }
                // Dictionary<string, int> watchCart = WatchHelper.PopualateCart(watches);
                Dictionary<int, int> watchCart = PopulateCartSet(watches);
                foreach (KeyValuePair<int,int> item in watchCart)
                {
                    //if (WatchHelper.watchLookup.ContainsKey(item.Key))
                    
                    if (dbContext.Watches.Where(w => w.WatchId.Equals(item.Key)).FirstOrDefault()!=null)
                    {
                        totalCost = totalCost += CalculateWatchCost(item, dbContext);
                    }
                    else
                    {
                        result = $"Invalid item {item.Key} in cart!";
                        return true;
                    }

                }
                if (totalCost == 0)
                {
                    result = "Total cost of cart cannot be zero";
                    return true;
                }
                result = totalCost.ToString();
                return false;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return true;
            }

        }

        public Dictionary<int, int> PopulateCartSet(List<string> watches)
        {
            Dictionary<int, int> watchCart = new Dictionary<int, int>();
            //maintain a cart to get watches and its corresponding count
            //foreach (string watch in watches)
                foreach (string watch in watches)
                {
                //string watchName = watch.WatchId.ToString();
                int watchId = Convert.ToInt32(watch);
                //check if watch is part of metadata
               
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

