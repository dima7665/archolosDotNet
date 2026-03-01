using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class ingredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "itemType",
                table: "RecipeIngredients",
                newName: "weaponId");

            migrationBuilder.RenameColumn(
                name: "itemId",
                table: "RecipeIngredients",
                newName: "miscId");

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

            migrationBuilder.AddColumn<int>(
                name: "armorId",
                table: "RecipeIngredients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "consumableId",
                table: "RecipeIngredients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateTable(
                name: "Miscs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    additionalInfo = table.Column<string>(type: "text", nullable: true),
                    sources = table.Column<string[]>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miscs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Miscs_RecipeIngredients_id",
                        column: x => x.id,
                        principalTable: "RecipeIngredients",
                        principalColumn: "id");
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Armors_RecipeIngredients_id",
                table: "Armors");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumables_RecipeIngredients_id",
                table: "Consumables");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_Recipes_Recipeid",
                table: "RecipeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Weapons_RecipeIngredients_id",
                table: "Weapons");

            migrationBuilder.DropTable(
                name: "Miscs");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_Recipeid",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "Recipeid",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "armorId",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "consumableId",
                table: "RecipeIngredients");

            migrationBuilder.RenameColumn(
                name: "weaponId",
                table: "RecipeIngredients",
                newName: "itemType");

            migrationBuilder.RenameColumn(
                name: "miscId",
                table: "RecipeIngredients",
                newName: "itemId");

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
        }
    }
}
