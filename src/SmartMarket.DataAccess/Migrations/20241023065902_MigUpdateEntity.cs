using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MigUpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_sale_SalesRequest_sales_request_id",
                table: "product_sale");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequest_pay_desk_pay_desk_id",
                table: "SalesRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequest_worker_worker_id",
                table: "SalesRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesRequest",
                table: "SalesRequest");

            migrationBuilder.RenameTable(
                name: "SalesRequest",
                newName: "sales_request");

            migrationBuilder.RenameIndex(
                name: "IX_SalesRequest_worker_id",
                table: "sales_request",
                newName: "IX_sales_request_worker_id");

            migrationBuilder.RenameIndex(
                name: "IX_SalesRequest_pay_desk_id",
                table: "sales_request",
                newName: "IX_sales_request_pay_desk_id");

            migrationBuilder.AlterColumn<long>(
                name: "transaction_id",
                table: "sales_request",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_sales_request",
                table: "sales_request",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_request_transaction_id",
                table: "sales_request",
                column: "transaction_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_product_sale_sales_request_sales_request_id",
                table: "product_sale",
                column: "sales_request_id",
                principalTable: "sales_request",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sales_request_pay_desk_pay_desk_id",
                table: "sales_request",
                column: "pay_desk_id",
                principalTable: "pay_desk",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sales_request_worker_worker_id",
                table: "sales_request",
                column: "worker_id",
                principalTable: "worker",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_sale_sales_request_sales_request_id",
                table: "product_sale");

            migrationBuilder.DropForeignKey(
                name: "FK_sales_request_pay_desk_pay_desk_id",
                table: "sales_request");

            migrationBuilder.DropForeignKey(
                name: "FK_sales_request_worker_worker_id",
                table: "sales_request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sales_request",
                table: "sales_request");

            migrationBuilder.DropIndex(
                name: "IX_sales_request_transaction_id",
                table: "sales_request");

            migrationBuilder.RenameTable(
                name: "sales_request",
                newName: "SalesRequest");

            migrationBuilder.RenameIndex(
                name: "IX_sales_request_worker_id",
                table: "SalesRequest",
                newName: "IX_SalesRequest_worker_id");

            migrationBuilder.RenameIndex(
                name: "IX_sales_request_pay_desk_id",
                table: "SalesRequest",
                newName: "IX_SalesRequest_pay_desk_id");

            migrationBuilder.AlterColumn<long>(
                name: "transaction_id",
                table: "SalesRequest",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesRequest",
                table: "SalesRequest",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_sale_SalesRequest_sales_request_id",
                table: "product_sale",
                column: "sales_request_id",
                principalTable: "SalesRequest",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequest_pay_desk_pay_desk_id",
                table: "SalesRequest",
                column: "pay_desk_id",
                principalTable: "pay_desk",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequest_worker_worker_id",
                table: "SalesRequest",
                column: "worker_id",
                principalTable: "worker",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
