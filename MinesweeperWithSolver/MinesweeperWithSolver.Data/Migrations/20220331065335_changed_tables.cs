using Microsoft.EntityFrameworkCore.Migrations;

namespace MinesweeperWithSolver.Data.Migrations
{
    public partial class changed_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GamesFailed",
                table: "PlayedGame");

            migrationBuilder.DropColumn(
                name: "GamesPlayed",
                table: "PlayedGame");

            migrationBuilder.DropColumn(
                name: "GamesSolved",
                table: "PlayedGame");

            migrationBuilder.DropColumn(
                name: "MinesFlagged",
                table: "PlayedGame");

            migrationBuilder.DropColumn(
                name: "TilesRevealed",
                table: "PlayedGame");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Simulation",
                newName: "Solver");

            migrationBuilder.RenameColumn(
                name: "Solver",
                table: "PlayedGame",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "GamesFailed",
                table: "Simulation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GamesPlayed",
                table: "Simulation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GamesSolved",
                table: "Simulation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MinesFlagged",
                table: "Simulation",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TilesRevealed",
                table: "Simulation",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GamesFailed",
                table: "Simulation");

            migrationBuilder.DropColumn(
                name: "GamesPlayed",
                table: "Simulation");

            migrationBuilder.DropColumn(
                name: "GamesSolved",
                table: "Simulation");

            migrationBuilder.DropColumn(
                name: "MinesFlagged",
                table: "Simulation");

            migrationBuilder.DropColumn(
                name: "TilesRevealed",
                table: "Simulation");

            migrationBuilder.RenameColumn(
                name: "Solver",
                table: "Simulation",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PlayedGame",
                newName: "Solver");

            migrationBuilder.AddColumn<int>(
                name: "GamesFailed",
                table: "PlayedGame",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GamesPlayed",
                table: "PlayedGame",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GamesSolved",
                table: "PlayedGame",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MinesFlagged",
                table: "PlayedGame",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TilesRevealed",
                table: "PlayedGame",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
