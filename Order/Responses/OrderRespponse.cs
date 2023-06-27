using BirdPlatFormEcommerce.IEntity;
using BirdPlatFormEcommerce.Payment.Responses;

namespace BirdPlatFormEcommerce.Order.Responses
{
    public class OrderRespponse
    {
        public int OrderId { get; set; }

        public bool? Status { get; set; }

        public int UserId { get; set; }

        public string? Note { get; set; }
        public int? AddressID { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual PaymentResponse? Payment { get; set; }

        public virtual ICollection<OrderDetailResponse> Items { get; set; } = new List<OrderDetailResponse>();

    }
    public class OrderResponses
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemResponse> Items { get; set; }
    }

    public class OrderItemResponse
    {
        public decimal Price { get; set; }
        public decimal? SoldPrice { get; set; }
        public string? ShopName { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
        public string? ImagePath { get; set; }
    }

    public class OrderDetailResponse
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int? Quantity { get; set; }

        public int? Discount { get; set; }

        public decimal? ProductPrice { get; set; }

        public decimal? DiscountPrice { get; set; }

        public decimal? SubTotal { get; set; }
    }
}
