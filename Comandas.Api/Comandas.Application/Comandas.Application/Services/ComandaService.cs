

using Comandas.Application.Interfaces;
using Comandas.Data.Repositories.Interface;

namespace Comandas.Application.Services;

public class ComandaService : IComandaService
{
    private readonly IComandaRepository _comandaRepository;

    public ComandaService(IComandaRepository comandaRepository)
    {
        _comandaRepository = comandaRepository;
    }
}
