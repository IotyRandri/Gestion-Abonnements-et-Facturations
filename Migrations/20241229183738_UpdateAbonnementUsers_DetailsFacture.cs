using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet_.NET_Core_M2_Devops_Mr_Naina.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAbonnementUsers_DetailsFacture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PrixCommission",
                table: "AbonnementUsers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PrixEstimation",
                table: "AbonnementUsers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PrixTotal",
                table: "AbonnementUsers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrixCommission",
                table: "AbonnementUsers");

            migrationBuilder.DropColumn(
                name: "PrixEstimation",
                table: "AbonnementUsers");

            migrationBuilder.DropColumn(
                name: "PrixTotal",
                table: "AbonnementUsers");
        }
    }
}
