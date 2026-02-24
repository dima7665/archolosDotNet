using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace archolosDotNet.Migrations
{
    /// <inheritdoc />
    public partial class consumable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }
    }
}
