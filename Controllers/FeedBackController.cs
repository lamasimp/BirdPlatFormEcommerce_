
using BirdPlatForm.UserRespon;
using BirdPlatForm.ViewModel;
using BirdPlatFormEcommerce.DEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdPlatForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly SwpDataContext _context;

        public FeedBackController(SwpDataContext bird)
        {
            _context = bird;

        }
        [HttpPost("Feedback")]
        public async Task<IActionResult> Feedback( FeedbackModel feedback)
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "UserId");
            if(userIdClaim == null)
            {
                return Unauthorized();
            }
            int userid = int.Parse(userIdClaim.Value);
            var feed = await _context.TbProducts.Include(p => p.TbFeedbacks).FirstOrDefaultAsync(u => u.ProductId == feedback.ProductId);
            if (feedback == null) return Ok(new ErrorRespon
            {
                Error = true,
                Message ="No Product"
            }) ;
            if (feedback.Rate < 1 || feedback.Rate > 5) return BadRequest("Invalid rating , rating must be between 1 to 5");

            var tbfeedback = new TbFeedback
            {
                ProductId = feedback.ProductId,
                UserId = userid,
                Rate = feedback.Rate,
                Detail = feedback.Detail

            };
            _context.TbFeedbacks.Add(tbfeedback);
            await _context.SaveChangesAsync();

            var rate = await GetProductAverageRates(feedback.ProductId);
            var updateRate = await _context.TbProducts.FirstOrDefaultAsync(u => u.ProductId == feedback.ProductId);
            {
                updateRate.Rate = (int?)rate;

                
            }
            await _context.SaveChangesAsync();
            var rateshop = await GetProductAverageRates((int)updateRate.ShopId);
            var updateRateShop = await _context.TbShops.FirstOrDefaultAsync(u => u.ShopId == updateRate.ShopId);
            {
                updateRateShop.Rate = (int?)rateshop;
            }
            await _context.SaveChangesAsync();

            return Ok("success");
            
        }
        [HttpGet]
        public async Task<IActionResult> getFeedBack()
        {
            var feedback = _context.TbFeedbacks.ToList();
            return Ok(feedback);
        }
       
        private async Task<double> GetProductAverageRates(int productID)
        {
                double argRate =(double) await _context.TbFeedbacks.Where(p => p.ProductId == productID).AverageAsync(x => x.Rate);
                double roundRate = Math.Round(argRate, 1);
            return roundRate;
            
        }
        private async Task<double> GetShopAverageRates(int shopId)
        {
            double argRate = (double)await _context.TbProducts.Where(p => p.ShopId == shopId).AverageAsync(x => x.Rate);
            double roundRate = Math.Round(argRate, 1);
            return roundRate;

        }
    }
}
