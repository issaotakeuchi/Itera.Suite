using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Itera.Suite.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<UsuarioIdentity, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets principais
        public DbSet<ProjetoDeViagem> Projetos { get; set; }
        public DbSet<ItemDeCusto> ItensDeCusto { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        // NOVOS DbSets financeiros
        public DbSet<OrdemDePagamento> OrdensDePagamento { get; set; }
        public DbSet<PagamentoDaOrdemDePagamento> PagamentosDaOrdemDePagamento { get; set; }
        public DbSet<QuitacaoDaOrdemDePagamento> QuitacoesDaOrdemDePagamento { get; set; }
        public DbSet<ComprovanteDaOrdemDePagamento> ComprovantesDaOrdemDePagamento { get; set; }

        // Observações
        public DbSet<ObservacaoProjeto> ObservacoesProjeto { get; set; }
        public DbSet<ObservacaoItem> ObservacoesItem { get; set; }
        public DbSet<ObservacaoDaOrdemDePagamento> ObservacoesOrdemDePagamento { get; set; }
        public DbSet<ObservacaoDaQuitacaoDaOrdemDePagamento> ObservacoesQuitacaoDaOrdemDePagamento { get; set; }

        // Histórico de status
        public DbSet<RegistroStatusItemDeCusto> RegistrosStatusItemDeCusto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Conversões de Enum como string
            modelBuilder.Entity<ProjetoDeViagem>()
                .Property(p => p.Status)
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

            modelBuilder.Entity<PagamentoDaOrdemDePagamento>()
                .Property(p => p.FormaPagamento)
                .HasConversion<string>();

            // ✅ Relacionamentos principais
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
                .HasMany(o => o.Observacoes)
                .WithOne(o => o.OrdemDePagamento)
                .HasForeignKey(o => o.OrdemDeProgramadoId);

            modelBuilder.Entity<OrdemDePagamento>()
                .HasMany(o => o.Quitacoes)
                .WithOne(q => q.OrdemDePagamento)
                .HasForeignKey(q => q.OrdemDePagamentoId);

            modelBuilder.Entity<PagamentoDaOrdemDePagamento>()
                .HasMany(p => p.Quitacoes)
                .WithOne(q => q.Pagamento)
                .HasForeignKey(q => q.PagamentoDaOrdemDePagamentoId);

            modelBuilder.Entity<PagamentoDaOrdemDePagamento>()
                .HasOne(p => p.ComprovanteDaOrdemDePagamento)
                .WithOne(c => c.Pagamento)
                .HasForeignKey<ComprovanteDaOrdemDePagamento>(c => c.PagamentoDaOrdemDePagamentoId);

            modelBuilder.Entity<QuitacaoDaOrdemDePagamento>()
                .HasMany(q => q.Observacoes)
                .WithOne(o => o.QuitacaoDaOrdemDePagamento)
                .HasForeignKey(o => o.QuitacaoDaOrdemDePagamentoId);

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
}
