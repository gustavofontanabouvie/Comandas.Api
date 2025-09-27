using Comandas.Api.DTOs.CardapioItem;
using Comandas.Domain;
using Comandas.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repositories.Interface
{
    public interface ICardapioItemRepository
    {
        public Task<CardapioItem> CreateCardapioItem(CardapioItem cardapioItem, CancellationToken cancellationToken);
        public Task<CardapioItem> DeleteCardapioItem(int id, CancellationToken cancellationToken);
        public Task<CardapioItemByIdDto?> GetCardapioItemById(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<CardapioItem>> GetCardapioItens();
        public Task<CardapioItem> UpdateCardapioItem(int id, CardapioItemUpdateDto updateDto);
    }
}
