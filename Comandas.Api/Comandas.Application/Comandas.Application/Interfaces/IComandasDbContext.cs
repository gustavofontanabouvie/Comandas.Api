using Comandas.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Application.Interfaces
{
    public interface IComandasDbContext
    {
        public DbSet<CardapioItem> CardapioItens { get; set; }

        public DbSet<Comanda> Comandas { get; set; }

        public DbSet<ComandaItem> ComandaItens { get; set; }

        public DbSet<Mesa> Mesas { get; set; }

        public DbSet<PedidoCozinha> PedidosCozinha { get; set; }

        public DbSet<PedidoCozinhaItem> PedidoCozinhaItens { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
