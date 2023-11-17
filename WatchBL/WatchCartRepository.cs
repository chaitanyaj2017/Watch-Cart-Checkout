
using WatchCart.Data.Watchcartchallenge;
using WatchCart.IBLL;
using WatchCart.Model;

namespace WatchCart.BL
{
	public class WatchCartRepository:IWatchCart
	{
        
        public WatchcartchallengeContext dbContext { get; set; }
        public WatchCartRepository(WatchcartchallengeContext dbContext)
        {
            this.dbContext = dbContext;
        }
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

                Dictionary<int, int> watchCart = Helper.PopulateCartSet(watches);
                foreach (KeyValuePair<int,int> item in watchCart)
                {
                    
                    
                    if (dbContext.Watches.Where(w => w.WatchId.Equals(item.Key)).FirstOrDefault()!=null)
                    {
                        totalCost = totalCost += Helper.CalculateWatchCost(item, dbContext);
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
        
    }
}

