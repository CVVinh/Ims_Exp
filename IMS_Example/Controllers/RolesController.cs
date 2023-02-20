using IMS_Example.Data.Contexts;
using IMS_Example.Data.DTOs.RoleDTO;
using IMS_Example.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMS_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public RolesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Methods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetAll()
        {
            return await _appDbContext.Roles.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<Roles>>> GetID(int id)
        {
            var role = await _appDbContext.Roles.Where(d => d.idRole == id).SingleOrDefaultAsync();
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpGet]
        [Route("getRoleDropdown")]
        public async Task<ActionResult<IEnumerable<RolesDropdown>>> getRolesDropdown()
        {
            var roles = await _appDbContext.Roles.ToListAsync();
            var rolesDropdown = new List<RolesDropdown>();
            foreach (var role in roles)
            {
                rolesDropdown.Add(new RolesDropdown(role));
            }
            return Ok(rolesDropdown);
        }

        #endregion

    }
}
