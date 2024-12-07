using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ScaleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sclaes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    update_time_interval = table.Column<int>(type: "integer", nullable: false),
                    select_file_path = table.Column<string>(type: "text", nullable: false),
                    txt_file_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sclaes", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sclaes");
        }
    }
}
