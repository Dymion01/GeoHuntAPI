using Microsoft.EntityFrameworkCore.Migrations;

namespace Hakaton.Persistance.Migrations
{
    public partial class Achivements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements");

            migrationBuilder.DropIndex(
                name: "IX_UserAchievements_AchievementId",
                table: "UserAchievements");

            migrationBuilder.DropColumn(
                name: "AchievementId",
                table: "UserAchievements");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_AchivementId",
                table: "UserAchievements",
                column: "AchivementId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Achievements_AchivementId",
                table: "UserAchievements",
                column: "AchivementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Achievements_AchivementId",
                table: "UserAchievements");

            migrationBuilder.DropIndex(
                name: "IX_UserAchievements_AchivementId",
                table: "UserAchievements");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Achievements");

            migrationBuilder.AddColumn<int>(
                name: "AchievementId",
                table: "UserAchievements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
