using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workhub_api.Migrations
{
    /// <inheritdoc />
    public partial class reserves_table_adjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntryCode",
                table: "Reserves",
                newName: "AccessCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccessCode",
                table: "Reserves",
                newName: "EntryCode");
        }
    }
}
