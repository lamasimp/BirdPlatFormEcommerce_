
using BirdPlatForm.UserRespon;
using BirdPlatForm.ViewModel;
using BirdPlatFormEcommerce.NEntity;
using BirdPlatFormEcommerce.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdPlatForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly SwpDataBaseContext _context;
        private readonly IWebHostEnvironment _enviroment;

        public FeedBackController(SwpDataBaseContext bird, IWebHostEnvironment enviroment)
        {
            _context = bird;
            _enviroment = enviroment;
        }
        [HttpPost("Feedback")]
        public async Task<IActionResult> Feedback([FromForm] FeedbackModel feedback)
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
                ProductId = (int)feedback.ProductId,
                UserId = userid,
                Rate = feedback.Rate,
                FeedbackDate = DateTime.Now

            };

            if (!string.IsNullOrEmpty(feedback.Detail))
            {
                tbfeedback.Detail = feedback.Detail;
            }
            _context.TbFeedbacks.Add(tbfeedback);
            await _context.SaveChangesAsync();

            //add image
            if (feedback.ImageFile != null&& feedback.ImageFile.Length > 0)
            {
                int passcount = 0;

                int maxImageCount = 6;


                string Filepath = GetFileProductPath(tbfeedback.Id);
                if (!Directory.Exists(Filepath))
                {
                    Directory.CreateDirectory(Filepath);
                }
                int imageCount = 0;
                if (feedback.ImageFile.Length > 0)
                {
                    foreach (var file in feedback.ImageFile)
                    {
                        if (imageCount >= maxImageCount)
                        {
                            break;
                        }

                        var image = new TbFeedbackImage()
                        {
                            FeedbackId = tbfeedback.Id,


                            ImagePath = GetImageProductPath(tbfeedback.Id, file.FileName),



                        };
                        string imagepath = Path.Combine(Filepath, file.FileName);
                        if (System.IO.File.Exists(imagepath))
                        {
                            System.IO.File.Delete(imagepath);
                        }
                        using (FileStream stream = System.IO.File.Create(imagepath))
                        {
                            await file.CopyToAsync(stream);
                            passcount++;
                        }
                        _context.Add(image);
                        imageCount++;

                    }
                }
                await _context.SaveChangesAsync();
            }
           


            var orderDetail = await _context.TbOrderDetails.FindAsync(feedback.OrderDetailId);
            orderDetail.ToFeedback = true;
            await _context.SaveChangesAsync();

            var rate = await GetProductAverageRates((int)feedback.ProductId);
            var updateRate = await _context.TbProducts.FirstOrDefaultAsync(u => u.ProductId == feedback.ProductId);
            {
                updateRate.Rate = (int?)rate;

                await _context.SaveChangesAsync();
            }
            
            var rateshop = await GetShopAverageRates((int)updateRate.ShopId);
            var updateRateShop = await _context.TbShops.FirstOrDefaultAsync(u => u.ShopId == updateRate.ShopId);
            {
                updateRateShop.Rate = (int?)rateshop;
                await _context.SaveChangesAsync();
            }



            return Ok("success");
            
        }
        [HttpGet]
        public async Task<IActionResult> getFeedBack(int productID)
        {
            var feedback = _context.TbFeedbacks
                .Include(u => u.TbFeedbackImages)
                .Include(u => u.User)
                .Where(u => u.ProductId == productID)
                .Select(u => new FeedbackReponse {
                    ProductId = productID,
                    Rate = (int)u.Rate,
                    Detail = u.Detail,
                    UserName = u.User.Name,
                    CreateDate = u.FeedbackDate,
                    imgAvatar = u.User.Avatar,
                    imgFeedback = u.TbFeedbackImages.Where(f => f.FeedbackId == u.Id)
                    .Select(f => f.ImagePath).ToList(),
                })
                .ToList();
            
            return Ok(feedback);
        }

        [HttpGet("getFeedbackuser")]
        public async Task<IActionResult> GetfeedbackUser()
        {
            var useridClaim = User.Claims.FirstOrDefault(u => u.Type == "UserId");
            if (useridClaim == null) return Unauthorized("No user");
            int userid = int.Parse(useridClaim.Value);
            var feedback = await _context.TbFeedbacks
                .Include(u => u.TbFeedbackImages)
                .Include(u => u.User)
                .Include(u => u.Product)
                .ThenInclude(u => u.TbImages)
                .Where(u => u.UserId == userid)
                .Select(u => new FeedbackUser
                {
                    feedbackID = u.Id,
                    Username = u.User.Name,
                    imgAvatar = u.User.Avatar,
                    Quantity = (int)u.Product.Quantity,
                    rate =(int) u.Rate,
                    Detail = u.Detail,
                    CreateDate = u.FeedbackDate,
                    imgFeedback = u.TbFeedbackImages.Where(f => f.FeedbackId == u.Id)
                    .Select(f => f.ImagePath).ToList(),
                    productId = u.Product.ProductId,
                    productName = u.Product.Name,
                    imgProduct = u.Product.TbImages.FirstOrDefault().ImagePath,


                }).ToListAsync();
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

        private string GetFileProductPath(int productId)
        {
            return this._enviroment.WebRootPath + "\\user-content\\feedback\\" + productId.ToString();
        }


        private string GetImageProductPath(int productId, string fileName)
        {
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            return hosturl + "/user-content/feedback/" + productId + "/" + fileName;

        }
    }
}
