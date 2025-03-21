using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventArtistRelation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ArtistEvent",
                columns: table => new
                {
                    ArtistsId = table.Column<int>(type: "int", nullable: false),
                    EventsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistEvent", x => new { x.ArtistsId, x.EventsId });
                    table.ForeignKey(
                        name: "FK_ArtistEvent_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistEvent_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistEvent_EventsId",
                table: "ArtistEvent",
                column: "EventsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistEvent");

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
    }
}
