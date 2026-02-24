using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class consumable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ConsumableStats",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stat = table.Column<int>(type: "integer", nullable: false),
                    isPermanent = table.Column<bool>(type: "boolean", nullable: false),
                    isPercentage = table.Column<bool>(type: "boolean", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: true),
                    value = table.Column<int>(type: "integer", nullable: true),
                    consumableId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumableStats", x => x.id);
                    table.ForeignKey(
                        name: "FK_ConsumableStats_Items_consumableId",
                        column: x => x.consumableId,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableStats_consumableId",
                table: "ConsumableStats",
                column: "consumableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumableStats");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Items");
        }
    }
}
