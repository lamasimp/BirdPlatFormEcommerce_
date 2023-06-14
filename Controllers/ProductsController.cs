
using BirdPlatFormEcommerce.Etities;
using BirdPlatFormEcommerce.FileService;
using BirdPlatFormEcommerce.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace BirdPlatFormEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SwpContext _context;
        private readonly IHomeViewProductService _homeViewProductService;
        private readonly IManageProductService _manageProductService;
        private readonly IWebHostEnvironment _enviroment;
        private readonly IStorageService _storageService;

        public ProductsController(SwpContext context, IHomeViewProductService homeViewProductService, IManageProductService manageProductService,
            IWebHostEnvironment environment, IStorageService storageService)
        {
            _context = context;
            _homeViewProductService = homeViewProductService;
            _manageProductService = manageProductService;
            _enviroment = environment;
            _storageService = storageService;
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





        [HttpPost]
        [Route("Add_Product_Image")]
        public async Task<IActionResult> AddProductImage([FromForm] ProductImageCreateRequest request, int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var image = new TbImage()
                {
                   
                    Caption = "Image",
                    CreateDate = DateTime.Now,
                    IsDefault = request.IsDefault,
                    SortOrder = request.SortOrder,
                    ProductId = productId,

                    ImagePath = GetImagePath(productId),



                };
                string Filepath = GetFilePath(productId);
                if (!System.IO.File.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }

                string imagepath = Path.Combine(Filepath, productId + ".png");
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await request.ImageFile.CopyToAsync(stream);


                }
                _context.Add(image);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }


        [HttpGet("Image_ProductID")]
        public async Task<IActionResult> GetImageById(int productId)
        {
            var image = await _context.TbImages.FindAsync(productId);
            var tb_image = await _context.TbImages.Where(x => x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();

            if (image == null)

                throw new Exception("can not find an image with id");

            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {

                string Filepath = GetFilePath(productId);
                string imageUrl = Path.Combine(Filepath, productId + ".png");
                if (System.IO.File.Exists(imageUrl))
                {
                    var productImageVM = new ProductImageVM()
                    {
                        ImageId = tb_image.Id,
                        ProductId = productId,
                        FileSize = image.FileSize,
                        IsDefault = image.IsDefault,
                        SortOrder = image.SortOrder,
                        ImagePath = hosturl + "/user-content/" + productId + "/" + productId + ".png"

                    };
                    return Ok(productImageVM);
                }



            }
            catch (Exception ex)
            {

            }
            return NotFound();




        }

        [NonAction]
        private string GetFilePath(int productId)
        {
            return this._enviroment.WebRootPath + "\\user-content\\" + productId.ToString();
        }

        private string GetImagePath(int productId)
        {
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            return hosturl + "/user-content/" + productId + "/" + productId + ".png";
        }

    }
}

