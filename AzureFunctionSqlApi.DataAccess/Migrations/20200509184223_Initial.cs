using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureFunctionSqlApi.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Rating = table.Column<string>(maxLength: 150, nullable: true),
                    InSchedule = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    DescriptionLong = table.Column<string>(maxLength: 3500, nullable: true),
                    LocalRelease = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<double>(nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: true),
                    HomepageUrl = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShowEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowEventId = table.Column<int>(nullable: false),
                    StartingTime = table.Column<DateTime>(nullable: false),
                    AreaId = table.Column<int>(nullable: true),
                    MovieRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShowEvents_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShowEvents_Movies_MovieRef",
                        column: x => x.MovieRef,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShowEvents_AreaId",
                table: "ShowEvents",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowEvents_MovieRef",
                table: "ShowEvents",
                column: "MovieRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowEvents");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
