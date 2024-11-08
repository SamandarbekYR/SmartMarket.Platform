using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMarket.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "salary_worker");

            migrationBuilder.DropTable(
                name: "salary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "salary",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    advance = table.Column<double>(type: "double precision", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "salary_worker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    salary_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salary_worker", x => x.id);
                    table.ForeignKey(
                        name: "FK_salary_worker_salary_salary_id",
                        column: x => x.salary_id,
                        principalTable: "salary",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_salary_worker_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_salary_worker_salary_id",
                table: "salary_worker",
                column: "salary_id");

            migrationBuilder.CreateIndex(
                name: "IX_salary_worker_worker_id",
                table: "salary_worker",
                column: "worker_id");
        }
    }
}
