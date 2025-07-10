using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<UsuarioIdentity, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSets principais
    public DbSet<ProjetoDeViagem> Projetos { get; set; }
    public DbSet<ItemDeCusto> ItensDeCusto { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<OrdemDePagamento> PagamentosProgramados { get; set; }

    // Observações (ValueObjects persistidos)
    public DbSet<ObservacaoProjeto> ObservacoesProjeto { get; set; }
    public DbSet<ObservacaoItem> ObservacoesItem { get; set; }
    public DbSet<ObservacaoDaOrdemDePagamento> ObservacoesPagamento { get; set; }

    // Histórico de status
    public DbSet<RegistroStatusItemDeCusto> RegistrosStatusItemDeCusto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Enum como string (melhor para leitura e manutenção)
        modelBuilder.Entity<ProjetoDeViagem>()
            .Property(p => p.Status)
            .HasConversion<string>();

        modelBuilder.Entity<ProjetoDeViagem>()
            .Property(p => p.Tipo)
            .HasConversion<string>();

        modelBuilder.Entity<ItemDeCusto>()
            .Property(i => i.StatusAtual)
            .HasConversion<string>();

        modelBuilder.Entity<Fornecedor>()
            .Property(f => f.TipoDeServico)
            .HasConversion<string>();

        modelBuilder.Entity<OrdemDePagamento>()
            .Property(p => p.Forma)
            .HasConversion<string>();

        modelBuilder.Entity<OrdemDePagamento>()
            .Property(p => p.Status)
            .HasConversion<string>();

        // Relacionamentos principais
        modelBuilder.Entity<ProjetoDeViagem>()
            .HasMany(p => p.Observacoes)
            .WithOne(o => o.ProjetoDeViagem)
            .HasForeignKey(o => o.ProjetoDeViagemId);

        modelBuilder.Entity<ItemDeCusto>()
            .HasMany(i => i.Observacoes)
            .WithOne(o => o.ItemDeCusto)
            .HasForeignKey(o => o.ItemDeCustoId);

        modelBuilder.Entity<ItemDeCusto>()
            .HasMany(i => i.Pagamentos)
            .WithOne(p => p.ItemDeCusto)
            .HasForeignKey(p => p.ItemDeCustoId);

        modelBuilder.Entity<ItemDeCusto>()
            .HasMany(i => i.HistoricoStatus)
            .WithOne()
            .HasForeignKey("ItemDeCustoId");

        modelBuilder.Entity<OrdemDePagamento>()
            .HasMany(p => p.Observacoes)
            .WithOne(o => o.PagamentoProgramado)
            .HasForeignKey(o => o.PagamentoProgramadoId);

        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Projetos)
            .WithOne(p => p.Cliente)
            .HasForeignKey(p => p.ClienteId);

        modelBuilder.Entity<Fornecedor>()
            .HasMany(f => f.ItensDeCusto)
            .WithOne(i => i.Fornecedor)
            .HasForeignKey(i => i.FornecedorId);

        base.OnModelCreating(modelBuilder);
    }
}
