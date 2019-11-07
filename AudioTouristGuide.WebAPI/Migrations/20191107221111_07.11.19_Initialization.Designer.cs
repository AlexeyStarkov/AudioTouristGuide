﻿// <auto-generated />
using System;
using AudioTouristGuide.WebAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AudioTouristGuide.WebAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20191107221111_07.11.19_Initialization")]
    partial class _071119_Initialization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberDesiredTour", b =>
                {
                    b.Property<long>("MemberId");

                    b.Property<long>("TourId");

                    b.HasKey("MemberId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("MemberDesiredTour");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberFavoriteTour", b =>
                {
                    b.Property<long>("MemberId");

                    b.Property<long>("TourId");

                    b.HasKey("MemberId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("MemberFavoriteTour");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberPurchasedTour", b =>
                {
                    b.Property<long>("MemberId");

                    b.Property<long>("TourId");

                    b.HasKey("MemberId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("MemberPurchasedTour");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.TourPlace", b =>
                {
                    b.Property<long>("PlaceId");

                    b.Property<long>("TourId");

                    b.HasKey("PlaceId", "TourId");

                    b.HasIndex("TourId");

                    b.ToTable("TourPlace");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.MemberModels.Member", b =>
                {
                    b.Property<long>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("AvatarImageUrl");

                    b.Property<DateTime?>("Birthday");

                    b.Property<double>("BonusPoints");

                    b.Property<DateTime>("CereationTimeStamp");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsEmailValidated");

                    b.Property<bool>("IsMobilePhoneNumberValidated");

                    b.Property<string>("LastName");

                    b.Property<string>("MobilePhoneNumber");

                    b.Property<string>("PostCode");

                    b.Property<int>("Sex");

                    b.HasKey("MemberId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.AudioAsset", b =>
                {
                    b.Property<long>("AudioAssetId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssetContainerName");

                    b.Property<string>("AssetFileName");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Name");

                    b.Property<long>("PlaceId");

                    b.HasKey("AudioAssetId");

                    b.HasIndex("PlaceId")
                        .IsUnique();

                    b.ToTable("AudioAssets");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.ImageAsset", b =>
                {
                    b.Property<long>("ImageAssetId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssetContainerName");

                    b.Property<string>("AssetFileName");

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Name");

                    b.HasKey("ImageAssetId");

                    b.ToTable("ImageAssets");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ImageAsset");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Place", b =>
                {
                    b.Property<long>("PlaceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssetsContainerName");

                    b.Property<long>("DataSize");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("PlaceId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", b =>
                {
                    b.Property<long>("TourId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryName");

                    b.Property<long>("DataSize");

                    b.Property<string>("Description");

                    b.Property<TimeSpan>("EstimatedDuration");

                    b.Property<decimal?>("GrossPrice");

                    b.Property<long?>("LogoImageImageAssetId");

                    b.Property<string>("Name");

                    b.Property<string>("Settlement");

                    b.HasKey("TourId");

                    b.HasIndex("LogoImageImageAssetId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.PlaceImageAsset", b =>
                {
                    b.HasBaseType("AudioTouristGuide.WebAPI.Database.Entities.TourModels.ImageAsset");

                    b.Property<long>("PlaceId");

                    b.Property<TimeSpan>("PointOfDisplayingStart");

                    b.HasIndex("PlaceId");

                    b.HasDiscriminator().HasValue("PlaceImageAsset");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberDesiredTour", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.MemberModels.Member", "Member")
                        .WithMany("MemberDesiredTour")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", "Tour")
                        .WithMany("MemberDesiredTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberFavoriteTour", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.MemberModels.Member", "Member")
                        .WithMany("MemberFavoriteTours")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", "Tour")
                        .WithMany("MemberFavoriteTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.MemberPurchasedTour", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.MemberModels.Member", "Member")
                        .WithMany("MemberPurchasedTours")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", "Tour")
                        .WithMany("MemberPurchasedTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels.TourPlace", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Place", "Place")
                        .WithMany("TourPlaces")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", "Tour")
                        .WithMany("TourPlaces")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.AudioAsset", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Place", "Place")
                        .WithOne("AudioAsset")
                        .HasForeignKey("AudioTouristGuide.WebAPI.Database.Entities.TourModels.AudioAsset", "PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Tour", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.ImageAsset", "LogoImage")
                        .WithMany()
                        .HasForeignKey("LogoImageImageAssetId");
                });

            modelBuilder.Entity("AudioTouristGuide.WebAPI.Database.Entities.TourModels.PlaceImageAsset", b =>
                {
                    b.HasOne("AudioTouristGuide.WebAPI.Database.Entities.TourModels.Place", "Place")
                        .WithMany("PlaceImageAssets")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}