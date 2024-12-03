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
            migrationBuilder.AddColumn<Guid>(
                name: "partner_id",
                table: "sales_request",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_sales_request_partner_id",
                table: "sales_request",
                column: "partner_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_request_partner_partner_id",
                table: "sales_request",
                column: "partner_id",
                principalTable: "partner",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_request_partner_partner_id",
                table: "sales_request");

            migrationBuilder.DropIndex(
                name: "IX_sales_request_partner_id",
                table: "sales_request");

            migrationBuilder.DropColumn(
                name: "partner_id",
                table: "sales_request");
        }
    }
}
