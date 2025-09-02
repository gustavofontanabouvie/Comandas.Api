using Comandas.Api.Database;
using Comandas.Api.DTOs.Usuario;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Comandas.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly ComandasDbContext _dbContext;

    public UsuariosController(ComandasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetUsuarios()
    {
        List<Usuario> usuarios = await _dbContext.Usuarios.ToListAsync();

        List<UsuarioResponseDto> usuarioResposta = new();

        foreach (var user in usuarios)
        {
            var novoUsuario = new UsuarioResponseDto(user.Nome, user.Email);

            usuarioResposta.Add(novoUsuario);
        }
        return usuarioResposta;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioResponseDto>> GetUsuario(int id)
    {
        var usuario = await _dbContext.Usuarios.FindAsync(id);

        if (usuario == null)
            return NotFound();

        var usuarioDto = new UsuarioResponseDto(usuario.Nome, usuario.Email);

        return usuarioDto;
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDto>> PostUsuario(UsuarioCreateDto usuarioCreateDto)
    {
        var usuario = new Usuario
        {
            Nome = usuarioCreateDto.nome,
            Email = usuarioCreateDto.email,
            Senha = usuarioCreateDto.senha
        };

        _dbContext.Usuarios.Add(usuario);

        await _dbContext.SaveChangesAsync();

        var responseDto = new UsuarioResponseDto(usuario.Nome, usuario.Email);

        return CreatedAtAction("GetUsuario", new { id = usuario.Id }, responseDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioLoginResponseDto>> LoginUser([FromBody] UsuarioLoginRequest loginRequest)
    {
        var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(us => us.Email == loginRequest.email);

        if (usuario == null)
            return NotFound("Email Inválido");

        if (!loginRequest.senha.Equals(usuario.Senha))
        {
            return NotFound("Senha Inválida");
        }

        var secret = Encoding.UTF8.GetBytes("3e8acfc238f45a314fd4b2bde272678ad30bd1774743a11dbc5c53ac71ca494b");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name,usuario.Email),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
                }
                )
        };

        var tokenGenerator = new JwtSecurityTokenHandler();
        var token = tokenGenerator.CreateToken(tokenDescriptor);

        var tokenFinal = tokenGenerator.WriteToken(token);

        return Ok(new UsuarioLoginResponseDto(usuario.Email, tokenFinal));
    }
}
