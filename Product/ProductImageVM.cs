namespace BirdPlatFormEcommerce.Product
{
    public class ProductImageVM
    {
        public int ProductId { get; set; }
        public int ImageId { get; set; }

        public long? FileSize { get; set; }
        public bool? IsDefault { get; set; }
        public int? SortOrder { get; set; }
        public string ImagePath { get; set; }
    }
}
