using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NitroBoostAuthenticationService.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAccountStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_id",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "unique_username",
                table: "Accounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "verified",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verified",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "unique_username",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "profile_id",
                table: "Accounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
