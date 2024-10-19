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
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    phone_Number2 = table.Column<string>(type: "text", nullable: false),
                    adress = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "partner",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    total_debt = table.Column<double>(type: "double precision", nullable: false),
                    last_payment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "partners_company",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partners_company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pay_desk",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    income = table.Column<double>(type: "double precision", nullable: false),
                    outlay = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pay_desk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "position",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_position", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "salary",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    advance = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    from = table.Column<string>(type: "text", nullable: false),
                    to = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    type_of_payment = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "worker_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worker_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contr_agent",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contr_agent", x => x.id);
                    table.ForeignKey(
                        name: "FK_contr_agent_partners_company_company_id",
                        column: x => x.company_id,
                        principalTable: "partners_company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "worker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    position_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_roleid = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    img_path = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    password_salt = table.Column<string>(type: "text", nullable: false),
                    salary = table.Column<double>(type: "double precision", nullable: false),
                    advance = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worker", x => x.id);
                    table.ForeignKey(
                        name: "FK_worker_position_position_id",
                        column: x => x.position_id,
                        principalTable: "position",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_worker_worker_role_worker_roleid",
                        column: x => x.worker_roleid,
                        principalTable: "worker_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contr_agent_payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contr_agent_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_debt = table.Column<double>(type: "double precision", nullable: false),
                    total_payment = table.Column<double>(type: "double precision", nullable: false),
                    last_payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contr_agent_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_contr_agent_payment_contr_agent_contr_agent_id",
                        column: x => x.contr_agent_id,
                        principalTable: "contr_agent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "expense",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pay_desk_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    type_of_payment = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expense", x => x.id);
                    table.ForeignKey(
                        name: "FK_expense_pay_desk_pay_desk_id",
                        column: x => x.pay_desk_id,
                        principalTable: "pay_desk",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_expense_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contragent_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    barcode = table.Column<string>(type: "text", nullable: false),
                    pcode = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    sell_price = table.Column<double>(type: "double precision", nullable: false),
                    sell_price_persentage = table.Column<int>(type: "integer", nullable: false),
                    unit_of_measure = table.Column<string>(type: "text", nullable: false),
                    payment_status = table.Column<string>(type: "text", nullable: false),
                    note_amount = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_contr_agent_contragent_id",
                        column: x => x.contragent_id,
                        principalTable: "contr_agent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "salary_check",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    advance_check = table.Column<bool>(type: "boolean", nullable: false),
                    salary_check = table.Column<bool>(type: "boolean", nullable: false),
                    company_debt = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salary_check", x => x.id);
                    table.ForeignKey(
                        name: "FK_salary_check_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "salary_worker",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    salary_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_salary_worker_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesRequest",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    transaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pay_desk_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_cost = table.Column<double>(type: "double precision", nullable: false),
                    cash_sum = table.Column<double>(type: "double precision", nullable: false),
                    card_sum = table.Column<string>(type: "text", nullable: false),
                    transfer_money = table.Column<double>(type: "double precision", nullable: false),
                    debt_sum = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRequest", x => x.id);
                    table.ForeignKey(
                        name: "FK_SalesRequest_pay_desk_pay_desk_id",
                        column: x => x.pay_desk_id,
                        principalTable: "pay_desk",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesRequest_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "worker_debt",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worker_debt", x => x.id);
                    table.ForeignKey(
                        name: "FK_worker_debt_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "debtors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    partner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    debt_sum = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_debtors", x => x.id);
                    table.ForeignKey(
                        name: "FK_debtors_partner_partner_id",
                        column: x => x.partner_id,
                        principalTable: "partner",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_debtors_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "load_report",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contragent_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_price = table.Column<double>(type: "double precision", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_load_report", x => x.id);
                    table.ForeignKey(
                        name: "FK_load_report_contr_agent_contragent_id",
                        column: x => x.contragent_id,
                        principalTable: "contr_agent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_load_report_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_load_report_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    transaction_number = table.Column<string>(type: "text", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    image_path = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_image_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_sale",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sales_request_id = table.Column<Guid>(type: "uuid", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    discount = table.Column<double>(type: "double precision", nullable: false),
                    item_total_cost = table.Column<double>(type: "double precision", nullable: false),
                    PayDeskId = table.Column<Guid>(type: "uuid", nullable: true),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkerId = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_sale", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_sale_SalesRequest_sales_request_id",
                        column: x => x.sales_request_id,
                        principalTable: "SalesRequest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_sale_pay_desk_PayDeskId",
                        column: x => x.PayDeskId,
                        principalTable: "pay_desk",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_product_sale_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_sale_transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "transaction",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_product_sale_worker_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "worker",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "debt_payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    debtor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    debt_sum = table.Column<double>(type: "double precision", nullable: false),
                    payed_sum = table.Column<double>(type: "double precision", nullable: false),
                    remaining_sum = table.Column<double>(type: "double precision", nullable: false),
                    payment_type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_debt_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_debt_payment_debtors_debtor_id",
                        column: x => x.debtor_id,
                        principalTable: "debtors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "invalid_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_sale_id = table.Column<Guid>(type: "uuid", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    return_reason = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invalid_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_invalid_product_product_sale_product_sale_id",
                        column: x => x.product_sale_id,
                        principalTable: "product_sale",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_invalid_product_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "replace_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_sale_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_id = table.Column<Guid>(type: "uuid", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_replace_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_replace_product_product_sale_product_sale_id",
                        column: x => x.product_sale_id,
                        principalTable: "product_sale",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_replace_product_worker_worker_id",
                        column: x => x.worker_id,
                        principalTable: "worker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contr_agent_company_id",
                table: "contr_agent",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_contr_agent_first_name",
                table: "contr_agent",
                column: "first_name");

            migrationBuilder.CreateIndex(
                name: "IX_contr_agent_phone_number",
                table: "contr_agent",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contr_agent_payment_contr_agent_id",
                table: "contr_agent_payment",
                column: "contr_agent_id");

            migrationBuilder.CreateIndex(
                name: "IX_debt_payment_debtor_id",
                table: "debt_payment",
                column: "debtor_id");

            migrationBuilder.CreateIndex(
                name: "IX_debtors_partner_id",
                table: "debtors",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_debtors_product_id",
                table: "debtors",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_expense_pay_desk_id",
                table: "expense",
                column: "pay_desk_id");

            migrationBuilder.CreateIndex(
                name: "IX_expense_worker_id",
                table: "expense",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_invalid_product_product_sale_id",
                table: "invalid_product",
                column: "product_sale_id");

            migrationBuilder.CreateIndex(
                name: "IX_invalid_product_worker_id",
                table: "invalid_product",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_load_report_contragent_id",
                table: "load_report",
                column: "contragent_id");

            migrationBuilder.CreateIndex(
                name: "IX_load_report_product_id",
                table: "load_report",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_load_report_worker_id",
                table: "load_report",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_product_id",
                table: "order",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_worker_id",
                table: "order",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_barcode_pcode_name",
                table: "product",
                columns: new[] { "barcode", "pcode", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_contragent_id",
                table: "product",
                column: "contragent_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_worker_id",
                table: "product",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_image_product_id",
                table: "product_image",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_sale_PayDeskId",
                table: "product_sale",
                column: "PayDeskId");

            migrationBuilder.CreateIndex(
                name: "IX_product_sale_product_id",
                table: "product_sale",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_sale_sales_request_id",
                table: "product_sale",
                column: "sales_request_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_sale_TransactionId",
                table: "product_sale",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_product_sale_WorkerId",
                table: "product_sale",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_replace_product_product_sale_id",
                table: "replace_product",
                column: "product_sale_id");

            migrationBuilder.CreateIndex(
                name: "IX_replace_product_worker_id",
                table: "replace_product",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_salary_check_worker_id",
                table: "salary_check",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_salary_worker_salary_id",
                table: "salary_worker",
                column: "salary_id");

            migrationBuilder.CreateIndex(
                name: "IX_salary_worker_worker_id",
                table: "salary_worker",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_SalesRequest_pay_desk_id",
                table: "SalesRequest",
                column: "pay_desk_id");

            migrationBuilder.CreateIndex(
                name: "IX_SalesRequest_worker_id",
                table: "SalesRequest",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_worker_position_id",
                table: "worker",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "IX_worker_worker_roleid",
                table: "worker",
                column: "worker_roleid");

            migrationBuilder.CreateIndex(
                name: "IX_worker_debt_worker_id",
                table: "worker_debt",
                column: "worker_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contr_agent_payment");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "debt_payment");

            migrationBuilder.DropTable(
                name: "expense");

            migrationBuilder.DropTable(
                name: "invalid_product");

            migrationBuilder.DropTable(
                name: "load_report");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product_image");

            migrationBuilder.DropTable(
                name: "replace_product");

            migrationBuilder.DropTable(
                name: "salary_check");

            migrationBuilder.DropTable(
                name: "salary_worker");

            migrationBuilder.DropTable(
                name: "worker_debt");

            migrationBuilder.DropTable(
                name: "debtors");

            migrationBuilder.DropTable(
                name: "product_sale");

            migrationBuilder.DropTable(
                name: "salary");

            migrationBuilder.DropTable(
                name: "partner");

            migrationBuilder.DropTable(
                name: "SalesRequest");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "pay_desk");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "contr_agent");

            migrationBuilder.DropTable(
                name: "worker");

            migrationBuilder.DropTable(
                name: "partners_company");

            migrationBuilder.DropTable(
                name: "position");

            migrationBuilder.DropTable(
                name: "worker_role");
        }
    }
}
