namespace BirdPlatForm.ViewModel
{
    public class RegisterModel
    {
        public DateTime Dob { get; set; }

        public string Gender { get; set; }     
        public string? Email { get; set; }

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string RoleId { get; set; } = null!;

        public DateTime? UpdateDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public byte[] Avatar { get; set; }

        public string? Phone { get; set; }

        public string Address { get; set; }

        public int ShopId { get; set; }
    }
}
