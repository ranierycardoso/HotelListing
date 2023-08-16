using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7eccc6d5-0274-44ee-933e-3395c1be2968", "29a2c69f-f107-4d57-adde-8a0f23514120", "Administrator", "ADMINISTRATOR" },
                    { "9bff44fe-09b6-4edf-8e63-fa7f75a1cbcd", "f8fe25c6-949d-4b59-a8ff-5ff454c1dcc6", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7eccc6d5-0274-44ee-933e-3395c1be2968");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bff44fe-09b6-4edf-8e63-fa7f75a1cbcd");
        }
    }
}
