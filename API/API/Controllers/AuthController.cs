using API.Data;
using Microsoft.AspNetCore.Mvc;
using API.Models;


namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : Controller
{
    private readonly DataContext _context;
    public AuthController(DataContext context)
    {
        _context = context;
    }

    /*
    [HttpPost("register")]
    public ActionResult<Users> Register(UsersDto request)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        
    }
    */
    
    [HttpPost("register")]
    public async Task<ActionResult<Users>> Register(Users user)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = passwordHash;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User created");
    }
    
    
    
}
