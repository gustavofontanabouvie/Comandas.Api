using Comandas.Api.DTOs.Mesa;
using Comandas.Application.Interfaces;
using Comandas.Data.Repositories.Interface;
using Comandas.Domain;
using Comandas.Shared.DTOs;
using Microsoft.Extensions.Logging;

namespace Comandas.Application.Services;

public class MesaService : IMesaService
{
    private readonly IMesaRepository _mesaRepository;

    public MesaService(IMesaRepository mesaRepository)
    {
        _mesaRepository = mesaRepository;
    }

    public async Task<MesaByIdDto?> GetMesaById(int id)
    {
        var mesa = await _mesaRepository.GetMesaById(id);

        return mesa;
    }

    public async Task<IEnumerable<Mesa>> GetMesas()
    {
        var mesas = await _mesaRepository.GetMesas();

        return mesas;
    }

    public async Task<MesaResponseDto> PostMesa(MesaCreateDto mesaDto, CancellationToken cancellationToken)
    {
        var mesa = new Mesa
        {
            Numero = mesaDto.numero
        };

        var verificaMesa = await _mesaRepository.VerificaMesa(mesaDto.numero);

        if (!verificaMesa)
        {
            await _mesaRepository.CreateMesa(mesa, cancellationToken);

            var createDto = new MesaResponseDto(mesa.Numero, mesa.Id);

            return createDto;
        }

        return null;
    }
}
