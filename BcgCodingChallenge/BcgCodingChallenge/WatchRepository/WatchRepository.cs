using System;
using BcgCodingChallenge.Models;

namespace BcgCodingChallenge.WatchRepository
{
    public class WatchRepository : IWatchRepository
    {
        

        public bool GetTotalCostFromCart(List<string> watches, out string result)
        {
            decimal totalCost = 0;            
            try
            {
                if(watches is null || watches.Count == 0)
                {
                    result = "Watch cart is empty. Please add some items to the cart";
                    return true;
                }
                Dictionary<string, int> watchCart = WatchHelper.PopualateCart(watches);

                foreach (var item in watchCart)
                {
                    if (WatchHelper.watchLookup.ContainsKey(item.Key))
                    {
                        totalCost=totalCost += WatchHelper.CalculateWatchCost(item);
                    }
                    else
                    {
                        result = "Invalid item in cart!";
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

