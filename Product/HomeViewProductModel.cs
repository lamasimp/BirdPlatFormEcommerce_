namespace BirdPlatFormEcommerce.Product
{
    public class HomeViewProductModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public bool? Status { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountPercent { get; set; }

        public string? Decription { get; set; }
        public int? QuantitySold { get; set; }


        public int? Rate { get; set; }



        

    }
}
