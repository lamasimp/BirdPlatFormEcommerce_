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
                        select new { p };

            var data = await query.Select(x => new HomeViewProductModel()
            {
                ProductId = x.p.ProductId,
                Name = x.p.Name,
                Status = x.p.Status,
                Price = x.p.Price,
                DiscountPercent = x.p.DiscountPercent,
                
                Decription = x.p.Decription,
                QuantitySold = x.p.QuantitySold,
                Rate = x.p.Rate,
              
            }).ToListAsync();
            return data;





        }



       

       
    }
}

