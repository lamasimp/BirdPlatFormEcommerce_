namespace BirdPlatFormEcommerce.Product
{
    public class DetailShopViewProduct
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; } = null!;
        public string? Avatar { get; set; }

        public int? Rate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Address { get; set; }
        public int TotalProduct { get; set; }


    }
}
