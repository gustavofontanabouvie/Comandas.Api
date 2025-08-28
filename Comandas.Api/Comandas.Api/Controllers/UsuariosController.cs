using Comandas.Api.Database;
using Comandas.Api.DTOs.Usuario;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Comandas.Api.Controllers;
[Authorize]
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
}
