using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class ingredient5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_consumableId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_miscId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_weaponId",
                table: "RecipeIngredients");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_consumableId",
                table: "RecipeIngredients",
                column: "consumableId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_miscId",
                table: "RecipeIngredients",
                column: "miscId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_weaponId",
                table: "RecipeIngredients",
                column: "weaponId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_consumableId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_miscId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_weaponId",
                table: "RecipeIngredients");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_consumableId",
                table: "RecipeIngredients",
                column: "consumableId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_miscId",
                table: "RecipeIngredients",
                column: "miscId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_weaponId",
                table: "RecipeIngredients",
                column: "weaponId",
                unique: true);
        }
    }
}
