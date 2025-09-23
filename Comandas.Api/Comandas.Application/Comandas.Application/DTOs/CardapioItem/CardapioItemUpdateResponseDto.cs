namespace Comandas.Api.DTOs.CardapioItem;

public record CardapioItemUpdateDto(string titulo, string descricao, double preco, bool possuiPreparo);

public record CardapioItemUpdateResponseDto(string titulo, string descricao, double preco, bool possuiPreparo);
