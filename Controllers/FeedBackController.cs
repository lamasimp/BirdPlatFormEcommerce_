
using BirdPlatForm.UserRespon;
using BirdPlatForm.ViewModel;
using BirdPlatFormEcommerce.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdPlatForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly SwpContext _context;

        public FeedBackController(SwpContext bird)
        {
            _context = bird;

        }
        [HttpPost("Feedback")]
        public async Task<IActionResult> Feedback(int id, FeedbackModel feedback)
        {
            var feed = await _context.TbFeedbacks.FindAsync(id);
            if (feedback == null) return Ok(new ErrorRespon
            {
                Error = true,
                Message ="No Product"
            }) ;
            if (feedback.Rate < 1 || feedback.Rate > 5) return BadRequest("Invalid rating , rating must be between 1 to 5");
            var tbfeedback = new TbFeedback
            {
                ProductId = id,
                UserId = feedback.UserId,
                Rate = feedback.Rate,
                Detail = feedback.Detail
            };
            _context.TbFeedbacks.Add(tbfeedback);
            await _context.SaveChangesAsync();
            return Ok("success");
            
        }
    }
}
