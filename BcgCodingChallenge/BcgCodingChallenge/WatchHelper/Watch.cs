using System;
namespace BcgCodingChallenge.WatchHelper
{
	
        public class Watch
        {
            public Watch()
            {

            }
            public int WatchId { get; set; }

            public string? WatchName { get; set; }

            public Discount? Discount { get; set; }

            public double UnitPrice { get; set; }

        }
   
}

