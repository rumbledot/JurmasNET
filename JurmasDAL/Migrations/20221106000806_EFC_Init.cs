using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JurmasDAL.Migrations
{
    public partial class EFC_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    varchar100 = table.Column<string>(name: "varchar(100)", type: "nvarchar(max)", nullable: false),
                    varchar20 = table.Column<string>(name: "varchar(20)", type: "nvarchar(max)", nullable: false),
                    varchar500 = table.Column<string>(name: "varchar(500)", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jurmases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(20)", nullable: false),
                    Password = table.Column<string>(type: "varchar(20)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(20)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(20)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jurmases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    IsFavourite = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecipeeStatusId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    JurmasId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipees_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recipees_Jurmases_JurmasId",
                        column: x => x.JurmasId,
                        principalTable: "Jurmases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recipees_Statuses_RecipeeStatusId",
                        column: x => x.RecipeeStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastCooked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    varchar500 = table.Column<string>(name: "varchar(500)", type: "nvarchar(max)", nullable: false),
                    RecipeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeeHistory_Recipees_RecipeeId",
                        column: x => x.RecipeeId,
                        principalTable: "Recipees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeeIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    decimal142 = table.Column<decimal>(name: "decimal(14,2)", type: "decimal(18,2)", nullable: false),
                    RecipeeId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeeIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeeIngredients_Recipees_RecipeeId",
                        column: x => x.RecipeeId,
                        principalTable: "Recipees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    varchar100 = table.Column<string>(name: "varchar(100)", type: "nvarchar(max)", nullable: false),
                    ImageBinary = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RecipeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeePhotos_Recipees_RecipeeId",
                        column: x => x.RecipeeId,
                        principalTable: "Recipees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeeSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StepNum = table.Column<int>(type: "int", nullable: false),
                    varchar200 = table.Column<string>(name: "varchar(200)", type: "nvarchar(max)", nullable: false),
                    RecipeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeeSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeeSteps_Recipees_RecipeeId",
                        column: x => x.RecipeeId,
                        principalTable: "Recipees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeeHistory_RecipeeId",
                table: "RecipeeHistory",
                column: "RecipeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeeIngredients_IngredientId",
                table: "RecipeeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeeIngredients_RecipeeId",
                table: "RecipeeIngredients",
                column: "RecipeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeePhotos_RecipeeId",
                table: "RecipeePhotos",
                column: "RecipeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipees_CategoryId",
                table: "Recipees",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipees_JurmasId",
                table: "Recipees",
                column: "JurmasId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipees_RecipeeStatusId",
                table: "Recipees",
                column: "RecipeeStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeeSteps_RecipeeId",
                table: "RecipeeSteps",
                column: "RecipeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeeHistory");

            migrationBuilder.DropTable(
                name: "RecipeeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeePhotos");

            migrationBuilder.DropTable(
                name: "RecipeeSteps");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipees");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Jurmases");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
