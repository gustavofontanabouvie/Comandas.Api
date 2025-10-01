using Comandas.Api.Database;
using Comandas.Api.DTOs.Usuario;
using Comandas.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<UsuarioResponseDto>> GetUsers()
        {
            return await _dbContext.Usuarios.Select(u => new UsuarioResponseDto(u.Nome, u.Email)).ToListAsync();
        }
    }
}
