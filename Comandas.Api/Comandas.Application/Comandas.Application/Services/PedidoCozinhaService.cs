
using Comandas.Application.Interfaces;
using Comandas.Data.Repositories.Interface;

namespace Comandas.Application.Services;

public class PedidoCozinhaService : IPedidoCozinhaService
{
    private readonly IPedidoCozinhaRepository _pedidoCozinhaRepository;

    public PedidoCozinhaService(IPedidoCozinhaRepository pedidoCozinhaRepository)
    {
        _pedidoCozinhaRepository = pedidoCozinhaRepository;
    }
}
