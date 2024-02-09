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

public class ProductsController : Controller
{

    private readonly DataContext _context;
    public ProductsController(DataContext context)
    {
        _context = context;
    }

    
    [HttpGet("all"), Authorize(Roles = "1")]
    public async Task<ActionResult<List<Products>>> GetALlProducts()
    {
        var Products = await _context.Products.ToListAsync();
        
        return Ok(Products);
    }
    
    [HttpGet("{id}"), Authorize]
    public async Task<ActionResult<Products>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound("Product not found.");
        }
        return Ok(product);
    }
    
    [HttpPost, Authorize(Roles = "1,2")]
    public async Task<ActionResult<string>> CreateProduct(ProductsCreate productCreated)
    {
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FindAsync(int.Parse(userId));
        
        var newProduct = new Products
        {
            Name = productCreated.Name,
            Image = productCreated.Image,
            Price = productCreated.Price,
            UserId = user.Id
        };
        
        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();
        
        return Ok(new { Response = "Product created"});
    }
    
    [HttpPut, Authorize(Roles = "1,2")]
    public async Task<ActionResult<string>> UpdateProduct(ProductsCreate updateProduct,int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var dbUser = await _context.Users.FindAsync(int.Parse(userId));
        
        if (dbUser is null)
        {
            return NotFound(new { Response = "User not found."});
        }

        var product = await _context.Products.FindAsync(productId);

        if (product is null)
        {
            return NotFound(new { Response = "Product not found."});
        }
        
        product.Name = updateProduct.Name;
        product.Image = updateProduct.Image;
        product.Price = updateProduct.Price;
        
        await _context.SaveChangesAsync();

        return Ok(new { Response = "Product updated"});
        
    }
    
}


