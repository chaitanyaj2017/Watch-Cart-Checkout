using System;
using WatchCart.IBLL;
using WatchCart.BL;
using WatchCart.Model;
using WatchCart.Data.Watchcartchallenge;
using Microsoft.EntityFrameworkCore;


namespace UnitTestProject
{
	[TestClass]
	public class WatchUnitTest
	{
        //[TestInitialize]
        public WatchcartchallengeContext MockDbContext()
        {
            var options = new DbContextOptionsBuilder<WatchcartchallengeContext>().UseInMemoryDatabase(databaseName: "Watchcartchallenge").Options;
            using (var context = new WatchcartchallengeContext(options))
            {
                context.Discounts.Add(new Discount { DiscountId = 1, DiscountQuantity = 2, DiscountPrice = 300 });
                context.Discounts.Add(new Discount { DiscountId = 2, DiscountQuantity = 3, DiscountPrice = 225 });

                context.Watches.Add(new Watch { WatchId = 1,WatchName = "casio", UnitPrice = 20 });
                context.Watches.Add(new Watch { WatchId = 2,WatchName = "cartier", UnitPrice = 200, DiscountId = 1 });
                context.Watches.Add(new Watch { WatchId = 3,WatchName = "Ralph lauren", UnitPrice = 150, DiscountId = 2 });
                context.SaveChanges();
            }
            return new WatchcartchallengeContext(options);

        }

        private readonly IWatchCart watchCartRepository;

        public WatchUnitTest()
		{         
            watchCartRepository = new WatchCartRepository(MockDbContext());
        }

        [TestMethod]
		public void Test_Calculated_TotalCost_Is_Correct()
		{
			decimal expectedtotalCost = 620;
            string result;
            List<string> watches = new List<string>() { "001", "002", "003", "002", "003" };


			Assert.IsFalse(watchCartRepository.GetTotalCostFromCart(watches, out result)); 
			Assert.AreEqual(expectedtotalCost, Convert.ToDecimal(result));
		}

		[TestMethod]
		public void Test_Empty_Cart_Prompts_User()
		{
			//decimal expectedtotalCost = 360;
			string expectedMessage = "Watch cart is empty. Please add some items to the cart";
            string result;
			List<string>? watches = null;


            Assert.IsTrue(watchCartRepository.GetTotalCostFromCart(watches, out result));
            Assert.AreEqual(expectedMessage,result);
        }

        [TestMethod]
        public void Test_Invalid_Watch_Id_Throws_Error()
        {
            string expectedMessage = "Invalid item 7 in cart!";
            List<string> watches = new List<string>() { "007", "002", "001", "002", "003" };
            string result;

            Assert.IsTrue(watchCartRepository.GetTotalCostFromCart(watches, out result));
            Assert.AreEqual(expectedMessage, result);
        }
        [TestMethod]
        public void Test_Multiple_discounts_applied()
        {
            decimal expectedtotalCost = 600;
            string result;
            List<string> watches = new List<string>() { "003", "003", "003", "003", "003","003","003" };


            Assert.IsFalse(watchCartRepository.GetTotalCostFromCart(watches, out result));
            Assert.AreEqual(expectedtotalCost, Convert.ToDecimal(result));
        }
    }
}

