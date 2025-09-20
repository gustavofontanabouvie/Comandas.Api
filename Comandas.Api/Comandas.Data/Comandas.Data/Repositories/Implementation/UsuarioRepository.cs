using Comandas.Api.Database;
using Comandas.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repositories.Implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ComandasDbContext _dbContext;

        public UsuarioRepository(ComandasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
