using Comandas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repositories.Interface
{
    public interface IPedidoCozinhaRepository
    {
        public Task CreatePedidoCozinha(PedidoCozinha pedidoCozinha, CancellationToken cancellationToken);
    }
}
