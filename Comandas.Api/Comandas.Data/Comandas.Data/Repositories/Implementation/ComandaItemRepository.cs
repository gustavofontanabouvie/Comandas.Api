using Comandas.Api.Database;
using Comandas.Api.DTOs.ComandaItem;
using Comandas.Data.Repositories.Interface;
using Comandas.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repositories.Implementation
{
    public class ComandaItemRepository : IComandaItemRepository
    {
        private readonly ComandasDbContext _dbContext;

        public ComandaItemRepository(ComandasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateComandaItem(ComandaItem comandaItem, CancellationToken cancellationToken)
        {
            await _dbContext.ComandaItens.AddAsync(comandaItem);
        }

        public async Task<ComandaItemResponseDto> GetComandaItemById(int id)
        {
            var comanItem = await _dbContext.ComandaItens
                                    .Where(ci => ci.Id == id)
                                    .Include(ci => ci.Comanda)
                                    .Include(ci => ci.CardapioItem)
                                    .Select(ci => new ComandaItemResponseDto(ci.ComandaId, ci.CardapioItemId))
                                    .FirstOrDefaultAsync();

            return comanItem;
        }
    }
}
