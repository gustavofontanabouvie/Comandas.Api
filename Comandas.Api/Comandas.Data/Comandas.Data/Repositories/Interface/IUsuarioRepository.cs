using Comandas.Api.DTOs.Usuario;
using Comandas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Data.Repositories.Interface
{
    public interface IUsuarioRepository
    {
        public Task<IEnumerable<UsuarioResponseDto>> GetUsers();
    }
}
