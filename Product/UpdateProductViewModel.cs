using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BirdPlatFormEcommerce.Product
{
    public class UpdateProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;



        public decimal Price { get; set; }

        public string? Decription { get; set; }

        //    public string? Detail { get; set; }

        public decimal? DiscountPercent { get; set; }

        public int? Quantity { get; set; }

        public decimal? SoldPrice { get; set; }

        [BindNever]
        public IFormFile[]? ImageFile { get; set; }
        public int ShopId { get; set; }
    }
}
