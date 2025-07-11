using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itera.Suite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableColumnsToItemDeCusto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AtualizadoPor",
                table: "Projetos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Projetos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AtualizadoPor",
                table: "ItensDeCusto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "ItensDeCusto",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "ItensDeCusto",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "ItensDeCusto",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "Clientes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Clientes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Clientes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ContatoPrincipal",
                table: "Clientes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AtualizadoPor",
                table: "Clientes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "Clientes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Clientes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Clientes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtualizadoPor",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "AtualizadoPor",
                table: "ItensDeCusto");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "ItensDeCusto");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "ItensDeCusto");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "ItensDeCusto");

            migrationBuilder.DropColumn(
                name: "AtualizadoPor",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Clientes");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "Clientes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Clientes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Clientes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContatoPrincipal",
                table: "Clientes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
