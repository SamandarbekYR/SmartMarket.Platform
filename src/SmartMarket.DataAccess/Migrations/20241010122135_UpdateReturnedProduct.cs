using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReturnedProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contr_agent_first_name_phone_number",
                table: "contr_agent");

            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "replace_product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "invalid_product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_contr_agent_first_name",
                table: "contr_agent",
                column: "first_name");

            migrationBuilder.CreateIndex(
                name: "IX_contr_agent_phone_number",
                table: "contr_agent",
                column: "phone_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contr_agent_first_name",
                table: "contr_agent");

            migrationBuilder.DropIndex(
                name: "IX_contr_agent_phone_number",
                table: "contr_agent");

            migrationBuilder.DropColumn(
                name: "count",
                table: "replace_product");

            migrationBuilder.DropColumn(
                name: "count",
                table: "invalid_product");

            migrationBuilder.CreateIndex(
                name: "IX_contr_agent_first_name_phone_number",
                table: "contr_agent",
                columns: new[] { "first_name", "phone_number" },
                unique: true);
        }
    }
}
