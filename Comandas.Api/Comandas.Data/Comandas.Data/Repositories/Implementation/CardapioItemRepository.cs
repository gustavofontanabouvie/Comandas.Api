using Comandas.Api.Database;
using Comandas.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repositories.Implementation
{
    public class CardapioItemRepository : ICardapioItemRepository
    {
        private readonly ComandasDbContext _dbContext;

        public CardapioItemRepository(ComandasDbContext comandasDbContext)
        {
            _dbContext = comandasDbContext;
        }

    }
}
