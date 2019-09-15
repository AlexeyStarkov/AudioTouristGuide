using Microsoft.EntityFrameworkCore.Migrations;

namespace AudioTouristGuide.WebAPI.Migrations
{
    public partial class _150919 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                table: "Tours",
                newName: "LogoFileName");

            migrationBuilder.RenameColumn(
                name: "AssetsFolderGuid",
                table: "Tours",
                newName: "AssetsContainerName");

            migrationBuilder.RenameColumn(
                name: "AssetsFolderGuid",
                table: "Places",
                newName: "AssetsContainerName");

            migrationBuilder.RenameColumn(
                name: "AssetFileUrl",
                table: "ImageAssets",
                newName: "AssetFileName");

            migrationBuilder.RenameColumn(
                name: "AssetFileUrl",
                table: "AudioAssets",
                newName: "AssetFileName");

            migrationBuilder.AddColumn<string>(
                name: "AssetContainerName",
                table: "ImageAssets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetContainerName",
                table: "AudioAssets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetContainerName",
                table: "ImageAssets");

            migrationBuilder.DropColumn(
                name: "AssetContainerName",
                table: "AudioAssets");

            migrationBuilder.RenameColumn(
                name: "LogoFileName",
                table: "Tours",
                newName: "LogoUrl");

            migrationBuilder.RenameColumn(
                name: "AssetsContainerName",
                table: "Tours",
                newName: "AssetsFolderGuid");

            migrationBuilder.RenameColumn(
                name: "AssetsContainerName",
                table: "Places",
                newName: "AssetsFolderGuid");

            migrationBuilder.RenameColumn(
                name: "AssetFileName",
                table: "ImageAssets",
                newName: "AssetFileUrl");

            migrationBuilder.RenameColumn(
                name: "AssetFileName",
                table: "AudioAssets",
                newName: "AssetFileUrl");
        }
    }
}
