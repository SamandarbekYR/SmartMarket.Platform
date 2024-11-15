using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductOrderMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_pay_desk_pay_desk_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_product_sale_order_OrderId",
                table: "product_sale");

            migrationBuilder.DropIndex(
                name: "IX_product_sale_OrderId",
                table: "product_sale");

            migrationBuilder.DropIndex(
                name: "IX_order_pay_desk_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "product_sale");

            migrationBuilder.DropColumn(
                name: "card_sum",
                table: "order");

            migrationBuilder.DropColumn(
                name: "cash_sum",
                table: "order");

            migrationBuilder.DropColumn(
                name: "debt_sum",
                table: "order");

            migrationBuilder.DropColumn(
                name: "pay_desk_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "total_cost",
                table: "order");

            migrationBuilder.DropColumn(
                name: "transfer_money",
                table: "order");

            migrationBuilder.AlterColumn<double>(
                name: "discount",
                table: "product_sale",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.CreateTable(
                name: "order_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sales_request_id = table.Column<Guid>(type: "uuid", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    item_total_cost = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_product_order_sales_request_id",
                        column: x => x.sales_request_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_product_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_product_product_id",
                table: "order_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_product_sales_request_id",
                table: "order_product",
                column: "sales_request_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_product");

            migrationBuilder.AlterColumn<double>(
                name: "discount",
                table: "product_sale",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "product_sale",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "card_sum",
                table: "order",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "cash_sum",
                table: "order",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "debt_sum",
                table: "order",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "pay_desk_id",
                table: "order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "total_cost",
                table: "order",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "transfer_money",
                table: "order",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_product_sale_OrderId",
                table: "product_sale",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_order_pay_desk_id",
                table: "order",
                column: "pay_desk_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_pay_desk_pay_desk_id",
                table: "order",
                column: "pay_desk_id",
                principalTable: "pay_desk",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_sale_order_OrderId",
                table: "product_sale",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "id");
        }
    }
}
