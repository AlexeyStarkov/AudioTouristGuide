using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AudioTouristGuide.WebAPI.Migrations
{
    public partial class _071119_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageAssets_Places_PlaceId",
                table: "ImageAssets");

            migrationBuilder.DropIndex(
                name: "IX_ImageAssets_PlaceId",
                table: "ImageAssets");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ImageAssets");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "ImageAssets");

            migrationBuilder.DropColumn(
                name: "PointOfDisplayingStart",
                table: "ImageAssets");

            migrationBuilder.CreateTable(
                name: "PlaceImageAssets",
                columns: table => new
                {
                    PlaceImageAssetId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AssetContainerName = table.Column<string>(nullable: true),
                    AssetFileName = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    PointOfDisplayingStart = table.Column<TimeSpan>(nullable: false),
                    PlaceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceImageAssets", x => x.PlaceImageAssetId);
                    table.ForeignKey(
                        name: "FK_PlaceImageAssets_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceImageAssets_PlaceId",
                table: "PlaceImageAssets",
                column: "PlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceImageAssets");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ImageAssets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PlaceId",
                table: "ImageAssets",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PointOfDisplayingStart",
                table: "ImageAssets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageAssets_PlaceId",
                table: "ImageAssets",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageAssets_Places_PlaceId",
                table: "ImageAssets",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "PlaceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
