using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreditCardDetails",
                table: "Payments",
                newName: "PaymentStatus");

            migrationBuilder.AddColumn<string>(
                name: "CardDetails",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "Ws5Pwrd6cRBs/3dS9jYBOA==.Qyh4Us3YC5Wh3/Ei+Cp7hSKnBXbhh02s9LwY6YIwg8g=", "r/whwOx/Lw4PDPPuleCIKA==.uAKgfjpvTo98xuqPM/FTu8PQrbq7Lk9wCIAorZNybO0=" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 2,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "nYM1bFmRkpqXlbaORywfKw==.aaA+MgG6JDQUOF5aEagJ38rJMABcWeMGrhA2asFLEjU=", "qN9HSXuQsp7VJ6CNFHrQ+w==.6uHd+f6o7amKVsJNArlhk6Gn/b66lnWZhg9hf+jAWGU=" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 3,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "S0X8rSYFVPjcUdAL050jrg==.s5R82oKBZznzFfKbYktTeL3X7z4+ujtc5z6iOb/Cspg=", "0Fun0DqzjAkH1O8c78eXrA==.RKPwm2HM28i5tbIbSr5ieUO7prKCmARXwEGs7eP0tvA=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardDetails",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "Payments",
                newName: "CreditCardDetails");

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
    }
}
