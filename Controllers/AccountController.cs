using API.Dtos.Account;
using API.Interfaces; 
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers; 

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

   [HttpPost("register")]
public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
{
    try
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var appUser = new AppUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email
        };

        var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (createdUser.Succeeded)
        {
            // بخش نقش موقتاً حذف شد تا توکن برگردد
            return Ok(new NewUserDto
            {
                UserName = appUser.UserName,
                Email = appUser.Email,
                Token = _tokenService.CreateToken(appUser)
            });
        }
        else
        {
            return StatusCode(500, createdUser.Errors);
        }
    }
    catch (Exception e)
    {
        return StatusCode(500, e.Message);
    }
}
}