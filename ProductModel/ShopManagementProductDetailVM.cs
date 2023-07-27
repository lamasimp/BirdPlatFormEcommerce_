using System.ComponentModel.DataAnnotations;

namespace BirdPlatFormEcommerce.Product
{
    public class ShopManagementProductDetailVM
    {
        public int ProductId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        public string ProductName { get; set; }



        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        public decimal? DiscountPercent { get; set; }

        public decimal? SoldPrice { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MinLength(100, ErrorMessage = "Desciption must be at least 100 characters")]
        public string? Decription { get; set; }


        //     public string? Detail { get; set; }

        //    [Required(ErrorMessage = "Quantity is required")]
        public int? Quantity { get; set; }


        public string CateId { get; set; }


        public List<string> Images { get; set; }
        public int ShopId { get; set; }


    }
}
