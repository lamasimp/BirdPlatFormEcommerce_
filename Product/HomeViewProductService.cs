using Azure.Core;
using BirdPlatFormEcommerce.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

                        orderby p.QuantitySold descending
                        select new { p, c };

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
                Thumbnail = x.p.Thumbnail
            }).ToListAsync();
            return data;
        }


        public async Task<List<HomeViewProductModel>> GetProductByRateAndQuantitySold()
        {
            var query = from p in _context.TbProducts
                        join s in _context.TbShops on p.ShopId equals s.ShopId
                        join c in _context.TbProductCategories on p.CateId equals c.CateId
                        orderby p.Rate descending, p.QuantitySold descending
                        where p.DiscountPercent >0
                        select new { p, c };

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
                Thumbnail = x.p.Thumbnail

            }).ToListAsync();
            return data;
        }

        public async Task<DetailProductViewModel> GetProductById(int productId)
        {
            var product = await _context.TbProducts.FindAsync(productId);
     //       var post = await _context.TbPosts.FirstOrDefaultAsync(x=>x.ProductId == productId);

            var image = await _context.TbImages.Where(x=>x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();
            var detailProductViewModel = new DetailProductViewModel()
            {
                ProductName = product.Name,          
                Price = product.Price,
                 DiscountPercent = (int)product.Price,
                SoldPrice = (int)Math.Round((decimal)(product.Price - product.Price / 100 * product.DiscountPercent)),
                Decription = product != null ? product.Decription : null,
                Detail = product != null ? product.Detail : null,
                Quantity = product.Quantity,
                ShopId = product.ShopId,
                CateId = product.CateId,
                
               CreateDate = DateTime.Now,
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

                        where p.ShopId.Equals(shopId)
                        select new { p, c };



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
                Thumbnail = x.p.Thumbnail
            }).ToListAsync();
            return data;
        }








    }
}

