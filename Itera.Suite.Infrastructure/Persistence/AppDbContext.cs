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

        // ✅ DbSets principais
        public DbSet<ProjetoDeViagem> ProjetosDeViagem { get; set; }
        public DbSet<ItemDeCusto> ItensDeCusto { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        // ✅ DbSets financeiros
        public DbSet<OrdemDePagamento> OrdensDePagamento { get; set; }
        public DbSet<PagamentoDaOrdemDePagamento> PagamentosDaOrdemDePagamento { get; set; }
        public DbSet<QuitacaoDaOrdemDePagamento> QuitacoesDaOrdemDePagamento { get; set; }
        public DbSet<ComprovanteDaOrdemDePagamento> ComprovantesDaOrdemDePagamento { get; set; }

        // ✅ Observações
        public DbSet<ObservacaoProjeto> ObservacoesProjeto { get; set; }
        public DbSet<ObservacaoItem> ObservacoesItem { get; set; }
        public DbSet<ObservacaoDaOrdemDePagamento> ObservacoesOrdemDePagamento { get; set; }
        public DbSet<ObservacaoDaQuitacaoDaOrdemDePagamento> ObservacoesQuitacaoDaOrdemDePagamento { get; set; }

        // ✅ Histórico de status
        public DbSet<RegistroStatusItemDeCusto> RegistrosStatusItemDeCusto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Permite usar IEntityTypeConfiguration no futuro
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // ✅ --- PROJETO DE VIAGEM ---
            modelBuilder.Entity<ProjetoDeViagem>(builder =>
            {
                builder.ToTable("ProjetosDeViagem"); // Força plural no schema

                builder.Property(p => p.Status).HasConversion<string>();
                builder.Property(p => p.Tipo).HasConversion<string>();

                builder.HasMany(p => p.Observacoes)
                       .WithOne(o => o.ProjetoDeViagem)
                       .HasForeignKey(o => o.ProjetoDeViagemId);

                builder.Navigation(p => p.Observacoes)
                       .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            // ✅ --- ITEM DE CUSTO ---
            modelBuilder.Entity<ItemDeCusto>()
                .Property(i => i.StatusAtual)
                .HasConversion<string>();

            modelBuilder.Entity<ItemDeCusto>()
                .HasOne(i => i.Fornecedor)
                .WithMany(f => f.ItensDeCusto)
                .HasForeignKey(i => i.FornecedorId)
                .IsRequired();

            modelBuilder.Entity<ItemDeCusto>()
                .HasMany(i => i.Observacoes)
                .WithOne(o => o.ItemDeCusto)
                .HasForeignKey(o => o.ItemDeCustoId);

            modelBuilder.Entity<ItemDeCusto>()
                .Navigation(i => i.Observacoes)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<ItemDeCusto>()
                .HasMany(i => i.Pagamentos)
                .WithOne(p => p.ItemDeCusto)
                .HasForeignKey(p => p.ItemDeCustoId);

            modelBuilder.Entity<ItemDeCusto>()
                .Navigation(i => i.Pagamentos)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<ItemDeCusto>()
                .HasMany(i => i.HistoricoStatus)
                .WithOne()
                .HasForeignKey("ItemDeCustoId");

            modelBuilder.Entity<ItemDeCusto>()
                .Navigation(i => i.HistoricoStatus)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            // ✅ --- FORNECEDOR ---
            modelBuilder.Entity<Fornecedor>()
                .Property(f => f.TipoDeServico)
                .HasConversion<string>();

            modelBuilder.Entity<Fornecedor>()
                .HasMany(f => f.ItensDeCusto)
                .WithOne(i => i.Fornecedor)
                .HasForeignKey(i => i.FornecedorId);

            modelBuilder.Entity<Fornecedor>()
                .Navigation(f => f.ItensDeCusto)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            // ✅ --- CLIENTE ---
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Projetos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId);

            modelBuilder.Entity<Cliente>()
                .Navigation(c => c.Projetos)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            // ✅ --- ORDEM DE PAGAMENTO ---
            modelBuilder.Entity<OrdemDePagamento>()
                .Property(p => p.Forma).HasConversion<string>();

            modelBuilder.Entity<OrdemDePagamento>()
                .Property(p => p.Status).HasConversion<string>();

            modelBuilder.Entity<OrdemDePagamento>()
                .HasMany(o => o.Observacoes)
                .WithOne(o => o.OrdemDePagamento)
                .HasForeignKey(o => o.OrdemDePagamentoId);

            modelBuilder.Entity<OrdemDePagamento>()
                .Navigation(o => o.Observacoes)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<OrdemDePagamento>()
                .HasMany(o => o.Quitacoes)
                .WithOne(q => q.OrdemDePagamento)
                .HasForeignKey(q => q.OrdemDePagamentoId);

            // ✅ --- PAGAMENTO DA ORDEM ---
            modelBuilder.Entity<PagamentoDaOrdemDePagamento>()
                .Property(p => p.FormaPagamento).HasConversion<string>();

            modelBuilder.Entity<PagamentoDaOrdemDePagamento>()
                .HasMany(p => p.Quitacoes)
                .WithOne(q => q.Pagamento)
                .HasForeignKey(q => q.PagamentoDaOrdemDePagamentoId);

            modelBuilder.Entity<PagamentoDaOrdemDePagamento>()
                .HasOne(p => p.ComprovanteDaOrdemDePagamento)
                .WithOne(c => c.Pagamento)
                .HasForeignKey<ComprovanteDaOrdemDePagamento>(c => c.PagamentoDaOrdemDePagamentoId);

            // ✅ --- QUITAÇÃO ---
            modelBuilder.Entity<QuitacaoDaOrdemDePagamento>()
                .HasMany(q => q.Observacoes)
                .WithOne(o => o.QuitacaoDaOrdemDePagamento)
                .HasForeignKey(o => o.QuitacaoDaOrdemDePagamentoId);
        }
    }
}
