using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimeHeaven.Data.Migrations
{
    public partial class AddedUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
