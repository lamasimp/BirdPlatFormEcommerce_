namespace BirdPlatFormEcommerce.Product
{
    public class DetailProductViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        //   public DateTime? CreateDate { get; set; }
        public bool? Status { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountPercent { get; set; }

        public decimal? SoldPrice { get; set; }
        public string? Decription { get; set; }

        public string? Detail { get; set; }

        public int? QuantitySold { get; set; }


        public int? ShopId { get; set; }
        public string CateId { get; set; } = null!;
        public string? Thumbnail { get; set; }

        public int? Quantity { get; set; }


        public string ShopName { get; set; } = null!;

        public int? Rate { get; set; }

       

      
    }
}
