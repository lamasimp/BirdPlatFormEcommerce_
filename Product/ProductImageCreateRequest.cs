namespace BirdPlatFormEcommerce.Product
{
    public class ProductImageCreateRequest
    {

        public bool? IsDefault { get; set; }
        public int? SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
