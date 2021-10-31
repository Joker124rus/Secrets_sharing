using Microsoft.EntityFrameworkCore.Migrations;

namespace Secrets_sharing.Migrations
{
    public partial class FileDeleteOnDownload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeleteOnDownload",
                table: "Files",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteOnDownload",
                table: "Files");
        }
    }
}
