using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "contexts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "QrToken",
                table: "contexts",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "contexts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsActive", "QrToken" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "contexts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsActive", "QrToken" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "contexts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsActive", "QrToken" },
                values: new object[] { false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "contexts");

            migrationBuilder.DropColumn(
                name: "QrToken",
                table: "contexts");
        }
    }
}
