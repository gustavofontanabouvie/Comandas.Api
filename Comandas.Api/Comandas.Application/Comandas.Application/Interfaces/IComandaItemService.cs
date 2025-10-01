

using Comandas.Api.DTOs.ComandaItem;

namespace Comandas.Application.Interfaces;

public interface IComandaItemService
{
    public Task<ComandaItemResponseDto> GetComandaItemById(int id);
}
