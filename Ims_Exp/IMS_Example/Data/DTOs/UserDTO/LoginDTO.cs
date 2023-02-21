using System.ComponentModel.DataAnnotations;

namespace IMS_Example.Data.DTOs.UserDTO
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
