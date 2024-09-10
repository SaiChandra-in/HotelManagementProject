using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "AVkitIBXy3gv9YVyn76IzQ==.IhlIdY1idvI2hfOIVwyd8Im3d1HgiImSWbBiVSGzn6o=", "ayEvXs5WRcRG62U0cAsKZg==.6AF4T1ggPtdV9YSg5cxi6EyhBdfoiyQW2b+drA8TOAM=" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 2,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "g5vTMAo6tkK/pB6RsUspNg==.yQn5rmVybgZBkOtvZSZsyR5n6vCgTBwj9yBckwWxK7w=", "idigWFJZ+rcWVOz2+1lWcA==.i1adSZEUlLfmG8KrQREYTzIE7Gs9FeidOeMKTGKmO28=" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 3,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "GtzkJxP6JLDto0U7EiB+eA==./F8y1bKJPrBL3i6OEta0489g47RnhNGH4HPjhtMCZ7k=", "pWJEl/j+Tc7Ew16q1Pp9CA==.YdYpU7YLixaIhqyZHWr9sh4ngxo5V5C3Fn5m0uxnng4=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
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
    }
}
