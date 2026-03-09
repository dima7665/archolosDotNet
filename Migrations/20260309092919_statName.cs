using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class statName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stat",
                table: "ConsumableStats",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumableStats_consumableId_stat_isPermanent",
                table: "ConsumableStats",
                newName: "IX_ConsumableStats_consumableId_name_isPermanent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "ConsumableStats",
                newName: "stat");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumableStats_consumableId_name_isPermanent",
                table: "ConsumableStats",
                newName: "IX_ConsumableStats_consumableId_stat_isPermanent");
        }
    }
}
