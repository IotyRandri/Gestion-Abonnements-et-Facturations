using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAbonnementUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbonnementUsers_Facture_FactureId",
                table: "AbonnementUsers");

            migrationBuilder.AlterColumn<int>(
                name: "FactureId",
                table: "AbonnementUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AbonnementUsers_Facture_FactureId",
                table: "AbonnementUsers",
                column: "FactureId",
                principalTable: "Facture",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbonnementUsers_Facture_FactureId",
                table: "AbonnementUsers");

            migrationBuilder.AlterColumn<int>(
                name: "FactureId",
                table: "AbonnementUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AbonnementUsers_Facture_FactureId",
                table: "AbonnementUsers",
                column: "FactureId",
                principalTable: "Facture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
