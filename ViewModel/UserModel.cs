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

       public DateTime? birth { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }

        }

}
