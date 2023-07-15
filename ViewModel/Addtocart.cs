namespace BirdPlatFormEcommerce.ViewModel
{
    public class Addtocart
    {
        
            
        public int? Quantity { get; set; }
     
        
        public int? ProductID { get; set; }
    }
    public class ViewCart
    {
        public string productName { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int quantityCart { get; set; }
        public decimal PriceCart { get; set; }
        public int PriceProduct { get; set; }
        public int quantityProduct { get; set; }
        public string ImageProduct { get; set; }
    }
    public class Updatequantity
    {
        public int cartID { get; set; }
        public int quantity { get; set; }
        
        
    }
}
