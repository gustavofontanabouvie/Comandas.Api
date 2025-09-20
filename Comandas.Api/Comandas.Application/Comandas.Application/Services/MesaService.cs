using Comandas.Application.Interfaces;
using Comandas.Data.Repositories.Interface;
using Microsoft.Extensions.Logging;

namespace Comandas.Application.Services;

public class MesaService : IMesaService
{
    private readonly IMesaRepository _mesaRepository;

    public MesaService(IMesaRepository mesaRepository)
    {
        _mesaRepository = mesaRepository;
    }
}
