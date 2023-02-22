using IMS_Example.Data.Models;

namespace IMS_Example.Data.DTOs.UserDTO
{
    public class DropdownUserDTO
    {
        public int id { get; set; }
        public string userCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DropdownUserDTO(Users user)
        {
            id = user.id;
            userCode = user.userCode;
            firstName = user.firstName;
            lastName = user.lastName;
        }
    }
}
