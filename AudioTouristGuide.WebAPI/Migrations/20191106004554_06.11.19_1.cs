using Microsoft.EntityFrameworkCore.Migrations;

namespace AudioTouristGuide.WebAPI.Migrations
{
    public partial class _061119_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_ImageAssets_LogoImageImageAssetId",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "LogoImageImageAssetId",
                table: "Tours",
                newName: "CoverImageImageAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_LogoImageImageAssetId",
                table: "Tours",
                newName: "IX_Tours_CoverImageImageAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_ImageAssets_CoverImageImageAssetId",
                table: "Tours",
                column: "CoverImageImageAssetId",
                principalTable: "ImageAssets",
                principalColumn: "ImageAssetId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_ImageAssets_CoverImageImageAssetId",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "CoverImageImageAssetId",
                table: "Tours",
                newName: "LogoImageImageAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Tours_CoverImageImageAssetId",
                table: "Tours",
                newName: "IX_Tours_LogoImageImageAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_ImageAssets_LogoImageImageAssetId",
                table: "Tours",
                column: "LogoImageImageAssetId",
                principalTable: "ImageAssets",
                principalColumn: "ImageAssetId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
