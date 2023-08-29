using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BcgCodingChallenge.WatchHelper;
using BcgCodingChallenge.WatchRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BcgCodingChallenge.Controllers
{
    [Route("api/[controller]")]
    public class WatchController : Controller
    {
        private readonly IWatchRepository watchRepository;

        public WatchController(IWatchRepository watchRepository)
        {
            this.watchRepository = watchRepository;
        }

        [HttpPost("checkout")]
        public JsonResult GetTotalCost([FromBody] List<string> watches)
        {
            /*
             ["001","002","001","004","003","001","001","001","007","002","002"]
             */
            if (watches is null || watches.Count == 0)
            {
                return Json("Watch cart is empty. Please add some items to the cart");
            }
            else
            {
                string result;
                if (watchRepository.GetTotalCostFromCart(watches, out result))
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
}

