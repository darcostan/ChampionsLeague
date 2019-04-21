using Microsoft.EntityFrameworkCore.Migrations;

namespace ChampionsLeagueTest.DAL.Migrations
{
    public partial class Updated_Model_matchteam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatch_Matches_MatchId",
                table: "TeamMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatch_Teams_TeamId",
                table: "TeamMatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMatch",
                table: "TeamMatch");

            migrationBuilder.RenameTable(
                name: "TeamMatch",
                newName: "TeamMatches");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMatch_MatchId",
                table: "TeamMatches",
                newName: "IX_TeamMatches_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMatches",
                table: "TeamMatches",
                columns: new[] { "TeamId", "MatchId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_Matches_MatchId",
                table: "TeamMatches",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_Teams_TeamId",
                table: "TeamMatches",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_Matches_MatchId",
                table: "TeamMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_Teams_TeamId",
                table: "TeamMatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMatches",
                table: "TeamMatches");

            migrationBuilder.RenameTable(
                name: "TeamMatches",
                newName: "TeamMatch");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMatches_MatchId",
                table: "TeamMatch",
                newName: "IX_TeamMatch_MatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMatch",
                table: "TeamMatch",
                columns: new[] { "TeamId", "MatchId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatch_Matches_MatchId",
                table: "TeamMatch",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatch_Teams_TeamId",
                table: "TeamMatch",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
