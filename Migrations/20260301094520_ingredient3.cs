using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class ingredient3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "targetType",
                table: "Recipes",
                newName: "weaponId");

            migrationBuilder.RenameColumn(
                name: "targetId",
                table: "Recipes",
                newName: "miscId");

            migrationBuilder.AddColumn<int>(
                name: "armorId",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "consumableId",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_armorId",
                table: "Recipes",
                column: "armorId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_consumableId",
                table: "Recipes",
                column: "consumableId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_miscId",
                table: "Recipes",
                column: "miscId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_weaponId",
                table: "Recipes",
                column: "weaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Armors_armorId",
                table: "Recipes",
                column: "armorId",
                principalTable: "Armors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Consumables_consumableId",
                table: "Recipes",
                column: "consumableId",
                principalTable: "Consumables",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Miscs_miscId",
                table: "Recipes",
                column: "miscId",
                principalTable: "Miscs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Weapons_weaponId",
                table: "Recipes",
                column: "weaponId",
                principalTable: "Weapons",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Armors_armorId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Consumables_consumableId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Miscs_miscId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Weapons_weaponId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_armorId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_consumableId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_miscId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_weaponId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "armorId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "consumableId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "weaponId",
                table: "Recipes",
                newName: "targetType");

            migrationBuilder.RenameColumn(
                name: "miscId",
                table: "Recipes",
                newName: "targetId");
        }
    }
}
