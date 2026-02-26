using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class consumabledb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableStats_Items_consumableId",
                table: "ConsumableStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Consumables");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumables",
                table: "Consumables",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableStats_Consumables_consumableId",
                table: "ConsumableStats",
                column: "consumableId",
                principalTable: "Consumables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableStats_Consumables_consumableId",
                table: "ConsumableStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumables",
                table: "Consumables");

            migrationBuilder.RenameTable(
                name: "Consumables",
                newName: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "id");

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
