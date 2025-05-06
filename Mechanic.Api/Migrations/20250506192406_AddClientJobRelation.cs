using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mechanic.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddClientJobRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Jobs_customerId",
                table: "Jobs",
                column: "customerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Clients_customerId",
                table: "Jobs",
                column: "customerId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Clients_customerId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_customerId",
                table: "Jobs");
        }
    }
}
