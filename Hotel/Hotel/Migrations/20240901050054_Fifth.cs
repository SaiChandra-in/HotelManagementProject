using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Migrations
{
    /// <inheritdoc />
    public partial class Fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "mJG1kmhGtd9RtmkE4OMtrQ==.5+ELEHKXnjP24ZOZ8juHyrKz8Mhf/nRlOici2zwJ60I=", "Fbjxrg7zDGCht0grI4vtLA==.hpuIkwjZ9aKBPCzgeH9ucjTUcV/+Cz8LbZ2de42oHgg=" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 2,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "PHu0eZyfwi6LBTrnax9CZw==.f8F+FouGLpjcValqrtkmiiuWbEojfVkgPxtRa0MAo4k=", "qvPPBbFl8Zv2GNLUTy7ptg==.IepBDJIib0stm/SuX6Ulzc8xiQ8UxA84f0wbR1eB2u0=" });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 3,
                columns: new[] { "ConfirmPassword", "Password" },
                values: new object[] { "/QqOiWuzizqu05/Xkrj3Dw==.rtI5c/uFQIIY4cUv1QAVG33hzhqwRMSb0bCGqSR7/Pg=", "woRqSfmPp6onitQNE4FJPQ==.HFLu/86m+vdLwEw/+FUXwR/oHQYQlSf9XFKD2dDYpwM=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
