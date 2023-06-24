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
}
