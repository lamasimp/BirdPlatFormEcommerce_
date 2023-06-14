using Azure.Core;
using BirdPlatFormEcommerce.Etities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace BirdPlatFormEcommerce.Product
{
    public class HomeViewProductService :IHomeViewProductService
    {
        private readonly SwpContext _context;

        public HomeViewProductService(SwpContext context)
        {
            _context = context;
        }
        public async Task<List<HomeViewProductModel>> GetAllByQuantitySold()
        {
            var query = from p in _context.TbProducts
                        join s in _context.TbShops on p.ShopId equals s.ShopId
                        join c in _context.TbProductCategories on p.CateId equals c.CateId
                        join img in _context.TbImages on p.ProductId equals img.ProductId 
                        where img.Caption=="Thumbnail"
                        orderby p.QuantitySold descending
                        select new { p, c ,img};

            var data = await query.Select(x => new HomeViewProductModel()
            {
                ProductId = x.p.ProductId,
                ProductName = x.p.Name,
                CateName = x.c.CateName,
                Status = x.p.Status,
                Price = x.p.Price,
                DiscountPercent = x.p.DiscountPercent,
                SoldPrice = (int)Math.Round((decimal)(x.p.Price - x.p.Price / 100 * x.p.DiscountPercent)),
                QuantitySold = x.p.QuantitySold,
                Rate = x.p.Rate,
                Thumbnail = x.img != null ? x.img.ImagePath : "no-image.jpg",
            }).ToListAsync();
            return data;
        }


        public async Task<List<HomeViewProductModel>> GetProductByRateAndQuantitySold()
        {
            var query = from p in _context.TbProducts
                        join s in _context.TbShops on p.ShopId equals s.ShopId
                        join c in _context.TbProductCategories on p.CateId equals c.CateId
                        join img in _context.TbImages on p.ProductId equals img.ProductId
                        orderby p.Rate descending, p.QuantitySold descending
                        where p.DiscountPercent >0
                        select new { p, c ,img};

            var data = await query.Select(x => new HomeViewProductModel()
            {
                ProductId = x.p.ProductId,
                ProductName = x.p.Name,
                CateName = x.c.CateName,
                Status = x.p.Status,
                Price = x.p.Price,
                DiscountPercent = x.p.DiscountPercent,
                SoldPrice = (int)Math.Round((decimal)(x.p.Price - x.p.Price / 100 * x.p.DiscountPercent)),
                QuantitySold = x.p.QuantitySold,
                Rate = x.p.Rate,
                Thumbnail = x.img != null ? x.img.ImagePath : "no-image.jpg",

            }).ToListAsync();
            return data;
        }

        public async Task<DetailProductViewModel> GetProductById(int productId)
        {
            var product = await _context.TbProducts.FindAsync(productId);
            var image = await _context.TbImages.Where(x=>x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();
            var shop = await (from s in _context.TbShops
                              join p in _context.TbProducts on s.ShopId equals p.ShopId
                              where p.ProductId == productId
                              select s).FirstOrDefaultAsync();
            var cate =  await (from c in _context.TbProductCategories 
                       join p in _context.TbProducts on c.CateId equals p.CateId
                       where p.ProductId == productId
                       select c).FirstOrDefaultAsync();


            var detailProductViewModel = new DetailProductViewModel()
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                DiscountPercent = (int)product.DiscountPercent,
                SoldPrice = (int)Math.Round((decimal)(product.Price - product.Price / 100 * product.DiscountPercent)),
                Decription = product != null ? product.Decription : null,
                Detail = product != null ? product.Detail : null,
                Quantity = product.Quantity,
                ShopId = product.ShopId,
               ShopName = shop.ShopName,
               UserId = shop.UserId,
                CateId = product.CateId,
                CateName = cate.CateName,
               CreateDate = DateTime.Now,
               QuantitySold = product.QuantitySold,
               ShopRate = shop.Rate,
               ThumbnailImage = image !=null ? image.ImagePath : "no-image.jpg",


            };
            return detailProductViewModel;
        }

        public async Task<List<HomeViewProductModel>> GetProductByShopId(int shopId)
        {
            var product = await _context.TbShops.FindAsync(shopId);
            if (shopId == 0) throw new Exception("Can not find shopId");
            var query = from p in _context.TbProducts
                        join s in _context.TbShops on p.ShopId equals s.ShopId
                        join c in _context.TbProductCategories on p.CateId equals c.CateId
                        join img in _context.TbImages on p.ProductId equals img.ProductId
                        where p.ShopId.Equals(shopId)
                        select new { p, c,img };



            var data = await query.Select(x => new HomeViewProductModel()
            {

                ProductId = x.p.ProductId,
                ProductName = x.p.Name,
                CateName = x.c.CateName,
                Status = x.p.Status,
                Price = x.p.Price,
                DiscountPercent = x.p.DiscountPercent,
                SoldPrice = (int)Math.Round((decimal)(x.p.Price - x.p.Price / 100 * x.p.DiscountPercent)),
                QuantitySold = x.p.QuantitySold,
                Rate = x.p.Rate,
                Thumbnail = x.img != null ? x.img.ImagePath : "no-image.jpg",
            }).ToListAsync();
            return data;
        }

        public async Task<List<HomeViewProductModel>> GetAllProduct()
        {
            var query = from p in _context.TbProducts
                        join s in _context.TbShops on p.ShopId equals s.ShopId
                        join c in _context.TbProductCategories on p.CateId equals c.CateId
                        join img in _context.TbImages on p.ProductId equals img.ProductId
                        where img.Caption == "Thumbnail"
                        select new { p, c ,img};

            var data = await query.Select(x => new HomeViewProductModel()
            {
                ProductId = x.p.ProductId,
                ProductName = x.p.Name,
                CateName = x.c.CateName,
                Status = x.p.Status,
                Price = x.p.Price,
                DiscountPercent = x.p.DiscountPercent,
                SoldPrice = (int)Math.Round((decimal)(x.p.Price - x.p.Price / 100 * x.p.DiscountPercent)),
                QuantitySold = x.p.QuantitySold,
                Rate = x.p.Rate,
                Thumbnail = x.img != null ? x.img.ImagePath : "no-image.jpg",

            }).ToListAsync();
            return data;
        }
    }
}

