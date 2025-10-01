using Comandas.Api.DTOs.Comanda;
using Comandas.Domain;
using Comandas.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repositories.Interface
{
    public interface IComandaRepository
    {
        public Task CreateComanda(Comanda comanda, CancellationToken cancellationToken);
        public Task<ComandaByIdDto> GetComandaById(int id);
        public Task<IEnumerable<ComandasResponseDto>> GetComandas();
    }
}
