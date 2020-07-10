using Microsoft.EntityFrameworkCore.Migrations;

namespace Bar.Database.Migrations
{
    public partial class refresh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Order",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "LastChangeMadeById",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DodatniOpis",
                table: "ItemOrder",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Item",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "ReferringToId",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_LastChangeMadeById",
                table: "Order",
                column: "LastChangeMadeById");

            migrationBuilder.CreateIndex(
                name: "IX_Order_LocationId",
                table: "Order",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ReferringToId",
                table: "Item",
                column: "ReferringToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Item_ReferringToId",
                table: "Item",
                column: "ReferringToId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_LastChangeMadeById",
                table: "Order",
                column: "LastChangeMadeById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Location_LocationId",
                table: "Order",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Item_ReferringToId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_LastChangeMadeById",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Location_LocationId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Order_LastChangeMadeById",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_LocationId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Item_ReferringToId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "LastChangeMadeById",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DodatniOpis",
                table: "ItemOrder");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ReferringToId",
                table: "Item");
        }
    }
}
