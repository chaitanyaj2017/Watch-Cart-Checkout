using System;
namespace WatchCart.IBLL
{
	public interface IWatchCart { 
		public bool GetTotalCostFromCart(List<string> watches,out string result);
	}
}

