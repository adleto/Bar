using Microsoft.EntityFrameworkCore.Migrations;

namespace Bar.Database.Migrations
{
    public partial class cijenaForHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PojedinacnaCijena",
                table: "ItemOrder",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PojedinacnaCijena",
                table: "ItemOrder");
        }
    }
}
