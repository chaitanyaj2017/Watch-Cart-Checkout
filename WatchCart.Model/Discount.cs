using System;
namespace WatchCart.Model
{
	public class Discount
	{
        //public int DiscountQuantity { get; set; }

        //public decimal DiscountPrice { get; set; }

        public Discount()
        {

        }

        public int DiscountId { get; set; }

        public int? DiscountQuantity { get; set; }

        public decimal? DiscountPrice { get; set; }

        public virtual ICollection<Watch> Watches { get; set; } = new List<Watch>();
    }
}

