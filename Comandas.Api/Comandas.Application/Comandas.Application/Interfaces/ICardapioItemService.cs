

using Comandas.Api.DTOs.CardapioItem;
using Comandas.Domain;
using Comandas.Shared.DTOs;

namespace Comandas.Application.Interfaces;

public interface ICardapioItemService
{
    public Task<CardapioItem> DeleteCardapioItem(int id, CancellationToken cancellationToken);
    public Task<CardapioItemByIdDto?> GetCardapioItem(int id, CancellationToken cancellationToken);
    public Task<CardapioItemCreateResponseDto> PostCardapioItem(CardapioItemCreateDto cardapioItemCreateDto, CancellationToken cancellationToken);
    public Task<CardapioItem> UpdateCardapioItem(int id, CardapioItemUpdateDto updateDto, CancellationToken cancellationToken);
    public Task<IEnumerable<CardapioItem>> GetCardapioItens();
}
