using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class consumableunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConsumableStats_consumableId",
                table: "ConsumableStats");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableStats_consumableId_stat_isPermanent",
                table: "ConsumableStats",
                columns: new[] { "consumableId", "stat", "isPermanent" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConsumableStats_consumableId_stat_isPermanent",
                table: "ConsumableStats");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableStats_consumableId",
                table: "ConsumableStats",
                column: "consumableId");
        }
    }
}
