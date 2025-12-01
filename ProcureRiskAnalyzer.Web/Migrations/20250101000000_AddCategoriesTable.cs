using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcureRiskAnalyzer.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Categories table
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            // Insert seed data for Categories
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Description" },
                values: new object[,]
                {
                    { 1, "IT Services", "Information Technology Services" },
                    { 2, "Healthcare", "Healthcare and Medical Services" },
                    { 3, "Infrastructure", "Infrastructure and Construction" }
                });

            // Add CategoryId column to Tenders table
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tenders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);

            // Create foreign key
            migrationBuilder.CreateIndex(
                name: "IX_Tenders_CategoryId",
                table: "Tenders",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenders_Categories_CategoryId",
                table: "Tenders",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // Update existing Tenders with CategoryId
            migrationBuilder.Sql("UPDATE Tenders SET CategoryId = 1 WHERE Id = 1");
            migrationBuilder.Sql("UPDATE Tenders SET CategoryId = 2 WHERE Id = 2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenders_Categories_CategoryId",
                table: "Tenders");

            migrationBuilder.DropIndex(
                name: "IX_Tenders_CategoryId",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tenders");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

