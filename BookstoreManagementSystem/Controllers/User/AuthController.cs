using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities.Users;


namespace BookstoreManagementSystem.Controllers.User;
[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<ApplicationUser> users,
    SignInManager<ApplicationUser> signIn,
    IConfiguration cfg)
    : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signIn = signIn;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
        var res  = await users.CreateAsync(user, dto.Password);
        if (!res.Succeeded) 
            return BadRequest(res.Errors);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await users.FindByEmailAsync(dto.Email);
        if (user is null || !(await users.CheckPasswordAsync(user, dto.Password)))
            return Unauthorized();

        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        var key    = Encoding.UTF8.GetBytes(cfg["Jwt:Key"]!);
        var token  = new JwtSecurityToken(
            issuer: cfg["Jwt:Issuer"],
            audience: cfg["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            )
        );

        return Ok(new JwtDto(new JwtSecurityTokenHandler().WriteToken(token)));
    }
   
    // DTOs using C# 11 record types
    public record RegisterDto(string Email, string Password);
    public record LoginDto(string Email, string Password);
    public record JwtDto(string Token);
}