using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeperateMembershipAndMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_membership_type_MembershipType",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_MembershipType",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MembershipType",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "Weight",
                table: "Supplements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "Supplements",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "MembershipType",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MembershipType",
                table: "Members",
                column: "MembershipType");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_membership_type_MembershipType",
                table: "Members",
                column: "MembershipType",
                principalTable: "membership_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
