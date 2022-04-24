﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinesweeperWithSolver.Data.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayedGame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Solver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GamesPlayed = table.Column<int>(type: "int", nullable: false),
                    GamesSolved = table.Column<int>(type: "int", nullable: false),
                    GamesFailed = table.Column<int>(type: "int", nullable: false),
                    MinesFlagged = table.Column<double>(type: "float", nullable: false),
                    TilesRevealed = table.Column<double>(type: "float", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayedGame", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simulation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayedGame");

            migrationBuilder.DropTable(
                name: "Simulation");
        }
    }
}
