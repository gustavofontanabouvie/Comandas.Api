
using Comandas.Api.DTOs.ComandaItem;
using Comandas.Application.Interfaces;
using Comandas.Data.Repositories.Interface;

namespace Comandas.Application.Services;

public class ComandaItemService : IComandaItemService
{
    private readonly IComandaItemRepository _comandaItemRepository;

    public ComandaItemService(IComandaItemRepository comandaItemRepository)
    {
        _comandaItemRepository = comandaItemRepository;
    }

    public async Task<ComandaItemResponseDto> GetComandaItemById(int id)
    {
        var comandaItem = await _comandaItemRepository.GetComandaItemById(id);


        return comandaItem;
    }
}
