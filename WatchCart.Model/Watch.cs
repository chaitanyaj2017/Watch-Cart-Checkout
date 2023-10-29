using System;
namespace WatchCart.Model
{
	public class Watch
	{
        public Watch()
        {

        }
        //public int WatchId { get; set; }

        //public string? WatchName { get; set; }

        //public Discount? Discount { get; set; }

        //public decimal UnitPrice { get; set; }

        public int WatchId { get; set; }

        public string? WatchName { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? DiscountId { get; set; }

        public virtual Discount? Discount { get; set; }
    }
}

