using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AudioTouristGuide.WebAPI.Migrations
{
    public partial class _051119_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetsContainerName",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "LogoFileName",
                table: "Tours",
                newName: "Settlement");

            migrationBuilder.AddColumn<long>(
                name: "LogoImageImageAssetId",
                table: "Tours",
                nullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "PointOfDisplayingStart",
                table: "ImageAssets",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "ImageAssets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "AudioAssets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Tours_LogoImageImageAssetId",
                table: "Tours",
                column: "LogoImageImageAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_ImageAssets_LogoImageImageAssetId",
                table: "Tours",
                column: "LogoImageImageAssetId",
                principalTable: "ImageAssets",
                principalColumn: "ImageAssetId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_ImageAssets_LogoImageImageAssetId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_LogoImageImageAssetId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "LogoImageImageAssetId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "ImageAssets");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "AudioAssets");

            migrationBuilder.RenameColumn(
                name: "Settlement",
                table: "Tours",
                newName: "LogoFileName");

            migrationBuilder.AddColumn<string>(
                name: "AssetsContainerName",
                table: "Tours",
                nullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "PointOfDisplayingStart",
                table: "ImageAssets",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);
        }
    }
}
