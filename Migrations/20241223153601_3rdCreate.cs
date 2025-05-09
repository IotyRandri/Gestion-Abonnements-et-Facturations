using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Migrations
{
    /// <inheritdoc />
    public partial class _3rdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CommissionAnnulation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Prixmin = table.Column<double>(type: "double precision", nullable: false),
                    Prixmax = table.Column<double>(type: "double precision", nullable: false),
                    Pourcentage = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionAnnulation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatutAbonnement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Intitule = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatutAbonnement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbonnementUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AbonnementId = table.Column<int>(type: "integer", nullable: false),
                    NomFacture = table.Column<string>(type: "text", nullable: true),
                    StatutAbonnementId = table.Column<int>(type: "integer", nullable: false),
                    DateHeureDebut = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DateHeureFin = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbonnementUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbonnementUsers_Abonnement_AbonnementId",
                        column: x => x.AbonnementId,
                        principalTable: "Abonnement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbonnementUsers_StatutAbonnement_StatutAbonnementId",
                        column: x => x.StatutAbonnementId,
                        principalTable: "StatutAbonnement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbonnementUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbonnementUsers_AbonnementId",
                table: "AbonnementUsers",
                column: "AbonnementId");

            migrationBuilder.CreateIndex(
                name: "IX_AbonnementUsers_StatutAbonnementId",
                table: "AbonnementUsers",
                column: "StatutAbonnementId");

            migrationBuilder.CreateIndex(
                name: "IX_AbonnementUsers_UserId",
                table: "AbonnementUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbonnementUsers");

            migrationBuilder.DropTable(
                name: "CommissionAnnulation");

            migrationBuilder.DropTable(
                name: "StatutAbonnement");

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
