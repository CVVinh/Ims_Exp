using IMS_Example.Data.Models;

namespace IMS_Example.Data.DTOs.UserDTO
{
    public class UserInfoDTO
    {
        public int id { get; set; }
        public string userCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public UserInfoDTO(Users user)
        {
            this.id = user.id;
            this.userCode = user.userCode;
            this.firstName = user.firstName;
            this.lastName = user.lastName;
        }
    }
}
