using Comandas.Api.DTOs.CardapioItem;
using Comandas.Domain;
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
        public Task<CardapioItemByIdDto?> GetCardapioItemById(int id, CancellationToken cancellationToken);
    }
}
