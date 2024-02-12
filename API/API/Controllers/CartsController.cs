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

public class CartsController : Controller
{

    private readonly DataContext _context;
    public CartsController(DataContext context)
    {
        _context = context;
    }

    
    [HttpGet("all"), Authorize(Roles = "1,2,3")]
    public async Task<ActionResult<List<CartsProductsName>>> GetUserCarts()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(int.Parse(userId));
        
        var userCart = await _context.Carts
            .Include(cart => cart.Product)
            .Where(cart => cart.UserId == user.Id)
            .Select(cart => new CartsProductsName
            {
                Id = cart.Id,
                ProductName = cart.Product.Name
            })
            .ToListAsync();
        
        return Ok(userCart);
    }
    
    [HttpPost, Authorize(Roles = "1,2,3")]
    public async Task<ActionResult<string>> AddProductInCart([FromQuery] int productId)
    {
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(int.Parse(userId));
        
        var newCartProduct = new Carts
        {
            UserId = user.Id,
            ProductId = productId
        };
        
        _context.Carts.Add(newCartProduct);
        await _context.SaveChangesAsync();
        
        return Ok(new { Response = "Product added to cart"});
    }
    
    [HttpDelete, Authorize(Roles = "1,2,3")]
    public async Task<ActionResult<string>> DeleteProductInCart([FromQuery] int cartItemId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var dbUser = await _context.Users.FindAsync(int.Parse(userId));
        
        if (dbUser is null)
        {
            return NotFound(new { Response = "User not found."});
        }
        
        var cartItem = await _context.Carts.FindAsync(cartItemId);

        if (cartItem is null)
        {
            return NotFound(new { Response = "Cart item not found."});
        }

        if (dbUser.Id != cartItem.UserId)
        {
            return Unauthorized();
        }
        
        _context.Carts.Remove(cartItem);
        await _context.SaveChangesAsync();
        
        return Ok(new { Response = "Cart item deleted"});
    }

    
}


