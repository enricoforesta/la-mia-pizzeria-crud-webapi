using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class MigrationImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PizzaImg",
                table: "Pizza");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageFile",
                table: "Pizza",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Pizza");

            migrationBuilder.AddColumn<string>(
                name: "PizzaImg",
                table: "Pizza",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
