using BirdPlatFormEcommerce.Entities;
using Microsoft.EntityFrameworkCore;

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
                SoldPrice = (int)Math.Round((decimal)(x.p.Price * x.p.DiscountPercent + x.p.Price)),
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
                        select new { p, c };

            var data = await query.Select(x => new HomeViewProductModel()
            {
                ProductId = x.p.ProductId,
                ProductName = x.p.Name,
                CateName = x.c.CateName,
                Status = x.p.Status,
                Price = x.p.Price,
                DiscountPercent = x.p.DiscountPercent,
                SoldPrice = (int)Math.Round((decimal)(x.p.Price * x.p.DiscountPercent + x.p.Price)),
                QuantitySold = x.p.QuantitySold,
                Rate = x.p.Rate,
                Thumbnail = x.p.Thumbnail

            }).ToListAsync();
            return data;
        }

        //public async Task<DetailProductViewModel> GetProductById(int productId)
        //{
        //    var product = await _context.TbProducts.FindAsync(productId);


        //    var detailProductViewModel = new DetailProductViewModel()
        //    {
        //        ProductId = product.ProductId,
        //        Name = product.Name,
        //        //            CreateDate = post.CreateDate,
        //        Status = product.Status,
        //        Price = product.Price,
        //        DiscountPercent = (int)product.Price 
        //        Decription = product != null ? product.Decription : null,
        //        Detail = product != null ? product.Detail : null,
        //        QuantitySold = product.QuantitySold,
        //        ShopId = product.ShopId,
        //        Rate = product.Rate,
        //        Video = product.Video,
        //        Image = product.Image,
        //        Image1 = product.Image1,
        //        Image2 = product.Image2,
        //        Image3 = product.Image3,
        //        Image4 = product.Image4
        //    };
        //    return detailProductViewModel;
        //}

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
                SoldPrice = (int)Math.Round((decimal)(x.p.Price * x.p.DiscountPercent + x.p.Price)),
                QuantitySold = x.p.QuantitySold,
                Rate = x.p.Rate,
                Thumbnail = x.p.Thumbnail
            }).ToListAsync();
            return data;
        }








    }
}

