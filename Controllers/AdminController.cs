using BirdPlatFormEcommerce.Etities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdPlatFormEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "AD")]
    public class AdminController : ControllerBase
    {
        private readonly SwpContext _context;

        public AdminController(SwpContext swp) {
            _context = swp;
        }
        [HttpGet]
        public  async  Task<IActionResult> getAlluser()
        {
            var user = _context.TbUsers.ToList();
            return Ok(user);
        }
    }
}
