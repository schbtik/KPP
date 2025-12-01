using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcureRiskAnalyzer.Web.Migrations
{
    /// <inheritdoc />
    public partial class FixedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tenders",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Tenders",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tenders",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 10, 16, 14, 26, 50, 240, DateTimeKind.Local).AddTicks(3388));

            migrationBuilder.UpdateData(
                table: "Tenders",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 10, 20, 14, 26, 50, 242, DateTimeKind.Local).AddTicks(1315));
        }
    }
}
