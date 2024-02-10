using System.Security.Claims;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : Controller
{

    private readonly DataContext _context;
    public UserController(DataContext context)
    {
        _context = context;
    }

    
    [HttpGet("all") , Authorize(Roles = "1")]
    public async Task<ActionResult<List<Users>>> GetALlUsers()
    {
        var users = await _context.Users.ToListAsync();
        
        return Ok(users);
    }
    
    
    [HttpGet("{id}"), Authorize]
    public async Task<ActionResult<Users>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return NotFound("User not found.");
        }
        
        return Ok(user);
    }
    
    [HttpPut("{id}"), Authorize(Roles = "1")]
    public async Task<ActionResult<Users>> UpdateUser(UsersModify updateUser,int id)
    {
        var dbUser = await _context.Users.FindAsync(id);
        if (dbUser is null)
        {
            return NotFound("User not found.");
        }
        
        dbUser.Email = updateUser.Email;
        dbUser.Pseudo = updateUser.Pseudo;
        dbUser.Password = updateUser.Password;
        dbUser.Role = updateUser.Role;
        
        await _context.SaveChangesAsync();

        return Ok(new { Response = "User updated"});
    }
    
    [HttpPut("me"), Authorize]
    public async Task<ActionResult<string>> UpdateMe(UsersCreate updateUser)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var dbUser = await _context.Users.FindAsync(int.Parse(userId));
        if (dbUser is null)
        {
            return NotFound("User not found.");
        }
        
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dbUser.Password);
        
        dbUser.Email = updateUser.Email;
        dbUser.Pseudo = updateUser.Pseudo;
        dbUser.Password = passwordHash;
        
        await _context.SaveChangesAsync();

        return Ok(new { Response = "User updated"});
        
    }
    
    [HttpDelete("{id}"), Authorize(Roles = "1")]
    public async Task<ActionResult<Users>> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return NotFound("User not found.");
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return Ok(new { Response = "User deleted"});
    }
    
    [HttpDelete("me"), Authorize]
    public async Task<ActionResult<string>> DeleteMe(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(int.Parse(userId));
        if (user is null)
        {
            return NotFound("User not found.");
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return Ok(new { Response = "User deleted"});
    }
    
    
    
}


