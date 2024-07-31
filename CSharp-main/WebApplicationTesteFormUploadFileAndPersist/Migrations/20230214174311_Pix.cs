using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationTesteFormUploadFileAndPersist.Migrations
{
    /// <inheritdoc />
    public partial class Pix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PixTransactionConciliation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Origem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoMovimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EndToEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Condicao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescCondicao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroRemessa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgenciaOrigem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgenciaDestino = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PixTransactionConciliation", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PixTransactionConciliation");
        }
    }
}
