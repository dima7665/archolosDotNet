using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class weapon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<int>(type: "integer", nullable: false),
                    damage = table.Column<int>(type: "integer", nullable: false),
                    damageType = table.Column<int>(type: "integer", nullable: false),
                    range = table.Column<int>(type: "integer", nullable: true),
                    armorPiercing = table.Column<int>(type: "integer", nullable: true),
                    skill = table.Column<int>(type: "integer", nullable: false),
                    skillRequirement = table.Column<int>(type: "integer", nullable: true),
                    skillBonus = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    additionalInfo = table.Column<string>(type: "text", nullable: true),
                    sources = table.Column<string[]>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weapons");
        }
    }
}
