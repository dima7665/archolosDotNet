using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class consumable6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableStats_Items_consumableId",
                table: "ConsumableStats");

            migrationBuilder.DropIndex(
                name: "IX_ConsumableStats_consumableId",
                table: "ConsumableStats");

            migrationBuilder.AddColumn<int>(
                name: "BaseItemid",
                table: "ConsumableStats",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableStats_BaseItemid",
                table: "ConsumableStats",
                column: "BaseItemid");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableStats_Items_BaseItemid",
                table: "ConsumableStats",
                column: "BaseItemid",
                principalTable: "Items",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableStats_Items_BaseItemid",
                table: "ConsumableStats");

            migrationBuilder.DropIndex(
                name: "IX_ConsumableStats_BaseItemid",
                table: "ConsumableStats");

            migrationBuilder.DropColumn(
                name: "BaseItemid",
                table: "ConsumableStats");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableStats_consumableId",
                table: "ConsumableStats",
                column: "consumableId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableStats_Items_consumableId",
                table: "ConsumableStats",
                column: "consumableId",
                principalTable: "Items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
