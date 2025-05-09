using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Migrations
{
    /// <inheritdoc />
    public partial class CreateFacture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "TypeAbonnements",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FactureId",
                table: "AbonnementUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Dateheure = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    StatutAbonnementId = table.Column<int>(type: "integer", nullable: false),
                    Prixtotal = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facture_StatutAbonnement_StatutAbonnementId",
                        column: x => x.StatutAbonnementId,
                        principalTable: "StatutAbonnement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facture_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbonnementUsers_FactureId",
                table: "AbonnementUsers",
                column: "FactureId");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_StatutAbonnementId",
                table: "Facture",
                column: "StatutAbonnementId");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_UserId",
                table: "Facture",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbonnementUsers_Facture_FactureId",
                table: "AbonnementUsers",
                column: "FactureId",
                principalTable: "Facture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbonnementUsers_Facture_FactureId",
                table: "AbonnementUsers");

            migrationBuilder.DropTable(
                name: "Facture");

            migrationBuilder.DropIndex(
                name: "IX_AbonnementUsers_FactureId",
                table: "AbonnementUsers");

            migrationBuilder.DropColumn(
                name: "FactureId",
                table: "AbonnementUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "TypeAbonnements",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
