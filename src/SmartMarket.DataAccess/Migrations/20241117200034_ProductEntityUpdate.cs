using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_product_ProductId",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_ProductId",
                table: "order");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "order",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_ProductId",
                table: "order",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_ProductId",
                table: "order",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "id");
        }
    }
}
