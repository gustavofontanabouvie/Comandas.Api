using System.Drawing;

namespace Comandas.Api.DTOs.CardapioItem;

public record CardapioItemCreateResponseDto(int id, string titulo, string descricao, bool possuiPreparo);

