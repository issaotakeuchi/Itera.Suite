using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itera.Suite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameProjetoTableToProjetosDeViagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensDeCusto_Projetos_ProjetoDeViagemId",
                table: "ItensDeCusto");

            migrationBuilder.DropForeignKey(
                name: "FK_ObservacoesProjeto_Projetos_ProjetoDeViagemId",
                table: "ObservacoesProjeto");

            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Clientes_ClienteId",
                table: "Projetos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projetos",
                table: "Projetos");

            migrationBuilder.RenameTable(
                name: "Projetos",
                newName: "ProjetosDeViagem");

            migrationBuilder.RenameIndex(
                name: "IX_Projetos_ClienteId",
                table: "ProjetosDeViagem",
                newName: "IX_ProjetosDeViagem_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjetosDeViagem",
                table: "ProjetosDeViagem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensDeCusto_ProjetosDeViagem_ProjetoDeViagemId",
                table: "ItensDeCusto",
                column: "ProjetoDeViagemId",
                principalTable: "ProjetosDeViagem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObservacoesProjeto_ProjetosDeViagem_ProjetoDeViagemId",
                table: "ObservacoesProjeto",
                column: "ProjetoDeViagemId",
                principalTable: "ProjetosDeViagem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjetosDeViagem_Clientes_ClienteId",
                table: "ProjetosDeViagem",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensDeCusto_ProjetosDeViagem_ProjetoDeViagemId",
                table: "ItensDeCusto");

            migrationBuilder.DropForeignKey(
                name: "FK_ObservacoesProjeto_ProjetosDeViagem_ProjetoDeViagemId",
                table: "ObservacoesProjeto");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjetosDeViagem_Clientes_ClienteId",
                table: "ProjetosDeViagem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjetosDeViagem",
                table: "ProjetosDeViagem");

            migrationBuilder.RenameTable(
                name: "ProjetosDeViagem",
                newName: "Projetos");

            migrationBuilder.RenameIndex(
                name: "IX_ProjetosDeViagem_ClienteId",
                table: "Projetos",
                newName: "IX_Projetos_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projetos",
                table: "Projetos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensDeCusto_Projetos_ProjetoDeViagemId",
                table: "ItensDeCusto",
                column: "ProjetoDeViagemId",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObservacoesProjeto_Projetos_ProjetoDeViagemId",
                table: "ObservacoesProjeto",
                column: "ProjetoDeViagemId",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Clientes_ClienteId",
                table: "Projetos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
