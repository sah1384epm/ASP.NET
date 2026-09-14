using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4542dc03-d3c4-4e3a-bd31-0241976947ae", "041bbfd6-14fe-47f0-970c-fdbe1049be40", "Admin", "ADMIN" },
                    { "62068eef-20f9-448e-82f5-7327abbbc3ac", "488c47e6-34ae-4d9d-92e7-6373ea7297e5", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4542dc03-d3c4-4e3a-bd31-0241976947ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62068eef-20f9-448e-82f5-7327abbbc3ac");
        }
    }
}
