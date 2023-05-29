using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Product");

            migrationBuilder.EnsureSchema(
                name: "User");

            migrationBuilder.CreateTable(
                name: "Type",
                schema: "User",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "User",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<int>(type: "int", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Product",
                columns: table => new
                {
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => new { x.UserID, x.ID });
                    table.ForeignKey(
                        name: "FK_Product_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "User",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "User",
                table: "Type",
                columns: new[] { "ID", "Description" },
                values: new object[] { 4422L, "User" });

            migrationBuilder.InsertData(
                schema: "User",
                table: "Type",
                columns: new[] { "ID", "Description" },
                values: new object[] { 8844L, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Type",
                schema: "User");

            migrationBuilder.DropTable(
                name: "User",
                schema: "User");
        }
    }
}
