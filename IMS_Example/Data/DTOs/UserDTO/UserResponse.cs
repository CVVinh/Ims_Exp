using IMS_Example.Data.Models;

namespace IMS_Example.Data.DTOs.UserDTO
{
    public class UserResponse
    {
        public List<Users> Users { get; set; } = new List<Users>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
