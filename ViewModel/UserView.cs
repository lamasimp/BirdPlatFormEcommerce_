using System.ComponentModel.DataAnnotations;

namespace BirdPlatForm.ViewModel
{
    public class UserView
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
