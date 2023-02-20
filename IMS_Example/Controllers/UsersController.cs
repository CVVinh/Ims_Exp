using IMS_Example.Data.Contexts;
using IMS_Example.Data.DTOs.UserDTO;
using IMS_Example.Data.Models;
using IMS_Example.Response;
using IMS_Example.Services.TokenServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMS_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenServices _tokenService;

        public UsersController(AppDbContext context, TokenServices tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "permission_group: True module: users")]
        public async Task<ActionResult<Users>> GetAll()
        {
            try
            {

                var users = await (from u in _context.Users
                                   from r in _context.Groups.Where(a => a.Id == u.IdGroup).DefaultIfEmpty()
                                   select new
                                   {
                                       id = u.id,
                                       userCode = u.userCode,
                                       userCreated = u.userCreated, // => userCode
                                       dateCreated = u.dateCreated,
                                       userModified = u.userModified, // => userCode
                                       dateModified = u.dateModified,
                                       firstName = u.firstName,
                                       lastName = u.lastName,
                                       phoneNumber = u.phoneNumber,
                                       dOB = u.dOB,
                                       gender = u.gender,
                                       address = u.address,
                                       university = u.university,
                                       yearGraduated = u.yearGraduated,
                                       email = u.email,
                                       skype = u.skype,
                                       workStatus = u.workStatus,
                                       dateStartWork = u.dateStartWork,
                                       dateLeave = u.dateLeave,
                                       maritalStatus = u.maritalStatus,
                                       reasonResignation = u.reasonResignation,
                                       identityCard = u.identitycard,
                                       idGroup = r.NameGroup,
                                       isDeleted = u.isDeleted,
                                   }).OrderByDescending(a => a.id).ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getUserByUserCode/{usercode}")]
        [Authorize(Roles = "permission_group: True module: users")]
        public async Task<ActionResult<Users>> GetUsersByUserCode(string usercode)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.Where(u => u.userCode.ToLower() == usercode.ToLower()).SingleOrDefaultAsync();
            if (users == null)
                return NotFound();
            var data = new GetAllDTO(users);
            return Ok(data);
        }

        [HttpGet("getInfo")]
        [Authorize(Roles = "permission_group: True module: users")]
        public async Task<ActionResult<UserInfoDTO>> GetInfo()
        {
            var users = await _context.Users.ToListAsync();
            var usersInfo = new List<UserInfoDTO>();
            if (users == null)
            {
                return NotFound();
            }
            foreach (var user in users)
            {
                usersInfo.Add(new UserInfoDTO(user));
            }
            return Ok(usersInfo);
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.userCode.ToLower() == loginDto.UserName.ToLower());
            if (user == null || user.isDeleted == 1)
            {
                return NotFound("Account does not exist !");
            }
            if (user.workStatus != 1)
            {
                return BadRequest("User has been locked !");
            }
            else
            {
                bool isPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.userPassword);
                if (isPassword)
                {
                    var accessToken = _tokenService.GenerateToken(user);
                    var refreshToken = _tokenService.GenerateRefreshToken();
                    user.refreshToken = refreshToken;
                    user.refreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                    await _context.SaveChangesAsync();
                    return Ok(new ApiResponseDTO
                    {
                        Success = true,
                        Message = "Authenticate success...",
                        Username = loginDto.UserName,
                        Token = accessToken,
                    });
                }
                return BadRequest("Wrong password !");
            }
        }



    }
}
