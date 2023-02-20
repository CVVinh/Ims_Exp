using IMS_Example.Data.Contexts;
using IMS_Example.Data.Models;
using IMS_Example.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IMS_Example.Services.TokenServices
{
    public class TokenServices : ITokenService
    {

        private readonly AppDbContext _context;
        private readonly JwtSetting _appSetting;
        private readonly IConfiguration  _iConfiguration;

        public TokenServices(AppDbContext context, IOptionsMonitor<JwtSetting> optionsMonitor, IConfiguration iConfiguration)
        {
            _context = context;
            _appSetting = optionsMonitor.CurrentValue;
            _iConfiguration = iConfiguration;
        }

        public ClaimToken Decode(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tokenS = jwtSecurityToken as JwtSecurityToken;

            var userCode = tokenS.Claims.First(claim => claim.Type == "UserCode").Value;
            var id = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var group = tokenS.Claims.First(claim => claim.Type == "IdGroup").Value;
            var key = tokenS.Claims.First(claim => claim.Type == "Key").Value;

            var response = new ClaimToken
            {
                userCode = userCode,
                id = Convert.ToInt32(id),
                group = Convert.ToInt32(group),
            };
            return response;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string GenerateToken(Users users)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.Secret);

            Claim[] getClaims ()
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, users.email));
                claims.Add(new Claim("UserCode", users.userCode));
                claims.Add(new Claim("Id", users.id.ToString()));
                claims.Add(new Claim("IdGroup", users.IdGroup.ToString()));
                claims.Add(new Claim("TokenId", Guid.NewGuid().ToString()));
                claims.Add(new Claim(ClaimTypes.Role, users.IdGroup.ToString()));

                if(getPermission_Use_Menu(users.id) != null)
                {
                    foreach(var item in getPermission_Use_Menu(users.id))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                }
                if (getPermission_Group(users.id) != null)
                {
                    foreach (var item in getPermission_Group(users.id))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                }
                return claims.ToArray();
            }

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(getClaims()),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256),
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromExpriredToken(string token)
        {
            var jwtSetting = Encoding.UTF8.GetBytes(_iConfiguration.GetSection("JwtSetting:Secret").Value);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(jwtSetting),
                ClockSkew = TimeSpan.Zero,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.Secret);

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }

        private List<string> getPermission_Use_Menu(int idUser)
        {
            var query = from a in _context.Permission_Use_Menus
                        join b in _context.Modules
                        on a.idModule equals b.id
                        where a.IdUser == idUser
                        select new { a, b };

            if(query.Count() != 0)
            {
                List<string> data = new List<string>();
                foreach(var permission in query)
                {
                    data.Add("module:" + permission.b.nameModule + " add" + permission.a.Add);
                    data.Add("module:" + permission.b.nameModule + " update" + permission.a.Update);
                    data.Add("module:" + permission.b.nameModule + " delete" + permission.a.Delete);
                    data.Add("module:" + permission.b.nameModule + " export" + permission.a.Export);
                }
                return data;
            }
            return null!;
        }

        private List<string> getPermission_Group(int idUser)
        {
            var query = from a in _context.Permission_Groups
                        join b in _context.Modules
                        on a.IdModule equals b.id
                        join c in _context.Users
                        on a.IdGroup equals c.IdGroup
                        where c.id == idUser
                        select new { a, b };

            if(query.Count() !=0)
            {
                List<string> data = new List<string>();
                foreach (var permission_group in query)
                {
                    data.Add("permission_group:" + permission_group.a.Access + " module" + permission_group.b.nameModule);
                }
                return data;
            }
            return null!;
        }


    }
}
