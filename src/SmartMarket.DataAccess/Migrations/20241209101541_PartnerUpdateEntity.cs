using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PartnerUpdateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partner_pay_desk_partner_id",
                table: "partner");

            migrationBuilder.RenameColumn(
                name: "partner_id",
                table: "partner",
                newName: "pay_desk_id");

            migrationBuilder.RenameIndex(
                name: "IX_partner_partner_id",
                table: "partner",
                newName: "IX_partner_pay_desk_id");

            migrationBuilder.AddForeignKey(
                name: "FK_partner_pay_desk_pay_desk_id",
                table: "partner",
                column: "pay_desk_id",
                principalTable: "pay_desk",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partner_pay_desk_pay_desk_id",
                table: "partner");

            migrationBuilder.RenameColumn(
                name: "pay_desk_id",
                table: "partner",
                newName: "partner_id");

            migrationBuilder.RenameIndex(
                name: "IX_partner_pay_desk_id",
                table: "partner",
                newName: "IX_partner_partner_id");

            migrationBuilder.AddForeignKey(
                name: "FK_partner_pay_desk_partner_id",
                table: "partner",
                column: "partner_id",
                principalTable: "pay_desk",
                principalColumn: "id");
        }
    }
}
