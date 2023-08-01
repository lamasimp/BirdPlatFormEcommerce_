using System.ComponentModel.DataAnnotations;

namespace BirdPlatFormEcommerce.ProductModel
{
    public class CreateProductViewModel
    {



      
        public string ProductName { get; set; }



        
        public decimal Price { get; set; }

        public decimal? DiscountPercent { get; set; }

        public decimal? SoldPrice { get; set; }
       
        public string? Decription { get; set; }

       
        //     public string? Detail { get; set; }

        //    [Required(ErrorMessage = "Quantity is required")]
        public int? Quantity { get; set; }


        public string CateId { get; set; }


        public IFormFile[] ImageFile { get; set; }


        public int ShopId { get; set; }
    }
}
