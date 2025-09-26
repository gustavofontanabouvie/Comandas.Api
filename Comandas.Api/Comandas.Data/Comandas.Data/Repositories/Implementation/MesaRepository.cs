using Comandas.Api.Database;
using Comandas.Api.DTOs.Mesa;
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
    public class MesaRepository : IMesaRepository
    {
        private readonly ComandasDbContext _dbContext;

        public MesaRepository(ComandasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Mesa> CreateMesa(Mesa mesa, CancellationToken cancellationToken)
        {
            _dbContext.Mesas.Add(mesa);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return mesa;
        }

        public async Task<MesaByIdDto> GetMesaById(int id)
        {
            var mesa = await _dbContext.Mesas.AsNoTracking()
                .Where(me => me.Id == id)
                .FirstOrDefaultAsync();

            var mesaDto = new MesaByIdDto(mesa.Numero, mesa.SituacaoMesa);

            return mesaDto;
        }

        public async Task<IEnumerable<Mesa>> GetMesas()
        {
            return await _dbContext.Mesas.ToListAsync();
        }

        public async Task<bool> VerificaMesa(int numero)
        {
            var verificamesa = await _dbContext.Mesas.AnyAsync(me => me.Numero == numero);

            if (verificamesa)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
