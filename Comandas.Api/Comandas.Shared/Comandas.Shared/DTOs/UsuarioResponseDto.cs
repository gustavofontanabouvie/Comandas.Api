namespace Comandas.Api.DTOs.Usuario;

public record UsuarioResponseDto(string nome, string email);

public record UsuarioLoginRequest(string email, string senha);

public record UsuarioLoginResponseDto(string email, string token);