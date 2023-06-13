using BirdPlatFormEcommerce.Etities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BirdPlatFormEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthorController : ControllerBase
    {
        private readonly SwpContext _context;

        public AuthorController(SwpContext swp)
        {
            _context = swp;
        }
        [HttpPost]
        [Route("logout")]
        [Authorize] 
        public async Task<IActionResult> Logout()
        {
            
            var userClaims = User.Claims.ToList();

            
            var tokenClaim = userClaims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (tokenClaim != null)
            {
                userClaims.Remove(tokenClaim);
            }

           

            return Ok();
        }

    }
}
