using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Itera.Suite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeCompleto = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Documento = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    ContatoPrincipal = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Contato = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Telefone = table.Column<string>(type: "text", nullable: true),
                    TipoDeServico = table.Column<string>(type: "text", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CriadoPor = table.Column<string>(type: "text", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoPor = table.Column<string>(type: "text", nullable: true),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PagamentosDaOrdemDePagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FormaPagamento = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentosDaOrdemDePagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeInterno = table.Column<string>(type: "text", nullable: false),
                    Origem = table.Column<string>(type: "text", nullable: false),
                    Destino = table.Column<string>(type: "text", nullable: false),
                    Objetivo = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    DataSaida = table.Column<DateOnly>(type: "date", nullable: false),
                    DataRetorno = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    CriadoPor = table.Column<string>(type: "text", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projetos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComprovantesDaOrdemDePagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PagamentoDaOrdemDePagamentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    NomeArquivoOriginal = table.Column<string>(type: "text", nullable: false),
                    DataUpload = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AnexadoPor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprovantesDaOrdemDePagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComprovantesDaOrdemDePagamento_PagamentosDaOrdemDePagamento~",
                        column: x => x.PagamentoDaOrdemDePagamentoId,
                        principalTable: "PagamentosDaOrdemDePagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensDeCusto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjetoDeViagemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Categoria = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uuid", nullable: true),
                    Diarias = table.Column<int>(type: "integer", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric", nullable: false),
                    StatusAtual = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensDeCusto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensDeCusto_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItensDeCusto_Projetos_ProjetoDeViagemId",
                        column: x => x.ProjetoDeViagemId,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservacoesProjeto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjetoDeViagemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Texto = table.Column<string>(type: "text", nullable: false),
                    AutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservacoesProjeto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservacoesProjeto_Projetos_ProjetoDeViagemId",
                        column: x => x.ProjetoDeViagemId,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservacoesItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemDeCustoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Texto = table.Column<string>(type: "text", nullable: false),
                    AutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservacoesItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservacoesItem_ItensDeCusto_ItemDeCustoId",
                        column: x => x.ItemDeCustoId,
                        principalTable: "ItensDeCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdensDePagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemDeCustoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValorAutorizado = table.Column<decimal>(type: "numeric", nullable: false),
                    DataPrevista = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Forma = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdensDePagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdensDePagamento_ItensDeCusto_ItemDeCustoId",
                        column: x => x.ItemDeCustoId,
                        principalTable: "ItensDeCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosStatusItemDeCusto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemDeCustoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioResponsavel = table.Column<string>(type: "text", nullable: false),
                    Justificativa = table.Column<string>(type: "text", nullable: true),
                    ArquivoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosStatusItemDeCusto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosStatusItemDeCusto_ItensDeCusto_ItemDeCustoId",
                        column: x => x.ItemDeCustoId,
                        principalTable: "ItensDeCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservacoesOrdemDePagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdemDePagamentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Texto = table.Column<string>(type: "text", nullable: false),
                    AutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservacoesOrdemDePagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservacoesOrdemDePagamento_OrdensDePagamento_OrdemDePagame~",
                        column: x => x.OrdemDePagamentoId,
                        principalTable: "OrdensDePagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuitacoesDaOrdemDePagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdemDePagamentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    PagamentoDaOrdemDePagamentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValorQuitado = table.Column<decimal>(type: "numeric", nullable: false),
                    DataQuitacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuitacoesDaOrdemDePagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuitacoesDaOrdemDePagamento_OrdensDePagamento_OrdemDePagame~",
                        column: x => x.OrdemDePagamentoId,
                        principalTable: "OrdensDePagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuitacoesDaOrdemDePagamento_PagamentosDaOrdemDePagamento_Pa~",
                        column: x => x.PagamentoDaOrdemDePagamentoId,
                        principalTable: "PagamentosDaOrdemDePagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservacoesQuitacaoDaOrdemDePagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuitacaoDaOrdemDePagamentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Texto = table.Column<string>(type: "text", nullable: false),
                    AutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservacoesQuitacaoDaOrdemDePagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservacoesQuitacaoDaOrdemDePagamento_QuitacoesDaOrdemDePag~",
                        column: x => x.QuitacaoDaOrdemDePagamentoId,
                        principalTable: "QuitacoesDaOrdemDePagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComprovantesDaOrdemDePagamento_PagamentoDaOrdemDePagamentoId",
                table: "ComprovantesDaOrdemDePagamento",
                column: "PagamentoDaOrdemDePagamentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItensDeCusto_FornecedorId",
                table: "ItensDeCusto",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensDeCusto_ProjetoDeViagemId",
                table: "ItensDeCusto",
                column: "ProjetoDeViagemId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservacoesItem_ItemDeCustoId",
                table: "ObservacoesItem",
                column: "ItemDeCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservacoesOrdemDePagamento_OrdemDePagamentoId",
                table: "ObservacoesOrdemDePagamento",
                column: "OrdemDePagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservacoesProjeto_ProjetoDeViagemId",
                table: "ObservacoesProjeto",
                column: "ProjetoDeViagemId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservacoesQuitacaoDaOrdemDePagamento_QuitacaoDaOrdemDePaga~",
                table: "ObservacoesQuitacaoDaOrdemDePagamento",
                column: "QuitacaoDaOrdemDePagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensDePagamento_ItemDeCustoId",
                table: "OrdensDePagamento",
                column: "ItemDeCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_ClienteId",
                table: "Projetos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_QuitacoesDaOrdemDePagamento_OrdemDePagamentoId",
                table: "QuitacoesDaOrdemDePagamento",
                column: "OrdemDePagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_QuitacoesDaOrdemDePagamento_PagamentoDaOrdemDePagamentoId",
                table: "QuitacoesDaOrdemDePagamento",
                column: "PagamentoDaOrdemDePagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosStatusItemDeCusto_ItemDeCustoId",
                table: "RegistrosStatusItemDeCusto",
                column: "ItemDeCustoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ComprovantesDaOrdemDePagamento");

            migrationBuilder.DropTable(
                name: "ObservacoesItem");

            migrationBuilder.DropTable(
                name: "ObservacoesOrdemDePagamento");

            migrationBuilder.DropTable(
                name: "ObservacoesProjeto");

            migrationBuilder.DropTable(
                name: "ObservacoesQuitacaoDaOrdemDePagamento");

            migrationBuilder.DropTable(
                name: "RegistrosStatusItemDeCusto");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "QuitacoesDaOrdemDePagamento");

            migrationBuilder.DropTable(
                name: "OrdensDePagamento");

            migrationBuilder.DropTable(
                name: "PagamentosDaOrdemDePagamento");

            migrationBuilder.DropTable(
                name: "ItensDeCusto");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
