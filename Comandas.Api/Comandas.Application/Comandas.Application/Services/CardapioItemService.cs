using Comandas.Api.DTOs.CardapioItem;
using Comandas.Application.Interfaces;
using Comandas.Data.Repositories.Interface;
using Comandas.Domain;
using Comandas.Shared.DTOs;
using Microsoft.Extensions.Logging;

namespace Comandas.Application.Services;

public class CardapioItemService : ICardapioItemService
{
    private readonly ICardapioItemRepository _cardapioItemRepository;
    private readonly ILogger<CardapioItemService> _logger;
    public CardapioItemService(ICardapioItemRepository cardapioItemRepository, ILogger<CardapioItemService> logger)
    {
        _cardapioItemRepository = cardapioItemRepository;
        _logger = logger;
    }

    public async Task<CardapioItem> DeleteCardapioItem(int id, CancellationToken cancellationToken)
    {
        var cardapioItem = await _cardapioItemRepository.DeleteCardapioItem(id, cancellationToken);

        return cardapioItem;
    }

    public async Task<CardapioItemByIdDto?> GetCardapioItem(int id, CancellationToken cancellationToken)
    {
        var cardapioItem = await _cardapioItemRepository.GetCardapioItemById(id, cancellationToken);

        if (cardapioItem is null)
        {
            _logger.LogWarning($"Cardápio {id} não encontrado");
        }
        return cardapioItem;
    }

    public async Task<IEnumerable<CardapioItem>> GetCardapioItens()
    {
        var cardapioItens = await _cardapioItemRepository.GetCardapioItens();

        return cardapioItens;
    }

    public async Task<CardapioItemCreateResponseDto> PostCardapioItem(CardapioItemCreateDto cardapioItemCreateDto, CancellationToken cancellationToken)
    {
        var cardapioItem = new CardapioItem
        {
            Descricao = cardapioItemCreateDto.descricao,
            Titulo = cardapioItemCreateDto.titulo,
            Preco = cardapioItemCreateDto.preco,
            PossuiPreparo = cardapioItemCreateDto.possuiPreparo
        };

        await _cardapioItemRepository.CreateCardapioItem(cardapioItem, cancellationToken);

        var responseDto = new CardapioItemCreateResponseDto(cardapioItem.Id, cardapioItem.Titulo, cardapioItem.Descricao, cardapioItem.PossuiPreparo);

        return responseDto;
    }

    public async Task<CardapioItem> UpdateCardapioItem(int id, CardapioItemUpdateDto updateDto, CancellationToken cancellationToken)
    {
        var cardapioItem = await _cardapioItemRepository.UpdateCardapioItem(id, updateDto);


        return cardapioItem;
    }
}
