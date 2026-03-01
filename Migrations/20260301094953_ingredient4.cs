using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class ingredient4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Armors_armorId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Armors_armorId",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "weaponId",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "miscId",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "consumableId",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "armorId",
                table: "Recipes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "weaponId",
                table: "RecipeIngredients",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "miscId",
                table: "RecipeIngredients",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "consumableId",
                table: "RecipeIngredients",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "armorId",
                table: "RecipeIngredients",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Armors_armorId",
                table: "RecipeIngredients",
                column: "armorId",
                principalTable: "Armors",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Armors_armorId",
                table: "Recipes",
                column: "armorId",
                principalTable: "Armors",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Armors_armorId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Armors_armorId",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "weaponId",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "miscId",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "consumableId",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "armorId",
                table: "Recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "weaponId",
                table: "RecipeIngredients",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "miscId",
                table: "RecipeIngredients",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "consumableId",
                table: "RecipeIngredients",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "armorId",
                table: "RecipeIngredients",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Armors_armorId",
                table: "RecipeIngredients",
                column: "armorId",
                principalTable: "Armors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Armors_armorId",
                table: "Recipes",
                column: "armorId",
                principalTable: "Armors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
