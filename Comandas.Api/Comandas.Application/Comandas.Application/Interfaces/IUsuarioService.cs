
using Comandas.Api.DTOs.Usuario;

namespace Comandas.Application.Interfaces;

public interface IUsuarioService
{
    public Task<IEnumerable<UsuarioResponseDto>> GetUsers();
}
