using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFacture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeAbonnementId",
                table: "Facture",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Facture_TypeAbonnementId",
                table: "Facture",
                column: "TypeAbonnementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facture_TypeAbonnements_TypeAbonnementId",
                table: "Facture",
                column: "TypeAbonnementId",
                principalTable: "TypeAbonnements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facture_TypeAbonnements_TypeAbonnementId",
                table: "Facture");

            migrationBuilder.DropIndex(
                name: "IX_Facture_TypeAbonnementId",
                table: "Facture");

            migrationBuilder.DropColumn(
                name: "TypeAbonnementId",
                table: "Facture");
        }
    }
}
