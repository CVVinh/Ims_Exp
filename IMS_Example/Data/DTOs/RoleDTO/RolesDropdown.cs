
using IMS_Example.Data.Models;

namespace IMS_Example.Data.DTOs.RoleDTO
{
    public class RolesDropdown
    {
        public int idRole { get; set; }
        public string roleName { get; set; }
        public RolesDropdown(Roles role)
        {
            idRole = role.idRole;
            roleName = role.nameRole;
        }
    }
}
