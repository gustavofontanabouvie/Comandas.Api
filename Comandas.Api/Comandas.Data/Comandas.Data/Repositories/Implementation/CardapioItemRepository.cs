using Comandas.Api.Database;
using Comandas.Api.DTOs.CardapioItem;
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
    public class CardapioItemRepository : ICardapioItemRepository
    {
        private readonly ComandasDbContext _dbContext;

        public CardapioItemRepository(ComandasDbContext comandasDbContext)
        {
            _dbContext = comandasDbContext;
        }

        public async Task<CardapioItem> CreateCardapioItem(CardapioItem cardapioItem, CancellationToken cancellationToken)
        {
            _dbContext.CardapioItens.Add(cardapioItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return cardapioItem;
        }

        public async Task<CardapioItemByIdDto?> GetCardapioItemById(int id, CancellationToken cancellationToken)
        {

            var cardapioItem = await _dbContext.CardapioItens.AsNoTracking()
                .Where(ci => ci.Id == id)
                .Select(ci => new CardapioItemByIdDto(ci.Titulo, ci.Descricao, ci.Preco))
                .TagWith(nameof(GetCardapioItemById))
                .FirstOrDefaultAsync();

            return cardapioItem;
        }
    }
}
