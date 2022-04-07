using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class test7263 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Vragen",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vragen_TeamId",
                table: "Vragen",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vragen_Team_TeamId",
                table: "Vragen",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vragen_Team_TeamId",
                table: "Vragen");

            migrationBuilder.DropIndex(
                name: "IX_Vragen_TeamId",
                table: "Vragen");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Vragen");
        }
    }
}
