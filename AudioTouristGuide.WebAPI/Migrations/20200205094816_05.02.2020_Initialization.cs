using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AudioTouristGuide.WebAPI.Migrations
{
    public partial class _05022020_Initialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageAssets",
                columns: table => new
                {
                    ImageAssetId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AssetContainerName = table.Column<string>(nullable: true),
                    AssetFileName = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageAssets", x => x.ImageAssetId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AvatarImageUrl = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    MobilePhoneNumber = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    BonusPoints = table.Column<double>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: true),
                    CereationTimeStamp = table.Column<DateTime>(nullable: false),
                    IsEmailValidated = table.Column<bool>(nullable: false),
                    IsMobilePhoneNumberValidated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssetsContainerName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    DataSize = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceId);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EstimatedDuration = table.Column<TimeSpan>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    Settlement = table.Column<string>(nullable: true),
                    DataSize = table.Column<long>(nullable: false),
                    GrossPrice = table.Column<decimal>(nullable: true),
                    LogoImageImageAssetId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.TourId);
                    table.ForeignKey(
                        name: "FK_Tours_ImageAssets_LogoImageImageAssetId",
                        column: x => x.LogoImageImageAssetId,
                        principalTable: "ImageAssets",
                        principalColumn: "ImageAssetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AudioAssets",
                columns: table => new
                {
                    AudioAssetId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AssetContainerName = table.Column<string>(nullable: true),
                    AssetFileName = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    PlaceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioAssets", x => x.AudioAssetId);
                    table.ForeignKey(
                        name: "FK_AudioAssets_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaceImageAssets",
                columns: table => new
                {
                    PlaceImageAssetId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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

            migrationBuilder.CreateTable(
                name: "MemberDesiredTour",
                columns: table => new
                {
                    MemberId = table.Column<long>(nullable: false),
                    TourId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberDesiredTour", x => new { x.MemberId, x.TourId });
                    table.ForeignKey(
                        name: "FK_MemberDesiredTour_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberDesiredTour_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberFavoriteTour",
                columns: table => new
                {
                    MemberId = table.Column<long>(nullable: false),
                    TourId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberFavoriteTour", x => new { x.MemberId, x.TourId });
                    table.ForeignKey(
                        name: "FK_MemberFavoriteTour_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberFavoriteTour_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberPurchasedTour",
                columns: table => new
                {
                    MemberId = table.Column<long>(nullable: false),
                    TourId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberPurchasedTour", x => new { x.MemberId, x.TourId });
                    table.ForeignKey(
                        name: "FK_MemberPurchasedTour_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberPurchasedTour_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourPlace",
                columns: table => new
                {
                    PlaceId = table.Column<long>(nullable: false),
                    TourId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPlace", x => new { x.PlaceId, x.TourId });
                    table.ForeignKey(
                        name: "FK_TourPlace_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourPlace_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioAssets_PlaceId",
                table: "AudioAssets",
                column: "PlaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberDesiredTour_TourId",
                table: "MemberDesiredTour",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberFavoriteTour_TourId",
                table: "MemberFavoriteTour",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberPurchasedTour_TourId",
                table: "MemberPurchasedTour",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaceImageAssets_PlaceId",
                table: "PlaceImageAssets",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPlace_TourId",
                table: "TourPlace",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_LogoImageImageAssetId",
                table: "Tours",
                column: "LogoImageImageAssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioAssets");

            migrationBuilder.DropTable(
                name: "MemberDesiredTour");

            migrationBuilder.DropTable(
                name: "MemberFavoriteTour");

            migrationBuilder.DropTable(
                name: "MemberPurchasedTour");

            migrationBuilder.DropTable(
                name: "PlaceImageAssets");

            migrationBuilder.DropTable(
                name: "TourPlace");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "ImageAssets");
        }
    }
}
