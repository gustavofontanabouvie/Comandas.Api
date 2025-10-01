
using Comandas.Api.DTOs.Comanda;
using Comandas.Domain;
using Comandas.Shared.DTOs;

namespace Comandas.Application.Interfaces;

public interface IComandaService
{
    public Task<ComandaByIdDto> GetComandaById(int id);
    public Task<IEnumerable<ComandasResponseDto>> GetComandas();

    public Task<ComandaCreateResponseDto> CreateComanda(ComandaCreateDto comandaCreateDto, CancellationToken cancellationToken);

}
