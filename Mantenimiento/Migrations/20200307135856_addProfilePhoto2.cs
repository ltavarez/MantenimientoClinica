using Microsoft.EntityFrameworkCore.Migrations;

namespace Mantenimiento.Migrations
{
    public partial class addProfilePhoto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "Doctor",
                nullable: true);
         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "Doctor");

        }
    }
}
