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
            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "partner",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "partner");
        }
    }
}
