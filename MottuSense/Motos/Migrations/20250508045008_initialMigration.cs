using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motos.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_EVENTO",
                columns: table => new
                {
                    IdEvento = table.Column<string>(type: "VARCHAR2(255)", nullable: false),
                    DescricaoEvento = table.Column<string>(type: "VARCHAR2(500)", nullable: false),
                    CorEvento = table.Column<string>(type: "VARCHAR2(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EVENTO", x => x.IdEvento);
                });

            migrationBuilder.CreateTable(
                name: "TB_PATIO",
                columns: table => new
                {
                    IdPatio = table.Column<string>(type: "VARCHAR2(255)", nullable: false),
                    IdFilial = table.Column<string>(type: "VARCHAR2(255)", nullable: false),
                    EstruturaPatioCriada = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PATIO", x => x.IdPatio);
                });

            migrationBuilder.CreateTable(
                name: "TB_ESTRUTURA_PATIO",
                columns: table => new
                {
                    IdPatio = table.Column<string>(type: "VARCHAR2(255)", nullable: false),
                    CoordenadaXEstrutura = table.Column<decimal>(type: "NUMBER", nullable: false),
                    CoordenadaYEstrutura = table.Column<decimal>(type: "NUMBER", nullable: false),
                    TamanhoEstrutura = table.Column<decimal>(type: "NUMBER(38,17)", nullable: false),
                    RotacaoEstrutura = table.Column<decimal>(type: "NUMBER(38,17)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ESTRUTURA_PATIO", x => x.IdPatio);
                    table.ForeignKey(
                        name: "FK_TB_ESTRUTURA_PATIO_TB_PATIO_IdPatio",
                        column: x => x.IdPatio,
                        principalTable: "TB_PATIO",
                        principalColumn: "IdPatio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_MOTO",
                columns: table => new
                {
                    IdMoto = table.Column<string>(type: "VARCHAR2(255)", nullable: false),
                    PlacaMoto = table.Column<string>(type: "VARCHAR2(50)", nullable: false),
                    ModeloMoto = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    StatusMoto = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    ChassiMoto = table.Column<string>(type: "VARCHAR2(500)", nullable: false),
                    IotMoto = table.Column<string>(type: "VARCHAR2(500)", nullable: false),
                    IdPatio = table.Column<string>(type: "VARCHAR2(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MOTO", x => x.IdMoto);
                    table.ForeignKey(
                        name: "FK_TB_MOTO_TB_PATIO_IdPatio",
                        column: x => x.IdPatio,
                        principalTable: "TB_PATIO",
                        principalColumn: "IdPatio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_EVENTO_MOTO",
                columns: table => new
                {
                    IdEventoMoto = table.Column<string>(type: "VARCHAR2(255)", nullable: false),
                    IdMoto = table.Column<string>(type: "VARCHAR2(255)", nullable: false),
                    IdEvento = table.Column<string>(type: "VARCHAR2(255)", nullable: false),
                    EventoVisualizado = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DataHoraEvento = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EVENTO_MOTO", x => x.IdEventoMoto);
                    table.ForeignKey(
                        name: "FK_TB_EVENTO_MOTO_TB_EVENTO_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "TB_EVENTO",
                        principalColumn: "IdEvento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_EVENTO_MOTO_TB_MOTO_IdMoto",
                        column: x => x.IdMoto,
                        principalTable: "TB_MOTO",
                        principalColumn: "IdMoto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_LOCALIZACAO_MOTO",
                columns: table => new
                {
                    IdMoto = table.Column<string>(type: "VARCHAR2(255)", nullable: false),
                    LatitudeMoto = table.Column<string>(type: "VARCHAR2(500)", nullable: false),
                    LongitudeMoto = table.Column<string>(type: "VARCHAR2(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LOCALIZACAO_MOTO", x => x.IdMoto);
                    table.ForeignKey(
                        name: "FK_TB_LOCALIZACAO_MOTO_TB_MOTO_IdMoto",
                        column: x => x.IdMoto,
                        principalTable: "TB_MOTO",
                        principalColumn: "IdMoto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTO_MOTO_IdEvento",
                table: "TB_EVENTO_MOTO",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTO_MOTO_IdMoto",
                table: "TB_EVENTO_MOTO",
                column: "IdMoto");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MOTO_IdPatio",
                table: "TB_MOTO",
                column: "IdPatio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ESTRUTURA_PATIO");

            migrationBuilder.DropTable(
                name: "TB_EVENTO_MOTO");

            migrationBuilder.DropTable(
                name: "TB_LOCALIZACAO_MOTO");

            migrationBuilder.DropTable(
                name: "TB_EVENTO");

            migrationBuilder.DropTable(
                name: "TB_MOTO");

            migrationBuilder.DropTable(
                name: "TB_PATIO");
        }
    }
}
