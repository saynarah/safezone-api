using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace safezone.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToOccurrence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Occurrences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Occurrences");
        }
    }
}
