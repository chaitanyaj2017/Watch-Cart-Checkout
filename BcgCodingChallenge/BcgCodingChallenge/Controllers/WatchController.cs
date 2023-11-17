using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Mvc;
using WatchCart.Data.Watchcartchallenge;
using WatchCart.IBLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BcgCodingChallenge.Controllers
{
    [Route("api/[controller]")]
    public class WatchController : Controller
    {
        private readonly IWatchCart watchCart;

        public WatchController(IWatchCart watchCart)
        {
            this.watchCart = watchCart;
           
        }

        [HttpGet("health")]
        public JsonResult GetHealth()
        {
            return Json(new { message = "success" });
        }

        [HttpPost("checkout")]
        public JsonResult GetTotalCost([FromBody] List<string> watches)
        {
            /*
             ["001","002","001","004","003","001","001","001","007","002","002"]
             */
            string result;
            if (watchCart.GetTotalCostFromCart(watches, out result))
            {

                return Json(result);
            }
            else
            {
                return Json(new { price = Convert.ToDouble(result) });
            }
        }

        
    }
}

