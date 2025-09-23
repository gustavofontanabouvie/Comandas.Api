

using Comandas.Api.DTOs.CardapioItem;

namespace Comandas.Application.Interfaces;

public interface ICardapioItemService
{
    public Task<CardapioItemByIdDto?> GetCardapioItem(int id, CancellationToken cancellationToken);
    public Task<CardapioItemCreateResponseDto> PostCardapioItem(CardapioItemCreateDto cardapioItemCreateDto, CancellationToken cancellationToken);

}
