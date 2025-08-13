using System.Reflection.Emit;
using Comandas.Api.Database.Configurations;
using Comandas.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace Comandas.Api.Database;

public class ComandasDbContext : DbContext
{

    public ComandasDbContext(DbContextOptions<ComandasDbContext> options)
        : base(options)
    {
    }


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("workstation id=comandasapibouviedb.mssql.somee.com;packet size=4096;user id=gustavofb01_SQLLogin_1;pwd=bp448hewag;data source=comandasapibouviedb.mssql.somee.com;persist security info=False;initial catalog=comandasapibouviedb;TrustServerCertificate=True");
    //    base.OnConfiguring(optionsBuilder);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CardapioItemConfiguration).Assembly);

        modelBuilder.Entity<Comanda>()
            .HasMany<ComandaItem>()
            .WithOne(ci => ci.Comanda)
            .HasForeignKey(ci => ci.ComandaId);

        modelBuilder.Entity<ComandaItem>()
            .HasOne(ci => ci.Comanda)
            .WithMany(c => c.ComandaItens)
            .HasForeignKey(ci => ci.ComandaId);

        modelBuilder.Entity<ComandaItem>()
            .HasOne(ci => ci.CardapioItem)
            .WithMany()
            .HasForeignKey(ci => ci.CardapioItemId);

        modelBuilder.Entity<PedidoCozinha>()
            .HasMany<PedidoCozinhaItem>()
            .WithOne(pci => pci.PedidoCozinha)
            .HasForeignKey(pci => pci.PedidoCozinhaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PedidoCozinhaItem>()
            .HasOne(pci => pci.PedidoCozinha)
            .WithMany(pc => pc.PedidoCozinhaItens)
            .HasForeignKey(pci => pci.PedidoCozinhaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PedidoCozinhaItem>()
            .HasOne(pci => pci.ComandaItem)
            .WithMany()
            .HasForeignKey(ci => ci.ComandaItemId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<CardapioItem> CardapioItens { get; set; }

    public DbSet<Comanda> Comandas { get; set; }

    public DbSet<ComandaItem> ComandaItens { get; set; }

    public DbSet<Mesa> Mesas { get; set; }

    public DbSet<PedidoCozinha> PedidosCozinha { get; set; }

    public DbSet<PedidoCozinhaItem> PedidoCozinhaItens { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

}
