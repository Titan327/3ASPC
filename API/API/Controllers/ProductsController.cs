using System.Security.Claims;
using API.Data;
using API.Models;
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

    [HttpGet]
    public async Task<ActionResult<List<Products>>> GetProducts(string typeFilter,string sortBy, string orderBy, int limit = 10)
    {
        switch (sortBy.ToLower())
        {
            case "date":
                if (orderBy.ToLower() == "asc")
                { 
                    Ok(await _context.Products.Where(products => products.Type == typeFilter).OrderBy(product => product.AddedTime).Take(limit).ToListAsync());
                }
                Ok(await _context.Products.Where(products => products.Type == typeFilter).OrderByDescending(product => product.AddedTime).Take(limit).ToListAsync());
                break;
            case "userid":
                if (orderBy.ToLower() == "asc")
                { 
                    Ok(await _context.Products.Where(products => products.Type == typeFilter).OrderBy(product => product.UserId).Take(limit).ToListAsync());
                }
                Ok(await _context.Products.Where(products => products.Type == typeFilter).OrderByDescending(product => product.UserId).Take(limit).ToListAsync());
                break;
            case "name":
                if (orderBy.ToLower() == "asc")
                { 
                    Ok(await _context.Products.Where(products => products.Type == typeFilter).OrderBy(product => product.Name).Take(limit).ToListAsync());
                }
                Ok(await _context.Products.Where(products => products.Type == typeFilter).OrderByDescending(product => product.Name).Take(limit).ToListAsync());
                break;
            case "price":
                if (orderBy.ToLower() == "asc")
                { 
                    Ok(await _context.Products.Where(products => products.Type == typeFilter).OrderBy(product => product.Price).Take(limit).ToListAsync());
                }
                Ok(await _context.Products.Where(products => products.Type == typeFilter).OrderByDescending(product => product.Price).Take(limit).ToListAsync());
                break;
        }
        
        return Ok(await _context.Products.Take(limit).ToListAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Products>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound("Product not found.");
        }
        return Ok(product);
    }
    
    [HttpGet("{search}")]
    public async Task<ActionResult<Products>> SearchProduct(string search)
    {
        var product = await _context.Products.Where(products => products.Name.Contains(search)).ToListAsync();
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
        
        if (dbUser.Id != product.UserId)
        {
            return Unauthorized();
        }
        
        product.Name = updateProduct.Name;
        product.Image = updateProduct.Image;
        product.Price = updateProduct.Price;
        
        await _context.SaveChangesAsync();

        return Ok(new { Response = "Product updated"});
    }
    
    [HttpDelete("{id}"), Authorize(Roles = "1,2")]
    public async Task<ActionResult<string>> DeleteProduct(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var dbUser = await _context.Users.FindAsync(int.Parse(userId));
        
        if (dbUser is null)
        {
            return NotFound(new { Response = "User not found."});
        }
        
        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
            return NotFound(new { Response = "Product not found."});
        }

        if (dbUser.Id != product.UserId)
        {
            return Unauthorized();
        }
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        
        return Ok(new { Response = "Product deleted"});
    }
    
    
}


