using BirdPlatForm.UserRespon;
using BirdPlatFormEcommerce.Entity;

using BirdPlatFormEcommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BirdPlatFormEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly SwpContext _context;


        public ShopController(SwpContext swp)
        {
            _context = swp;

        }
        [HttpPost("registerShop")]

        public async Task<IActionResult> RegisterShop(ShopModel shopmodel)
        {
            int userId = getuserIDfromtoken();
            var isShop = await _context.TbShops.FirstOrDefaultAsync(s => s.UserId == userId);
            if (isShop != null)
            {
                return Ok(new ErrorRespon
                {
                    Error = false,
                    Message = "UserId have to shop"
                });
            }
            var shop = new TbShop
            {
                ShopName = shopmodel.shopName,
                Address = shopmodel.Address,
                Phone = shopmodel.Phone,

                UserId = userId,

            };
            _context.TbShops.Add(shop);
            await _context.SaveChangesAsync();
            var user = await _context.TbUsers.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.IsShop = true; 
                await _context.SaveChangesAsync();
            }
            return Ok(new ErrorRespon
            {

                Message = "Register shop Success"

            });
        }

        private int getuserIDfromtoken()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var accountIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId");
                if (accountIdClaim != null && int.TryParse(accountIdClaim.Value, out int UserId))
                {
                    return UserId;
                }
            }
            throw new InvalidOperationException("Invalid token or missing accountId claim.");
        }
        [HttpGet]
        public async Task<IActionResult> getMyshop()
        {
            var myshop = User.Claims.FirstOrDefault(u => u.Type == "UserId");
            if(myshop == null)
            {
                return NotFound();
            }
            int userid = int.Parse(myshop.Value);
            var shop = _context.TbShops.FirstOrDefault(x => x.UserId == userid);
            if(shop == null)
            {
                return NotFound();
            }
            var isshop = new ViewShop
            {
                Rate = (int)shop.Rate,
                shopName = shop.ShopName,
                Address = shop.Address,
                phone = shop.Phone,
                
            };
            return Ok(isshop);
        }
        



    }
}
