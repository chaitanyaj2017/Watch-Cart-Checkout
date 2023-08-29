using System;
namespace BcgCodingChallenge.Models
{
	
        public class Watch
        {
            public Watch()
            {

            }
            public int WatchId { get; set; }

            public string? WatchName { get; set; }

            public Discount? Discount { get; set; }

            public decimal UnitPrice { get; set; }

        }
   
}

