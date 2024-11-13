using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_product_product_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_worker_worker_id",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_sales_request_transaction_id",
                table: "sales_request");

            migrationBuilder.DropColumn(
                name: "count",
                table: "order");

            migrationBuilder.DropColumn(
                name: "transaction_number",
                table: "order");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "order",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_order_product_id",
                table: "order",
                newName: "IX_order_ProductId");

            migrationBuilder.AlterColumn<long>(
                name: "transaction_id",
                table: "sales_request",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "product_sale",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "order",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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
                name: "partner_id",
                table: "order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<long>(
                name: "transaction_id",
                table: "order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<double>(
                name: "transfer_money",
                table: "order",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_worker_role_role_name",
                table: "worker_role",
                column: "role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_sale_OrderId",
                table: "product_sale",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_order_partner_id",
                table: "order",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_pay_desk_id",
                table: "order",
                column: "pay_desk_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_transaction_id",
                table: "order",
                column: "transaction_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_order_partner_partner_id",
                table: "order",
                column: "partner_id",
                principalTable: "partner",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_pay_desk_pay_desk_id",
                table: "order",
                column: "pay_desk_id",
                principalTable: "pay_desk",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_ProductId",
                table: "order",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_worker_worker_id",
                table: "order",
                column: "worker_id",
                principalTable: "worker",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_sale_order_OrderId",
                table: "product_sale",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_partner_partner_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_pay_desk_pay_desk_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_product_ProductId",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_worker_worker_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_product_sale_order_OrderId",
                table: "product_sale");

            migrationBuilder.DropIndex(
                name: "IX_worker_role_role_name",
                table: "worker_role");

            migrationBuilder.DropIndex(
                name: "IX_product_sale_OrderId",
                table: "product_sale");

            migrationBuilder.DropIndex(
                name: "IX_order_partner_id",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_pay_desk_id",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_transaction_id",
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
                name: "partner_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "pay_desk_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "total_cost",
                table: "order");

            migrationBuilder.DropColumn(
                name: "transaction_id",
                table: "order");

            migrationBuilder.DropColumn(
                name: "transfer_money",
                table: "order");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "order",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_ProductId",
                table: "order",
                newName: "IX_order_product_id");

            migrationBuilder.AlterColumn<long>(
                name: "transaction_id",
                table: "sales_request",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "product_id",
                table: "order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "order",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "transaction_number",
                table: "order",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_sales_request_transaction_id",
                table: "sales_request",
                column: "transaction_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_product_id",
                table: "order",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_order_worker_worker_id",
                table: "order",
                column: "worker_id",
                principalTable: "worker",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
