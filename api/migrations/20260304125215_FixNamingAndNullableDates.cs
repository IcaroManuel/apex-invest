using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApexInvest.migrations
{
    /// <inheritdoc />
    public partial class FixNamingAndNullableDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MounthlyContribution",
                table: "customers",
                newName: "MonthlyContribution");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeactivatedAt",
                table: "baskets",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthlyContribution",
                table: "customers",
                newName: "MounthlyContribution");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeactivatedAt",
                table: "baskets",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
