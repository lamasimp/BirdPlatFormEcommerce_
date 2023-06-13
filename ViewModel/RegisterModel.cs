namespace BirdPlatForm.ViewModel
{
    public class RegisterModel
    {
     
        public string Gender { get; set; }     
        public string? Email { get; set; }

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string RoleId = "CUS";

        
    }
}
