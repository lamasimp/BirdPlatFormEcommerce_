
using BirdPlatFormEcommerce.Etities;
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
       private readonly IManageProductService _manageProductService;


        public ProductsController(SwpContext context, IHomeViewProductService homeViewProductService, IManageProductService manageProductService)
        {
            _context = context;
            _homeViewProductService = homeViewProductService;
           _manageProductService = manageProductService;
        }

        [HttpGet("BestSeller_Product")]
        public async Task<IActionResult> GetByQuantitySold()
        {
            var product = await _homeViewProductService.GetAllByQuantitySold();
            return Ok(product);
        }

        [HttpGet("Hot_Product")]
        public async Task<IActionResult> GetProductByRateAndQuantitySold()
        {
            {
                var product = await _homeViewProductService.GetProductByRateAndQuantitySold();
                return Ok(product);
            }

        }

        [HttpGet("Product_ShopId")]
        public async Task<IActionResult> GetProductByShopId(int shopId)
        {
            var product = await _homeViewProductService.GetProductByShopId(shopId);
            if (product == null)

                return BadRequest("Cannot find product");

            return Ok(product);
        }

        [HttpGet("All_Product")]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _homeViewProductService.GetAllProduct();
            return Ok(product);
        }

        [HttpGet("detail_product")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _homeViewProductService.GetProductById(id);
            if (product == null)

                return BadRequest("cannot find product");

            return Ok(product);
        }

        [HttpPost]
        [Route("Add_Product")]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductViewModel request)
        {
            var productId = await _manageProductService.Create(request);
            if (productId == 0)
                return BadRequest();
            var product = await _homeViewProductService.GetProductById(productId);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, product);
        }

    }
}

