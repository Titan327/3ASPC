using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Text;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : Controller
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;
    public AuthController(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    
    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(UsersCreate userCreated)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(userCreated.Password);

        var newUser = new Users
        {
            Email = userCreated.Email,
            Pseudo = userCreated.Pseudo,
            Password = passwordHash,
            Role = '1'
        };
        
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(new { Response = "User created"});
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UsersLogin userLogin)
    {

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Pseudo == userLogin.Pseudo);
        
        if (existingUser == null)
        {
            return NotFound(new { Response = "User does not exist" });
        }

        if (!BCrypt.Net.BCrypt.Verify(userLogin.Password,existingUser.Password))
        {
            return NotFound(new { Response = "Wrong password" });
        }

        string token = CreateToken(existingUser);

        return Ok(new { Token = token });

    }



    private string CreateToken(Users user)
    {

        List<Claim> claims = new List<Claim>
        {
            new Claim("Pseudo", user.Pseudo)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Token:Key").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(int.Parse(_configuration.GetSection("Token:DayBeforeExpires").Value!)),
                signingCredentials: creds
            );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
    
    
    
}
