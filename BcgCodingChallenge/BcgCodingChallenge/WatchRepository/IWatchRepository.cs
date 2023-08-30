using System;
namespace BcgCodingChallenge.WatchRepository
{
	public interface IWatchRepository
	{
		public bool GetTotalCostFromCart(List<string> watches,out string result);
	}
}

