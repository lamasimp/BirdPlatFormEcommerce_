namespace BirdPlatFormEcommerce.ViewModel
{
    public class UserModel
    {
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public IFormFile avatar { get; set; }
    }
    public class Customer
        {
        public int UserId { get; set; }
       public DateTime? birth { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public bool Status { get; set; }
        }
    public class ShopAdmin
    {
        public int UserId { get; set; }
        public DateTime? birth { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int ShopId { get; set; }
        public string PhoneHome { get; set; }
        public string AddressHome { get; set; }
        public string Avatar { get; set; }
        public string ShopName { get; set; }
        public string AddressShop { get; set; }
        public string PhoneShop { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public decimal ?TotalRevenue { get; set; }

        public int TotalProducct { get; set; }

        public int? RateShop { get; set; }

        public int? TotalQuantitySold { get; set; }

    }

}
