using BirdPlatForm.UserRespon;
using BirdPlatFormEcommerce.Etities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        //[HttpPost("registerShop")]
        //public async Task<IActionResult> RegisterShop(int userID,string shopName)
        //{
        // var account = await _context.TbUsers.FirstOrDefaultAsync(a => a.UserId == userID);
        //    if(account == null)
        //    {
        //        return Ok(new ErrorRespon
        //        {
        //            Error = true,
        //            Message = "Userid Have to shop"
        //        }) ;
        //    }
        //    var isShop = await _context.TbShops.FirstOrDefaultAsync(s => s.UserId == userID);
        //    if(isShop != null)
        //    {
        //        return Ok(new ErrorRespon
        //        {
        //            Error = false,
        //            Message ="UserId have to shop"
        //        });
        //    }
        //    var shop = new TbShop
        //    {
        //        ShopName = shopName,
        //        UserId = account.UserId
        //    };
        //    await _context.TbShops.AddAsync(shop);
        //    await _context.SaveChangesAsync();
        //    int shopid = shop.ShopId;
        //    var accountUser = await _context.TbUsers.FirstOrDefaultAsync(u => u.UserId == userID);
        //    if(accountUser != null)
        //    {
        //        accountUser.ShopId = shopid;
        //        await _context.SaveChangesAsync();
        //    }


        //    return Ok(new ErrorRespon
        //    {
        //        Message = "Register shop Success"
        //    });
        //}
        [HttpPost("registerShop")]
        public async Task<IActionResult> RegisterShop(string shopName,string address,string phone)
        {
            var userId = User.Identity.Name;
            if (userId == null)
            {
                return Ok(new ErrorRespon
                {
                    Message = "User Not Login"
                });
            }
            var user = await _context.TbShops.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                return Ok(new ErrorRespon
                {
                    Error = true,
                    Message = "User already has a shop account"
                });
            }
            var shop = new TbShop
            {
                UserId = userId,
                ShopName = shopName,
                Address = address,
                Phone = phone
            };
            await _context.TbShops.AddAsync(shop);
            await _context.SaveChangesAsync();
            int shopId = shop.ShopId;


            var accountUser = await _context.TbUsers.FirstOrDefaultAsync(s => s.UserId == userId);
            if (accountUser != null)
            {
                accountUser.ShopId = shopId;
                await _context.SaveChangesAsync();


            }
            return Ok(new ErrorRespon
            {
                Error = false,
                Message = "Shop account registered successfully "
            });
        }

        private int GetLoggedInUserId()
        {

            if (!User.Identity.IsAuthenticated)
            {

                return -1;
            }


            string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (string.IsNullOrEmpty(userIdClaim))
            {

                return -1;
            }


            if (!int.TryParse(userIdClaim, out int userId))
            {

                return -1;
            }


            return userId;
        }
    }
}
