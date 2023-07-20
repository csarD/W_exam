
using CustomersApi.Dtos;
using CustomersApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CustomersApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly CustomerDatabaseContext _customerDatabaseContext;
        private IConfiguration config;
        public LoginController(CustomerDatabaseContext customerDatabaseContext, IConfiguration config)
        {
            _customerDatabaseContext = customerDatabaseContext;
            this.config = config;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(AdminDto adminDto)
        {
            var admin= await _customerDatabaseContext.GetAdmin(adminDto);
            if(admin == null)
            {
                return BadRequest(new { message = "Credenciales Invalidas" });
            }
            return new OkObjectResult(new { token = GenerateToken(admin) });
        }
        private string GenerateToken(AdminEntity admin)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,admin.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
            var creds= new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: creds);
            string token= new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
