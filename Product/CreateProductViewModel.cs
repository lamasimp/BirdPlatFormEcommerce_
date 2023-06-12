namespace BirdPlatFormEcommerce.Product
{
    public class CreateProductViewModel
    {
      

        public string ProductName { get; set; } = null!;

        //   public DateTime? CreateDate { get; set; }
       

        public decimal Price { get; set; }

        public decimal? DiscountPercent { get; set; }

        public decimal? SoldPrice { get; set; }
        public string? Decription { get; set; }

        public string? Detail { get; set; }

     
        public int? Quantity { get; set; }

        public int? ShopId { get; set; }
        public string ShopName { get; set; } = null!;
        public string CateId { get; set; } = null!;
        

     
        public DateTime? CreateDate { get; set; }

        public IFormFile  ThumbnailImage { get; set; }


       

      
    }
}
