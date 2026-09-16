using API.Extensions;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepo;
    private readonly IPortfolioRepository _portfolioRepo;

    public PortfolioController(
        UserManager<AppUser> userManager,
        IStockRepository stockRepo,
        IPortfolioRepository portfolioRepo)
    {
        _userManager = userManager;
        _stockRepo = stockRepo;
        _portfolioRepo = portfolioRepo;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var username = User.GetUsername();
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized("user not found");
        }
        var appUser = await _userManager.FindByNameAsync(username);
        if (appUser == null)
        {
            return Unauthorized("user not found");
        }
        var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

        return Ok(userPortfolio);
    }
}