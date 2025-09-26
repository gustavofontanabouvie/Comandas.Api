using Comandas.Api.DTOs.Mesa;
using Comandas.Domain;
using Comandas.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repositories.Interface
{
    public interface IMesaRepository
    {
        public Task<Mesa> CreateMesa(Mesa mesa, CancellationToken cancellationToken);
        public Task<MesaByIdDto?> GetMesaById(int id);
        public Task<bool> VerificaMesa(int numero);
    }
}
