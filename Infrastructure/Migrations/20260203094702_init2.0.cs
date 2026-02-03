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
                name: "FK_ContextParts_Contexts_ContextId",
                table: "ContextParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contexts_Owner_OwnerId",
                table: "Contexts");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_UserTabs_TabId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Products_ProductId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserTabs_TabId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTabs_ContextParts_ContextPartId",
                table: "UserTabs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTabs_Users_UserId",
                table: "UserTabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTabs",
                table: "UserTabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owner",
                table: "Owner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contexts",
                table: "Contexts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContextParts",
                table: "ContextParts");

            migrationBuilder.RenameTable(
                name: "UserTabs",
                newName: "usertabs");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "transactions");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "payments");

            migrationBuilder.RenameTable(
                name: "Owner",
                newName: "owner");

            migrationBuilder.RenameTable(
                name: "Contexts",
                newName: "contexts");

            migrationBuilder.RenameTable(
                name: "ContextParts",
                newName: "contextparts");

            migrationBuilder.RenameIndex(
                name: "IX_UserTabs_UserId",
                table: "usertabs",
                newName: "IX_usertabs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTabs_ContextPartId",
                table: "usertabs",
                newName: "IX_usertabs_ContextPartId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserId",
                table: "transactions",
                newName: "IX_transactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TabId",
                table: "transactions",
                newName: "IX_transactions_TabId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ProductId",
                table: "transactions",
                newName: "IX_transactions_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_UserId",
                table: "payments",
                newName: "IX_payments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_TabId",
                table: "payments",
                newName: "IX_payments_TabId");

            migrationBuilder.RenameIndex(
                name: "IX_Contexts_OwnerId",
                table: "contexts",
                newName: "IX_contexts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ContextParts_ContextId",
                table: "contextparts",
                newName: "IX_contextparts_ContextId");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usertabs",
                table: "usertabs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_transactions",
                table: "transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payments",
                table: "payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_owner",
                table: "owner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contexts",
                table: "contexts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contextparts",
                table: "contextparts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_contextparts_contexts_ContextId",
                table: "contextparts",
                column: "ContextId",
                principalTable: "contexts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_contexts_owner_OwnerId",
                table: "contexts",
                column: "OwnerId",
                principalTable: "owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_users_UserId",
                table: "payments",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_usertabs_TabId",
                table: "payments",
                column: "TabId",
                principalTable: "usertabs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_products_ProductId",
                table: "transactions",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_users_UserId",
                table: "transactions",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_usertabs_TabId",
                table: "transactions",
                column: "TabId",
                principalTable: "usertabs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_usertabs_contextparts_ContextPartId",
                table: "usertabs",
                column: "ContextPartId",
                principalTable: "contextparts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_usertabs_users_UserId",
                table: "usertabs",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contextparts_contexts_ContextId",
                table: "contextparts");

            migrationBuilder.DropForeignKey(
                name: "FK_contexts_owner_OwnerId",
                table: "contexts");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_users_UserId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_usertabs_TabId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_products_ProductId",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_users_UserId",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_usertabs_TabId",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_usertabs_contextparts_ContextPartId",
                table: "usertabs");

            migrationBuilder.DropForeignKey(
                name: "FK_usertabs_users_UserId",
                table: "usertabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usertabs",
                table: "usertabs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_transactions",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payments",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_owner",
                table: "owner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contexts",
                table: "contexts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contextparts",
                table: "contextparts");

            migrationBuilder.RenameTable(
                name: "usertabs",
                newName: "UserTabs");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "transactions",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "payments",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "owner",
                newName: "Owner");

            migrationBuilder.RenameTable(
                name: "contexts",
                newName: "Contexts");

            migrationBuilder.RenameTable(
                name: "contextparts",
                newName: "ContextParts");

            migrationBuilder.RenameIndex(
                name: "IX_usertabs_UserId",
                table: "UserTabs",
                newName: "IX_UserTabs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_usertabs_ContextPartId",
                table: "UserTabs",
                newName: "IX_UserTabs_ContextPartId");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_UserId",
                table: "Transactions",
                newName: "IX_Transactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_TabId",
                table: "Transactions",
                newName: "IX_Transactions_TabId");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_ProductId",
                table: "Transactions",
                newName: "IX_Transactions_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_UserId",
                table: "Payments",
                newName: "IX_Payments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_TabId",
                table: "Payments",
                newName: "IX_Payments_TabId");

            migrationBuilder.RenameIndex(
                name: "IX_contexts_OwnerId",
                table: "Contexts",
                newName: "IX_Contexts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_contextparts_ContextId",
                table: "ContextParts",
                newName: "IX_ContextParts_ContextId");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTabs",
                table: "UserTabs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owner",
                table: "Owner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contexts",
                table: "Contexts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContextParts",
                table: "ContextParts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContextParts_Contexts_ContextId",
                table: "ContextParts",
                column: "ContextId",
                principalTable: "Contexts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contexts_Owner_OwnerId",
                table: "Contexts",
                column: "OwnerId",
                principalTable: "Owner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_UserTabs_TabId",
                table: "Payments",
                column: "TabId",
                principalTable: "UserTabs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UserId",
                table: "Payments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Products_ProductId",
                table: "Transactions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserTabs_TabId",
                table: "Transactions",
                column: "TabId",
                principalTable: "UserTabs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTabs_ContextParts_ContextPartId",
                table: "UserTabs",
                column: "ContextPartId",
                principalTable: "ContextParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTabs_Users_UserId",
                table: "UserTabs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
