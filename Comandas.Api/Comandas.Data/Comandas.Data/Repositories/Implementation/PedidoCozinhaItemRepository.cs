using Comandas.Api.Database;
using Comandas.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repositories.Implementation
{
    public class PedidoCozinhaItemRepository : IPedidoCozinhaItemRepository
    {
        private readonly ComandasDbContext _dbContext;

        public PedidoCozinhaItemRepository(ComandasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
