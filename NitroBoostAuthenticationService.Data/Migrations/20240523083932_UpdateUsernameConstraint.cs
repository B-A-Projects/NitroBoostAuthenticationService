using Microsoft.EntityFrameworkCore.Migrations;
using NitroBoostAuthenticationService.Data.Entities;

#nullable disable

namespace NitroBoostAuthenticationService.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsernameConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<Account>(
            //     name: "unique_username",
            //     table: "Accounts",
            //     nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<Account>(
            //     name: "unique_username",
            //     table: "Accounts",
            //     nullable: false);
        }
    }
}
