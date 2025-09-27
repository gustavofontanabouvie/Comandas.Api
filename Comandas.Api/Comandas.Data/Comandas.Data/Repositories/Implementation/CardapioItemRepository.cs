using Comandas.Api.Database;
using Comandas.Api.DTOs.CardapioItem;
using Comandas.Data.Repositories.Interface;
using Comandas.Domain;
using Comandas.Shared.DTOs;
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

        public async Task<CardapioItem> DeleteCardapioItem(int id, CancellationToken cancellationToken)
        {
            var cardapioItem = await _dbContext.CardapioItens
                .AsNoTracking()
                .FirstOrDefaultAsync(ci => ci.Id == id);

            _dbContext.CardapioItens.Remove(cardapioItem);
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

        public async Task<IEnumerable<CardapioItem>> GetCardapioItens()
        {
            return await _dbContext.CardapioItens.ToListAsync();
        }

        public async Task<CardapioItem> UpdateCardapioItem(int id, CardapioItemUpdateDto updateDto)
        {
            var cardapioItem = await _dbContext.CardapioItens.AsNoTracking()
                .Where(ci => ci.Id == id)
                .FirstOrDefaultAsync();

            if (cardapioItem != null)
            {
                if (!cardapioItem.Titulo.Equals(updateDto.titulo))
                    cardapioItem.Titulo = updateDto.titulo;

                if (!cardapioItem.Descricao.Equals(updateDto.descricao))
                    cardapioItem.Descricao = updateDto.descricao;

                if (cardapioItem.Preco != updateDto.preco)
                    cardapioItem.Preco = updateDto.preco;

                if (cardapioItem.PossuiPreparo != updateDto.possuiPreparo)
                    cardapioItem.PossuiPreparo = updateDto.possuiPreparo;

                _dbContext.CardapioItens.Update(cardapioItem);

                await _dbContext.SaveChangesAsync();
            }

            return cardapioItem;

        }
    }
}
