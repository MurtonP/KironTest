using KironTestWebAPI.Data;
using KironTestWebAPI.Models;
using KironTestWebAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;

namespace KironTestWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AccessController(ApplicationDbContext dbContext)
        {
            this.dbContext=dbContext;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(dbContext.Users.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetUserById(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserDTO addUserDTO)
        {
            var userEntity = new User()
            {
                UserName = addUserDTO.UserName,
                Password = addUserDTO.Password
            };

            dbContext.Users.Add(userEntity);
            dbContext.SaveChanges();

            return Ok(userEntity);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            var user = dbContext.Users.Find(id);

            if (user is null)
            {
                return NotFound();
            }

            user.UserName = updateUserDTO.UserName;
            user.Password = updateUserDTO.Password;

            dbContext.SaveChanges();

            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            var user = dbContext.Users.Find(id);

            if (user is null)
            {
                return NotFound();
            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{userName},{userPassword}")]
        public IActionResult UserLogin(string userName, string userPassword, IConfiguration configuration)
        {
            TokenProvider tokenProvider = new TokenProvider(configuration);
            User? user = dbContext.Users.FirstOrDefault(u => u.UserName == userName && u.Password == userPassword);
            IConfiguration _config = configuration;

            if (user is null)
            {
                return Unauthorized();
            }

            //string token = tokenProvider.Create(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}
