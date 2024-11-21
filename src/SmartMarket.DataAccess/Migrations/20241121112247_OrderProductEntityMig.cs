using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class OrderProductEntityMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_order_transaction_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "transaction_id",
                table: "order");

            migrationBuilder.AddColumn<int>(
                name: "available_count",
                table: "order_product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "is_sold",
                table: "order",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_sales_request_transaction_id",
                table: "sales_request",
                column: "transaction_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sales_request_transaction_id",
                table: "sales_request");

            migrationBuilder.DropColumn(
                name: "available_count",
                table: "order_product");

            migrationBuilder.DropColumn(
                name: "is_sold",
                table: "order");

            migrationBuilder.AddColumn<long>(
                name: "transaction_id",
                table: "order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_order_transaction_id",
                table: "order",
                column: "transaction_id",
                unique: true);
        }
    }
}
