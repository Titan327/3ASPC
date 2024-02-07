using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetALlUser()
        {
            var User = new List<Users>()
            {
                new Users
                {
                    Id = 1,
                    Email = "Test@gmail.com",
                    Pseudo = "teest",
                    Password = "testtss",
                    Role = '1'
                }
            };
            return Ok(User);
        }
    }
}

