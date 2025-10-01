using Comandas.Api.Database;
using Comandas.Api.DTOs.Comanda;
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
    public class ComandaRepository : IComandaRepository
    {
        private readonly ComandasDbContext _dbContext;

        public ComandaRepository(ComandasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateComanda(Comanda comanda, CancellationToken cancellationToken)
        {
            await _dbContext.Comandas.AddAsync(comanda);
        }

        public async Task<ComandaByIdDto> GetComandaById(int id)
        {

            var comanda = await _dbContext.Comandas.AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new ComandaDto(c.NumeroMesa, c.NomeCliente, c.SituacaoComanda))
                .FirstOrDefaultAsync();

            var comandaitens = await _dbContext.ComandaItens.AsNoTracking()
                    .Where(ci => ci.Comanda.Id == id)
                    .Select(ci => new ComandaItemByIdDto(ci.Id, ci.CardapioItemId, ci.CardapioItem.Titulo))
                    .ToListAsync();

            var respostaDto = new ComandaByIdDto(comanda.numeroMesa, comanda.nomeCliente, comanda.situacaoComanda, comandaitens);

            return respostaDto;
        }

        public async Task<IEnumerable<ComandasResponseDto>> GetComandas()
        {
            return await _dbContext.Comandas.AsNoTracking()
                        .Include(c => c.ComandaItens)
                            .ThenInclude(ci => ci.CardapioItem)
                        .Select(c => new ComandasResponseDto(c.NomeCliente, c.NumeroMesa, c.SituacaoComanda, c.ComandaItens))
                        .ToListAsync();
        }
    }
}
