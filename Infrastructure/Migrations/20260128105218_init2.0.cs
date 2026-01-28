using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTabs_ContextPart_ContextPartId",
                table: "UserTabs");

            migrationBuilder.DropTable(
                name: "ContextPart");

            migrationBuilder.CreateTable(
                name: "ContextParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContextId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContextParts_Contexts_ContextId",
                        column: x => x.ContextId,
                        principalTable: "Contexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContextParts_ContextId",
                table: "ContextParts",
                column: "ContextId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTabs_ContextParts_ContextPartId",
                table: "UserTabs",
                column: "ContextPartId",
                principalTable: "ContextParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTabs_ContextParts_ContextPartId",
                table: "UserTabs");

            migrationBuilder.DropTable(
                name: "ContextParts");

            migrationBuilder.CreateTable(
                name: "ContextPart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContextId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextPart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContextPart_Contexts_ContextId",
                        column: x => x.ContextId,
                        principalTable: "Contexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContextPart_ContextId",
                table: "ContextPart",
                column: "ContextId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTabs_ContextPart_ContextPartId",
                table: "UserTabs",
                column: "ContextPartId",
                principalTable: "ContextPart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
