using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProcureRiskAnalyzer.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenderCode = table.Column<string>(type: "TEXT", nullable: false),
                    Buyer = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpectedValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "USA", "Microsoft" },
                    { 2, "Ukraine", "EPAM" },
                    { 3, "Poland", "SoftServe" }
                });

            migrationBuilder.InsertData(
                table: "Tenders",
                columns: new[] { "Id", "Buyer", "Date", "ExpectedValue", "SupplierId", "TenderCode" },
                values: new object[,]
                {
                    { 1, "Міністерство освіти", new DateTime(2025, 10, 16, 14, 26, 50, 240, DateTimeKind.Local).AddTicks(3388), 1000000m, 1, "T001" },
                    { 2, "Міністерство охорони здоров’я", new DateTime(2025, 10, 20, 14, 26, 50, 242, DateTimeKind.Local).AddTicks(1315), 750000m, 2, "T002" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenders_SupplierId",
                table: "Tenders",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenders");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
