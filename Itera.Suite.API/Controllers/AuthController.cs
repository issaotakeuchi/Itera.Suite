using Itera.Suite.Domain.Identity;
using Itera.Suite.Infrastructure.Services;
using Itera.Suite.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Itera.Suite.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<UsuarioIdentity> _userManager;
    private readonly SignInManager<UsuarioIdentity> _signInManager;
    private readonly TokenService _tokenService;

    public AuthController(
        UserManager<UsuarioIdentity> userManager,
        SignInManager<UsuarioIdentity> signInManager,
        TokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(string email, string password, string nomeCompleto)
    {
        var user = new UsuarioIdentity
        {
            UserName = email,
            Email = email,
            NomeCompleto = nomeCompleto
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
            return Ok("Usuário registrado!");

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Unauthorized("Usuário não encontrado.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Senha, false);
        if (!result.Succeeded)
            return Unauthorized("Senha inválida.");

        var token = _tokenService.GenerateToken(user);
        return Ok(new LoginResponse
        {
            Sucesso = true,
            Token = token,
            Expiration = DateTime.UtcNow.AddHours(3),
            UsuarioId = user.Id.ToString(),
            Email = user.Email,
            Roles = new List<string>() // preenche se tiver roles
        });

    }
}
