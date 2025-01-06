using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MentorMemberTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberMentors",
                columns: table => new
                {
                    MentorLoginName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    MemberLoginName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberMentors", x => new { x.MentorLoginName, x.MemberLoginName });
                    table.ForeignKey(
                        name: "FK_MemberMentors_Members_MemberLoginName",
                        column: x => x.MemberLoginName,
                        principalTable: "Members",
                        principalColumn: "MemberLoginName");
                    table.ForeignKey(
                        name: "FK_MemberMentors_Members_MentorLoginName",
                        column: x => x.MentorLoginName,
                        principalTable: "Members",
                        principalColumn: "MemberLoginName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberMentors_MemberLoginName",
                table: "MemberMentors",
                column: "MemberLoginName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberMentors");
        }
    }
}
