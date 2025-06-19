using KironTestWebAPI.Components;
using KironTestWebAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;

namespace KironTestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration _configuration;

        //public NavigationController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public NavigationController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext=dbContext;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            NavigationComponent navigationRecursive = new NavigationComponent(_configuration);
            var allNavigations = navigationRecursive.GetAllNavigation();
            if (allNavigations is null)
            {
                return NotFound();
            }
            var result = navigationRecursive.NavigationRecursive(allNavigations);
            return Ok(result);
        }
    }
}
