using System.ComponentModel.DataAnnotations;

namespace API.Controllers.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string password { get; set; }
    }
}