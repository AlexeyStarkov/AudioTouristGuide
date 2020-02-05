using AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels;
using AudioTouristGuide.WebAPI.Database.Entities.MemberModels;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using Microsoft.EntityFrameworkCore;

namespace AudioTouristGuide.WebAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<MemberDbModel> MemberDbModels { get; set; }

        public DbSet<PlaceDbModel> PlaceDbModels { get; set; }
        public DbSet<TourDbModel> TourDbModels { get; set; }

        public DbSet<AudioAssetDbModel> AudioAssetDbModels { get; set; }
        public DbSet<ImageAssetDbModel> ImageAssetDbModels { get; set; }
        public DbSet<PlaceImageAssetDbModel> PlaceImageAssetDbModels { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourPlaceDbModel>()
                .HasKey(t => new { t.PlaceDbModelId, t.TourDbModelId });

            modelBuilder.Entity<TourPlaceDbModel>()
                .HasOne(tp => tp.PlaceDbModel)
                .WithMany(p => p.TourPlaces)
                .HasForeignKey(tp => tp.PlaceDbModelId);

            modelBuilder.Entity<TourPlaceDbModel>()
                .HasOne(tp => tp.TourDbModel)
                .WithMany(t => t.TourPlaceDbModels)
                .HasForeignKey(tp => tp.TourDbModelId);


            modelBuilder.Entity<MemberPurchasedTourDbModel>()
                .HasKey(mt => new { mt.MemberDbModelId, mt.TourDbModelId });

            modelBuilder.Entity<MemberPurchasedTourDbModel>()
                .HasOne(mt => mt.TourDbModel)
                .WithMany(t => t.MemberPurchasedTourDbModels)
                .HasForeignKey(mt => mt.TourDbModelId);

            modelBuilder.Entity<MemberPurchasedTourDbModel>()
                .HasOne(mt => mt.MemberDbModel)
                .WithMany(t => t.MemberPurchasedTourDbModels)
                .HasForeignKey(mt => mt.MemberDbModelId);


            modelBuilder.Entity<MemberFavoriteTourDbModel>()
                .HasKey(mt => new { mt.MemberDbModelId, mt.TourDbModelId });

            modelBuilder.Entity<MemberFavoriteTourDbModel>()
                .HasOne(mt => mt.TourDbModel)
                .WithMany(t => t.MemberFavoriteTourDbModels)
                .HasForeignKey(mt => mt.TourDbModelId);

            modelBuilder.Entity<MemberFavoriteTourDbModel>()
                .HasOne(mt => mt.MemberDbModel)
                .WithMany(t => t.MemberFavoriteTourDbModels)
                .HasForeignKey(mt => mt.MemberDbModelId);


            modelBuilder.Entity<MemberDesiredTourDbModel>()
                .HasKey(mt => new { mt.MemberDbModelId, mt.TourDbModelId });

            modelBuilder.Entity<MemberDesiredTourDbModel>()
                .HasOne(mt => mt.TourDbModel)
                .WithMany(t => t.MemberDesiredTourDbModels)
                .HasForeignKey(mt => mt.TourDbModelId);

            modelBuilder.Entity<MemberDesiredTourDbModel>()
                .HasOne(mt => mt.MemberDbModel)
                .WithMany(t => t.MemberDesiredTourDbModels)
                .HasForeignKey(mt => mt.MemberDbModelId);
        }
    }
}
