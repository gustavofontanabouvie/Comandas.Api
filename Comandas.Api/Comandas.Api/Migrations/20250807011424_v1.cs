using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comandas.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardapioItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PossuiPreparo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardapioItens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroMesa = table.Column<int>(type: "int", nullable: false),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SituacaoComanda = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comandas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    SituacaoMesa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComandaItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComandaId = table.Column<int>(type: "int", nullable: false),
                    CardapioItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComandaItens_CardapioItens_CardapioItemId",
                        column: x => x.CardapioItemId,
                        principalTable: "CardapioItens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaItens_Comandas_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidosCozinha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComandaId = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosCozinha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosCozinha_Comandas_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoCozinhaItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoCozinhaId = table.Column<int>(type: "int", nullable: false),
                    ComandaItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoCozinhaItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoCozinhaItens_ComandaItens_ComandaItemId",
                        column: x => x.ComandaItemId,
                        principalTable: "ComandaItens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PedidoCozinhaItens_PedidosCozinha_PedidoCozinhaId",
                        column: x => x.PedidoCozinhaId,
                        principalTable: "PedidosCozinha",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComandaItens_CardapioItemId",
                table: "ComandaItens",
                column: "CardapioItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaItens_ComandaId",
                table: "ComandaItens",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCozinhaItens_ComandaItemId",
                table: "PedidoCozinhaItens",
                column: "ComandaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoCozinhaItens_PedidoCozinhaId",
                table: "PedidoCozinhaItens",
                column: "PedidoCozinhaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosCozinha_ComandaId",
                table: "PedidosCozinha",
                column: "ComandaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "PedidoCozinhaItens");

            migrationBuilder.DropTable(
                name: "ComandaItens");

            migrationBuilder.DropTable(
                name: "PedidosCozinha");

            migrationBuilder.DropTable(
                name: "CardapioItens");

            migrationBuilder.DropTable(
                name: "Comandas");
        }
    }
}
