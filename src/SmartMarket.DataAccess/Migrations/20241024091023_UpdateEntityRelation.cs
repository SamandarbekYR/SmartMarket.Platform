using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_sale_sales_request_sales_request_id",
                table: "product_sale");

            migrationBuilder.AddForeignKey(
                name: "FK_product_sale_sales_request_sales_request_id",
                table: "product_sale",
                column: "sales_request_id",
                principalTable: "sales_request",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_sale_sales_request_sales_request_id",
                table: "product_sale");

            migrationBuilder.AddForeignKey(
                name: "FK_product_sale_sales_request_sales_request_id",
                table: "product_sale",
                column: "sales_request_id",
                principalTable: "sales_request",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
