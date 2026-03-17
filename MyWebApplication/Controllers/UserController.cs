using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace MyWebApplication.Controllers;

public record CreateUserDto
{
    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
}

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser([FromBody]CreateUserDto dto)
    {
        return Ok(dto);
    }
}