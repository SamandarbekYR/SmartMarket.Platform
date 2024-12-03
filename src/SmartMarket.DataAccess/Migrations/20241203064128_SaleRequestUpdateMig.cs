using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SaleRequestUpdateMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_request_partner_partner_id",
                table: "sales_request");

            migrationBuilder.AlterColumn<Guid>(
                name: "partner_id",
                table: "sales_request",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_request_partner_partner_id",
                table: "sales_request",
                column: "partner_id",
                principalTable: "partner",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_request_partner_partner_id",
                table: "sales_request");

            migrationBuilder.AlterColumn<Guid>(
                name: "partner_id",
                table: "sales_request",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_sales_request_partner_partner_id",
                table: "sales_request",
                column: "partner_id",
                principalTable: "partner",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
