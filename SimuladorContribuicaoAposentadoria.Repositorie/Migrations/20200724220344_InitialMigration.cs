using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimuladorContribuicaoAposentadoria.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simulacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Salario = table.Column<double>(type: "double precision", nullable: false),
                    Nascimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdadeComQueComecouContribuicao = table.Column<int>(type: "integer", nullable: false),
                    Sexo = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Simulacao_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resultado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantidadeContribuicoesParaPagarIniciandoMesAtual = table.Column<int>(type: "integer", nullable: false),
                    DataUltimaContribuicao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ValorPagoMes = table.Column<double>(type: "double precision", nullable: false),
                    PercentualAplicadoParaCalculo = table.Column<double>(type: "double precision", nullable: false),
                    IdSimulacao = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resultado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resultado_Simulacao_IdSimulacao",
                        column: x => x.IdSimulacao,
                        principalTable: "Simulacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Nome", "Senha" },
                values: new object[] { new Guid("80838d6f-724b-4183-a0e5-03fdaffafb7a"), "admin", "123" });

            migrationBuilder.CreateIndex(
                name: "IX_Resultado_IdSimulacao",
                table: "Resultado",
                column: "IdSimulacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Simulacao_IdUsuario",
                table: "Simulacao",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Nome",
                table: "Usuario",
                column: "Nome",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resultado");

            migrationBuilder.DropTable(
                name: "Simulacao");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
