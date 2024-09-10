using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "YtblC7Pwht/Ii3rwPrlsFA==.mpOPfFcwc0XG87SXVh0NkFz2vsK51aO6rvUknUyC0FI=", "SdhisSGXT2br8DwxdQuE1w==.e2kk34lLz1bOFy5+zVT0jMnfao2E3giU6Sbfevucp5I=" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 2,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "PUZxdzUc7w3wiiwwcMLNbg==.Ja17kLrn6VVsl1FmoxGaXaRvgKvM+yk+AEpahR56sO0=", "PF+psJKlt5x3Bepiskl0cA==.P+MNClrNXZTV6aavnt6pN6O1RBOAXO8KPWFktUH8uQA=" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 3,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "9V6sDJ6wtOwVPtdN+vc8QA==.pHA1YED0ZKsJRG/MUfRREDGa1wmp7W1gcJYqhpnrFDs=", "v7wWzZts+cFGQj7ptG2rNA==.DNwFkpgj7TbfUtZtZ/SR5uCuQV1vjKsIvLnzX0o8W4M=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "Chandra@123", "Chandra@123" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 2,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "Sameer@123", "Sameer@123" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 3,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "Ashok@123", "Ashok@123" });
        }
    }
}
