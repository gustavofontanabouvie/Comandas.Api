using Comandas.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Comandas.Application.Services;

public class MesaService : IMesaService
{
    private readonly ILogger<MesaService> _logger;
    public MesaService(ILogger<MesaService> logger)
    {
        _logger = logger;
    }


}
