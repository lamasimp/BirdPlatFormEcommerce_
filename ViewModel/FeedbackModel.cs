namespace BirdPlatForm.ViewModel
{
    public class FeedbackModel
    {


        public int? ProductId { get; set; }
        public int? Rate { get; set; }

        public string   Detail { get; set; }
        public int? OrderDetailId { get; set; }
        public IFormFile[] ImageFile { get; set; }
    }
    public class FeedbackReponse
    {
        public int ProductId { get; set; }
        public int Rate { get; set; }
        public string? Detail { get; set; }
        public string UserName { get; set; }
        public List<string?> imgFeedback { get; set; }
        public string imgAvatar { get; set; }
        public DateTime CreateDate { get; set; }

    }
    public class FeedbackUser
    {
        public int feedbackID { get; set; }
        public string Username { get; set; }
        public int rate { get; set; }
        public string Detail { get; set; }
        public DateTime CreateDate { get; set; }
        public List<string> imgFeedback { get; set; }
        public string imgAvatar { get; set; }
        public int productId { get; set; }
        public int Quantity { get; set; }
        public string productName { get; set; }
        public string imgProduct { get; set; }
    }
    public class FeedbackAdmin
    {
        public int ProductId { get; set; }
        public int Rate { get; set; }
        public string? ImageProduct { get; set; }
        public string productName { get;set; }
        public decimal Price { get; set; }
       public string shopName { get; set; }
      public int FeedbackCount { get; set; }

    }
    public class QuantitySold
    {
        public int ProductId { get; set; }
        public int Rate { get; set; }
        public string? ImageProduct { get; set; }
        public string productName { get; set; }
        public decimal Price { get; set; }
        public string shopName { get; set; }
        public int Quantitysold { get; set; }

    }
    public class ListtotalOrder
    {
        public totalOrderCha TotalOrderCha { get; set; } 
        public TotalAmountSystem AmountSystem { get; set; } 
        public ProfitAdmin Profit { get; set; } 
        public refundAmountShop refundAmout { get; set; } 
    }
    public class totalOrderCha
    {
        public int countOrder { get; set; }
        public decimal totalPriceOrdercha { get; set; }
        public decimal price1Ordercha { get; set; }
    }
    public class TotalAmountSystem
    {
        public decimal totalAmountSystem { get; set; }
    }
    public class ProfitAdmin
    {
        public decimal profitAdmin { get; set; }
    }
    public class refundAmountShop
    {
        public decimal refundamout { get; set; }
    }

}
