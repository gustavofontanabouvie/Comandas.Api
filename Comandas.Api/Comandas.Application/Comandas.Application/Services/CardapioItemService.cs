using Comandas.Application.Interfaces;
using Comandas.Data.Repositories.Interface;

namespace Comandas.Application.Services;

public class CardapioItemService : ICardapioItemService
{
    private readonly ICardapioItemRepository _cardapioItemRepository;

    public CardapioItemService(ICardapioItemRepository cardapioItemRepository)
    {
        _cardapioItemRepository = cardapioItemRepository;
    }
}
