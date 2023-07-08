using BirdPlatFormEcommerce.NEntity;

using BirdPlatFormEcommerce.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace BirdPlatFormEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly SwpDataBaseContext _context;

        public CustomerController(SwpDataBaseContext swp)
        {
            _context = swp;
        }

        [HttpGet("GetcatoryReport")]
        public async Task<IActionResult> getcategoryReport()
        {
            var report = _context.TbCategoryReports.ToList();
            return Ok(report);
        }
        [HttpPost("reportShop")]
        public async Task<IActionResult> cretaereport(ReportShopModel model)
        {
            var useridClaim = User.Claims.FirstOrDefault(u => u.Type == "UserId");
            if (useridClaim == null)
            {
                return Unauthorized();
                
               
            }
            int userid = int.Parse(useridClaim.Value);
            var shop = await _context.TbShops.FindAsync(model.ShopId);
            if (shop == null)
            {
                return BadRequest("No shop");
            }
            shop.IsVerified = true;
            var report = new TbReport
            {
                Detail = model.Detail,
                Status = false,
                ShopId = model.ShopId,
                UserId = userid,
                CateRpId = model.categoriaId,
                
            };
             _context.TbReports.Add(report);
            await _context.SaveChangesAsync();
            return Ok("Success ");

        }
      
    }
}

