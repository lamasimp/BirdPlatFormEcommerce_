using BirdPlatFormEcommerce.Payment;
using System.ComponentModel.DataAnnotations;

namespace BirdPlatFormEcommerce.Payment.Requests
{
    public class PayOrderModel
    {
        [Required]
        public PaymentMethod Method { get; set; }

    }
}
