using BirdPlatForm.UserRespon;
using BirdPlatForm.ViewModel;
using BirdPlatFormEcommerce.Helper.Mail;
using BirdPlatFormEcommerce.NEntity;
using BirdPlatFormEcommerce.ViewModel;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace BirdPlatFormEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "AD")]
    public class AdminController : ControllerBase
    {
        private readonly SwpDataBaseContext _context;
        private readonly IMailService _mailService;

        public AdminController(SwpDataBaseContext swp, IMailService mailService)
        {
            _context = swp;
            _mailService = mailService;
        }
        [HttpGet]
        public async Task<IActionResult> getAlluser()
        {
            var user = _context.TbUsers.ToList();
            return Ok(user);
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdate user)

        {
            var update = await _context.TbUsers.FindAsync(id);
            if (update != null)
            {
                update.Dob = user.Dob;
                update.Gender = user.Gender;
                update.Name = user.Name;
                update.CreateDate = user.CreateDate;
                update.UpdateDate = user.UpdateDate;
                update.Avatar = user.Avatar;
                update.Phone = user.Phone;
                update.Address = user.Address;
                await _context.SaveChangesAsync();
                return Ok(update);
            }
            return BadRequest("Faill ");
        }


        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserByid(int id)
        {
            var user = await _context.TbUsers.FindAsync(id);
            if (user == null)
            {
                return Ok(new ErrorRespon
                {
                    Error = false,
                    Message = "No User :("
                });
            }
            return Ok(user);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Deleteacount(int Id)
        {
            var tokens = _context.TbTokens.Where(t => t.Id == Id).ToList();
            if (tokens == null)
            {
                return null;
            }

            _context.TbTokens.RemoveRange(tokens);
            var user = await _context.TbUsers.FindAsync(Id);


            if (user != null)
            {
                _context.TbUsers.Remove(user);
            }

            _context.SaveChanges();

            return Ok("Delete Success");

        }
        [HttpGet("CountSellingProducts")]
        public async Task<IActionResult> CountSellingProducts()
        {
            var count = await countProduct();
            return Ok(count);

        }
        private async Task<int> countProduct()
        {
            var count = await _context.TbProducts.CountAsync(x => x.Status.HasValue && x.Status.Value == true);

            return count;
        }
        [HttpGet("GetCustomer")]
        public async Task<IActionResult> GetCustomer()
        {
            var countCus = await CountCus();
            return Ok(countCus);
        }
        private async Task<int> CountCus()
        {
            var countcus = await _context.TbUsers.CountAsync(x => x.RoleId == "CUS");
            return countcus;
        }
        [HttpGet("GetShop")]
        public async Task<IActionResult> GetShop()
        {
            var countshop = await CountShop();
            return Ok(countshop);
        }
        private async Task<int> CountShop()
        {
            var countcus = await _context.TbUsers.CountAsync(x => x.RoleId == "SP");
            return countcus;
        }
        [HttpGet("Product/shop")]
        public async Task<IActionResult> GetProductShop(int shopId)
        {
            var pro = await _context.TbProducts.CountAsync(x => x.ShopId == shopId);
            return Ok(pro);
        }
        [HttpGet("TotalAmount/HighShop")]
        public List<ShoptotalAmount> gettotalAmounthighShop()
        {
            var shopTotalAmounts = _context.TbShops
        .Join(_context.TbProducts,
            shop => shop.ShopId,
            product => product.ShopId,
            (shop, product) => new { Shop = shop, Product = product })
        .Join(_context.TbOrderDetails,
            joinResult => joinResult.Product.ProductId,
            orderDetail => orderDetail.ProductId,
            (joinResult, orderDetail) => new { Shop = joinResult.Shop, OrderDetail = orderDetail })
        .GroupBy(result => result.Shop.ShopId)
        .Select(g => new ShoptotalAmount
        {
            shopId = g.Key,
            TotalAmount = (decimal)g.Sum(result => result.OrderDetail.Quantity * result.OrderDetail.Product.Price)
        })
        .OrderByDescending(sta => sta.TotalAmount)
        .ToList();
            return shopTotalAmounts;
        }

        [HttpGet("CountReport")]
        public async Task<IActionResult> Countreport()
        {
            var shopReportCounts = await _context.TbShops
                .Select(s => new ReportModel
                {
                    shopId = s.ShopId,
                    Shopname = s.ShopName,
                    Count = _context.TbReports.Count(r => r.ShopId == s.ShopId)
                })
                .ToListAsync();


            return Ok(shopReportCounts);


        }
        [HttpGet("getreport")]
        public async Task<IActionResult> getreportShop(int shopid)
        {
            var shop = await _context.TbShops.FindAsync(shopid);
            if (shop == null)
            {
                return BadRequest("No shop");
            }
            var report = await _context.TbReports.Include(r => r.CateRp)
                .Where(r => r.ShopId == shopid)
                .Select(r => new ShopreportModel
                {
                    reportID = r.ReportId,
                    detail = r.Detail,
                    DetailCate = r.CateRp.Detail

                })
                .ToListAsync();
            var shopreport = new Shopreport
            {
                shopId = shop.ShopId,
                shopname = shop.ShopName,
                reports = report
            };

            return Ok(shopreport);


        }
        [HttpPost("Sendwarning")]
        public async Task<IActionResult> SendwarningShop(int shopid)
        {
            var shop = _context.TbShops.Find(shopid);
            if (shop == null) { return BadRequest("Shop not found"); }

            var user = await _context.TbUsers.FindAsync(shop.UserId);
            if (user == null) { return NotFound(); }

            string email = user.Email;

            var reports = await _context.TbReports
                .Include(r => r.CateRp)
                .Where(r => r.ShopId == shop.ShopId)
                .ToListAsync();

            if (reports.Count >= 1 && reports.Count <= 3)
            {
                var emailBody = $"Shop Name: {shop.ShopName}\n\n";
                emailBody += " Cảnh báo lần đầu tiên dành cho shop của nếu quá 3 lần report tài khoản của bạn sẽ bị khóa:\n" +
                    "Mọi thắc mắc hãy liên hệ với chúng tôi.\n";

                foreach (var report in reports)
                {
                    emailBody += $"- Report ID: {report.ReportId}\n";
                    emailBody += $"  Detail: {report.Detail}\n";
                    emailBody += $"  DetailCategory: {report.CateRp.Detail}\n";
                }

                var mailRequest = new MailRequest()
                {
                    ToEmail = email,
                    Subject = "[BIRD TRADING PLATFORM] Cảnh cáo tới shop của bạn",
                    Body = emailBody
                };

                await _mailService.SendEmailAsync(mailRequest);
            }

            if (reports.Count > 3)
            {
                user.Status = true;
                _context.TbUsers.Update(user);
                await _context.SaveChangesAsync();

                var mailRequest = new MailRequest()
                {
                    ToEmail = email,
                    Subject = "[BIRD TRADING PLATFORM] Tài khoản của bạn đã bị khóa",
                    Body = "Tài khoản của bạn đã bị khóa do vi phạm quy định của chúng tôi. Mọi thắc mắc hãy liên hệ với chúng tôi." +
                    "Email: longnhatlekk@gmail.com"
                };

                await _mailService.SendEmailAsync(mailRequest);
            }

            return Ok("Warning email sent successfully.");
        }

    }
}
