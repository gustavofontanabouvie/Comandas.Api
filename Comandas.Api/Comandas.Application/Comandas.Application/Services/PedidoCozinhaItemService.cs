
using Comandas.Application.Interfaces;
using Comandas.Data.Repositories.Interface;

namespace Comandas.Application.Services;

public class PedidoCozinhaItemService : IPedidoCozinhaItemService
{
    private readonly IPedidoCozinhaItemRepository _pedidoCozinhaItemRepository;

    public PedidoCozinhaItemService(IPedidoCozinhaItemRepository pedidoCozinhaItemRepository)
    {
        _pedidoCozinhaItemRepository = pedidoCozinhaItemRepository;
    }
}
