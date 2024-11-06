using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MembershipPlanTableColumnNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipPlans_membership_type_Name",
                table: "MembershipPlans");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "MembershipPlans",
                newName: "MembershipTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipPlans_Name",
                table: "MembershipPlans",
                newName: "IX_MembershipPlans_MembershipTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipPlans_membership_type_MembershipTypeId",
                table: "MembershipPlans",
                column: "MembershipTypeId",
                principalTable: "membership_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipPlans_membership_type_MembershipTypeId",
                table: "MembershipPlans");

            migrationBuilder.RenameColumn(
                name: "MembershipTypeId",
                table: "MembershipPlans",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipPlans_MembershipTypeId",
                table: "MembershipPlans",
                newName: "IX_MembershipPlans_Name");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipPlans_membership_type_Name",
                table: "MembershipPlans",
                column: "Name",
                principalTable: "membership_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
