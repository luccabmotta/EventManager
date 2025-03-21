using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventArtistRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistIds",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Artists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_EventId",
                table: "Artists",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Events_EventId",
                table: "Artists",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Events_EventId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_EventId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Artists");

            migrationBuilder.AddColumn<string>(
                name: "ArtistIds",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
