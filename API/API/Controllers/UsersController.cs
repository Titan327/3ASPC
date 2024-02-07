using API.Data;
using API.Models;
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

    /*
    [HttpGet]
    public async Task<ActionResult<List<Users>>> GetALlUsers()
    {
        var users = await _context.Users.ToListAsync();
        
        return Ok(users);
    }
    */
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Users>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return NotFound("User not found.");
        }
        
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<ActionResult<Users>> AddUser(Users user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }
    
    [HttpPut]
    public async Task<ActionResult<Users>> UpdateHero(Users updateUser)
    {
        var dbUser = await _context.Users.FindAsync(updateUser.Id);
        if (dbUser is null)
        {
            return NotFound("User not found.");
        }
        
        dbUser.Email = updateUser.Email;
        dbUser.Pseudo = updateUser.Pseudo;
        dbUser.Password = updateUser.Password;
        dbUser.Role = updateUser.Role;
        
        await _context.SaveChangesAsync();

        return Ok(updateUser);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Users>> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
        {
            return NotFound("User not found.");
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return Ok(user);
    }
    
    
    
}


