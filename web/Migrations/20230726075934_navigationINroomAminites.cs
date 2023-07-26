using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    /// <inheritdoc />
    public partial class navigationINroomAminites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AmeRoomAmenitiesnities_AmenityId",
                table: "AmeRoomAmenitiesnities",
                column: "AmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AmeRoomAmenitiesnities_Amenities_AmenityId",
                table: "AmeRoomAmenitiesnities",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AmeRoomAmenitiesnities_Rooms_RoomId",
                table: "AmeRoomAmenitiesnities",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AmeRoomAmenitiesnities_Amenities_AmenityId",
                table: "AmeRoomAmenitiesnities");

            migrationBuilder.DropForeignKey(
                name: "FK_AmeRoomAmenitiesnities_Rooms_RoomId",
                table: "AmeRoomAmenitiesnities");

            migrationBuilder.DropIndex(
                name: "IX_AmeRoomAmenitiesnities_AmenityId",
                table: "AmeRoomAmenitiesnities");
        }
    }
}
