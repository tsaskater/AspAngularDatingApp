using System.ComponentModel.DataAnnotations;

namespace API.Controllers.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string password { get; set; }
    }
}