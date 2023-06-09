using BirdPlatFormEcommerce.Entities;
using BirdPlatFormEcommerce.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdPlatFormEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SwpContext _context;
        private readonly IHomeViewProductService _homeViewProductService;
  //      private readonly IManageProductService _manageProductService;


        public ProductsController(SwpContext context, IHomeViewProductService homeViewProductService)
        {
            _context = context;
            _homeViewProductService = homeViewProductService;
  //          _manageProductService = manageProductService;
        }

        [HttpGet("BestSeller_Product")]
        public async Task<IActionResult> GetByQuantitySold()
        {
            var product = await _homeViewProductService.GetAllByQuantitySold();
            return Ok(product);
        }


    }
}

