using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class ingredient2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Armors_RecipeIngredients_id",
                table: "Armors");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumables_RecipeIngredients_id",
                table: "Consumables");

            migrationBuilder.DropForeignKey(
                name: "FK_Miscs_RecipeIngredients_id",
                table: "Miscs");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Recipes_Recipeid",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_RecipeIngredients_id",
                table: "Weapons");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_Recipeid",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "Recipeid",
                table: "RecipeIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Weapons",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Miscs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Consumables",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Armors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_armorId",
                table: "RecipeIngredients",
                column: "armorId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Armors_armorId",
                table: "RecipeIngredients",
                column: "armorId",
                principalTable: "Armors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Consumables_consumableId",
                table: "RecipeIngredients",
                column: "consumableId",
                principalTable: "Consumables",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Miscs_miscId",
                table: "RecipeIngredients",
                column: "miscId",
                principalTable: "Miscs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Weapons_weaponId",
                table: "RecipeIngredients",
                column: "weaponId",
                principalTable: "Weapons",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Armors_armorId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Consumables_consumableId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Miscs_miscId",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Weapons_weaponId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_armorId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_consumableId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_miscId",
                table: "RecipeIngredients");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_weaponId",
                table: "RecipeIngredients");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Weapons",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Recipeid",
                table: "RecipeIngredients",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Miscs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Consumables",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Armors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_Recipeid",
                table: "RecipeIngredients",
                column: "Recipeid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Armors_RecipeIngredients_id",
                table: "Armors",
                column: "id",
                principalTable: "RecipeIngredients",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumables_RecipeIngredients_id",
                table: "Consumables",
                column: "id",
                principalTable: "RecipeIngredients",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Miscs_RecipeIngredients_id",
                table: "Miscs",
                column: "id",
                principalTable: "RecipeIngredients",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_Recipes_Recipeid",
                table: "RecipeIngredients",
                column: "Recipeid",
                principalTable: "Recipes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Weapons_RecipeIngredients_id",
                table: "Weapons",
                column: "id",
                principalTable: "RecipeIngredients",
                principalColumn: "id");
        }
    }
}
