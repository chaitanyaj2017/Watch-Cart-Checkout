﻿using System;
using BcgCodingChallenge.WatchRepository;

namespace UnitTestProject
{
	[TestClass]
	public class WatchUnitTest
	{
        private readonly IWatchRepository watchRepository;
        public WatchUnitTest()
		{
			watchRepository = new WatchRepository();

        }

		[TestMethod]
		public void Test_Calculated_TotalCost_Is_Correct()
		{
			decimal expectedtotalCost = 360;
            string result;
            List<string> watches = new List<string>() { "001", "002", "001", "004", "003" };


			Assert.IsFalse(watchRepository.GetTotalCostFromCart(watches, out result)); 
			Assert.AreEqual(expectedtotalCost, Convert.ToDecimal(result));
		}

		[TestMethod]
		public void Test_Empty_Cart_Prompts_User()
		{
			//decimal expectedtotalCost = 360;
			string expectedMessage = "Watch cart is empty. Please add some items to the cart";
            string result;
			List<string>? watches = null;


            Assert.IsTrue(watchRepository.GetTotalCostFromCart(watches, out result));
            Assert.AreEqual(expectedMessage,result);
        }

        [TestMethod]
        public void Test_Invalid_Watch_Id_Throws_Error()
        {
            string expectedMessage = "Invalid item 007 in cart!";
            List<string> watches = new List<string>() { "007", "002", "001", "004", "003" };
            string result;

            Assert.IsTrue(watchRepository.GetTotalCostFromCart(watches, out result));
            Assert.AreEqual(expectedMessage, result);
        }
        [TestMethod]
        public void Test_Multiple_discounts_applied()
        {
            decimal expectedtotalCost = 500;
            string result;
            List<string> watches = new List<string>() { "001", "001", "001", "001", "001","001","001" };


            Assert.IsFalse(watchRepository.GetTotalCostFromCart(watches, out result));
            Assert.AreEqual(expectedtotalCost, Convert.ToDecimal(result));
        }
    }
}

