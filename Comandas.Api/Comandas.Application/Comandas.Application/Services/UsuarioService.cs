using Comandas.Api.DTOs.Usuario;
using Comandas.Application.Interfaces;
using Comandas.Data.Repositories.Interface;

namespace Comandas.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> GetUsers()
    {
        var usuarios = await _usuarioRepository.GetUsers();

        return usuarios;
    }
}
