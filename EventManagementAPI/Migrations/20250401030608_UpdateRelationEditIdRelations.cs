using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationEditIdRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventIds",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Artists");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "EventIds",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
