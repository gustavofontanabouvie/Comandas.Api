

using Comandas.Api.DTOs.Mesa;
using Comandas.Shared.DTOs;

namespace Comandas.Application.Interfaces;

public interface IMesaService
{
    public Task<MesaByIdDto?> GetMesaById(int id);
    public Task<MesaResponseDto> PostMesa(MesaCreateDto mesaDto, CancellationToken cancellationToken);
}
