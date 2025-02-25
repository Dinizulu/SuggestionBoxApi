using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuggestionBoxApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__19093A2B536F8F83", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UserPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCAC4D3CA7CE", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    SuggestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    SuggestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Suggesti__9409952888F40B11", x => x.SuggestionID);
                    table.ForeignKey(
                        name: "FK__Suggestio__Categ__412EB0B6",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__Suggestio__UserI__403A8C7D",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuggestionID = table.Column<int>(type: "int", nullable: true),
                    FeedbackText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__6A4BEDF65AF723C4", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK__Feedback__Sugges__44FF419A",
                        column: x => x.SuggestionID,
                        principalTable: "Suggestions",
                        principalColumn: "SuggestionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_SuggestionID",
                table: "Feedback",
                column: "SuggestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_CategoryID",
                table: "Suggestions",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_UserID",
                table: "Suggestions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534A253293B",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "([Email] IS NOT NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
