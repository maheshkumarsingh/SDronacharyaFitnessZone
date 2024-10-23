using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDronacharyaFitnessZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialAddGymTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gyms",
                columns: table => new
                {
                    GymId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GymDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GymOffers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GymAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GymContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyms", x => x.GymId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_GymId",
                table: "Members",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Gyms_GymId",
                table: "Members",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "GymId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Gyms_GymId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "Gyms");

            migrationBuilder.DropIndex(
                name: "IX_Members_GymId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Members");
        }
    }
}
