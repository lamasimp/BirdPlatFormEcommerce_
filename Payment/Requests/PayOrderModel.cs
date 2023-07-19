using BirdPlatFormEcommerce.Payment;
using System.ComponentModel.DataAnnotations;

namespace BirdPlatFormEcommerce.Payment.Requests
{
    public class PayOrderModel
    {
        [Required]
        public int ParentOrderID { get; set; }
        [Required]
        public PaymentMethod Method { get; set; }

    }
}
