﻿// <auto-generated />
using System;
using AudioTouristGuide.WebAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AudioTouristGuide.WebAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200205094816_05.02.2020_Initialization")]
    partial class _05022020_Initialization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberDesiredTour", b =>
                {
                    b.Property<long>("MemberId")
                        .HasColumnType("bigint");

                    b.Property<long>("TourId")
                        .HasColumnType("bigint");

                    b.HasKey("MemberId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("MemberDesiredTour");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberFavoriteTour", b =>
                {
                    b.Property<long>("MemberId")
                        .HasColumnType("bigint");

                    b.Property<long>("TourId")
                        .HasColumnType("bigint");

                    b.HasKey("MemberId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("MemberFavoriteTour");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberPurchasedTour", b =>
                {
                    b.Property<long>("MemberId")
                        .HasColumnType("bigint");

                    b.Property<long>("TourId")
                        .HasColumnType("bigint");

                    b.HasKey("MemberId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("MemberPurchasedTour");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.TourPlace", b =>
                {
                    b.Property<long>("PlaceId")
                        .HasColumnType("bigint");

                    b.Property<long>("TourId")
                        .HasColumnType("bigint");

                    b.HasKey("PlaceId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("TourPlace");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.MemberModels.Member", b =>
                {
                    b.Property<long>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("AvatarImageUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("BonusPoints")
                        .HasColumnType("double");

                    b.Property<DateTime>("CereationTimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsEmailValidated")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsMobilePhoneNumberValidated")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("MobilePhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PostCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.HasKey("MemberId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.AudioAsset", b =>
                {
                    b.Property<long>("AudioAssetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AssetContainerName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("AssetFileName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("PlaceId")
                        .HasColumnType("bigint");

                    b.HasKey("AudioAssetId");

                    b.HasIndex("PlaceId")
                        .IsUnique();

                    b.ToTable("AudioAssets");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.ImageAsset", b =>
                {
                    b.Property<long>("ImageAssetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AssetContainerName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("AssetFileName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ImageAssetId");

                    b.ToTable("ImageAssets");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Place", b =>
                {
                    b.Property<long>("PlaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AssetsContainerName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("DataSize")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("PlaceId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.PlaceImageAsset", b =>
                {
                    b.Property<long>("PlaceImageAssetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AssetContainerName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("AssetFileName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("PlaceId")
                        .HasColumnType("bigint");

                    b.Property<TimeSpan>("PointOfDisplayingStart")
                        .HasColumnType("time(6)");

                    b.HasKey("PlaceImageAssetId");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlaceImageAssets");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", b =>
                {
                    b.Property<long>("TourId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CountryName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("DataSize")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<TimeSpan>("EstimatedDuration")
                        .HasColumnType("time(6)");

                    b.Property<decimal?>("GrossPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<long?>("LogoImageImageAssetId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Settlement")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("TourId");

                    b.HasIndex("LogoImageImageAssetId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberDesiredTour", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.MemberModels.Member", "Member")
                        .WithMany("MemberDesiredTour")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", "Tour")
                        .WithMany("MemberDesiredTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberFavoriteTour", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.MemberModels.Member", "Member")
                        .WithMany("MemberFavoriteTours")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", "Tour")
                        .WithMany("MemberFavoriteTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberPurchasedTour", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.MemberModels.Member", "Member")
                        .WithMany("MemberPurchasedTours")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", "Tour")
                        .WithMany("MemberPurchasedTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.TourPlace", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Place", "Place")
                        .WithMany("TourPlaces")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", "Tour")
                        .WithMany("TourPlaces")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.AudioAsset", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Place", "Place")
                        .WithOne("AudioAsset")
                        .HasForeignKey("AudioTouristGuide.WebAPI.Database.Entities.TourModels.AudioAsset", "PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.PlaceImageAsset", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Place", "Place")
                        .WithMany("PlaceImageAssets")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.ImageAsset", "LogoImage")
                        .WithMany()
                        .HasForeignKey("LogoImageImageAssetId");
                });
#pragma warning restore 612, 618
        }
    }
}