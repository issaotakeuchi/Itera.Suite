using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itera.Suite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdemDePagamentoModulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObservacoesPagamento");

            migrationBuilder.DropTable(
                name: "PagamentosProgramados");

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
                name: "ObservacoesOrdemDePagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdemDeProgramadoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Texto = table.Column<string>(type: "text", nullable: false),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Autor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservacoesOrdemDePagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservacoesOrdemDePagamento_OrdensDePagamento_OrdemDeProgra~",
                        column: x => x.OrdemDeProgramadoId,
                        principalTable: "OrdensDePagamento",
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
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Autor = table.Column<string>(type: "text", nullable: false)
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
                name: "IX_ComprovantesDaOrdemDePagamento_PagamentoDaOrdemDePagamentoId",
                table: "ComprovantesDaOrdemDePagamento",
                column: "PagamentoDaOrdemDePagamentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ObservacoesOrdemDePagamento_OrdemDeProgramadoId",
                table: "ObservacoesOrdemDePagamento",
                column: "OrdemDeProgramadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservacoesQuitacaoDaOrdemDePagamento_QuitacaoDaOrdemDePaga~",
                table: "ObservacoesQuitacaoDaOrdemDePagamento",
                column: "QuitacaoDaOrdemDePagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensDePagamento_ItemDeCustoId",
                table: "OrdensDePagamento",
                column: "ItemDeCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_QuitacoesDaOrdemDePagamento_OrdemDePagamentoId",
                table: "QuitacoesDaOrdemDePagamento",
                column: "OrdemDePagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_QuitacoesDaOrdemDePagamento_PagamentoDaOrdemDePagamentoId",
                table: "QuitacoesDaOrdemDePagamento",
                column: "PagamentoDaOrdemDePagamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComprovantesDaOrdemDePagamento");

            migrationBuilder.DropTable(
                name: "ObservacoesOrdemDePagamento");

            migrationBuilder.DropTable(
                name: "ObservacoesQuitacaoDaOrdemDePagamento");

            migrationBuilder.DropTable(
                name: "QuitacoesDaOrdemDePagamento");

            migrationBuilder.DropTable(
                name: "OrdensDePagamento");

            migrationBuilder.DropTable(
                name: "PagamentosDaOrdemDePagamento");

            migrationBuilder.CreateTable(
                name: "PagamentosProgramados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemDeCustoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComprovanteUrl = table.Column<string>(type: "text", nullable: true),
                    DataPrevista = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Forma = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentosProgramados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentosProgramados_ItensDeCusto_ItemDeCustoId",
                        column: x => x.ItemDeCustoId,
                        principalTable: "ItensDeCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservacoesPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PagamentoProgramadoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Autor = table.Column<string>(type: "text", nullable: false),
                    DataHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Texto = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservacoesPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservacoesPagamento_PagamentosProgramados_PagamentoProgram~",
                        column: x => x.PagamentoProgramadoId,
                        principalTable: "PagamentosProgramados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObservacoesPagamento_PagamentoProgramadoId",
                table: "ObservacoesPagamento",
                column: "PagamentoProgramadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentosProgramados_ItemDeCustoId",
                table: "PagamentosProgramados",
                column: "ItemDeCustoId");
        }
    }
}
