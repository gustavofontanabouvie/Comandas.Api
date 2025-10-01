using Comandas.Api.DTOs.ComandaItem;
using Comandas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repositories.Interface
{
    public interface IComandaItemRepository
    {
        public Task CreateComandaItem(ComandaItem comandaItem, CancellationToken cancellationToken);
        public Task<ComandaItemResponseDto?> GetComandaItemById(int id);
    }
}
