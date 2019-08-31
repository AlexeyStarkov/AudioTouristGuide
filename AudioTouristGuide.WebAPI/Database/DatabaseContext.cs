using AudioTouristGuide.WebAPI.Database.Entities.JoinTablesModels;
using AudioTouristGuide.WebAPI.Database.Entities.MemberModels;
using AudioTouristGuide.WebAPI.Database.Entities.TourModels;
using Microsoft.EntityFrameworkCore;

namespace AudioTouristGuide.WebAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Member> Members { get; set; }

        public DbSet<Place> Places { get; set; }
        public DbSet<Tour> Tours { get; set; }

        public DbSet<AudioAsset> AudioAssets { get; set; }
        public DbSet<ImageAsset> ImageAssets { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TourPlace>()
                .HasKey(t => new { t.PlaceId, t.TourId });

            modelBuilder.Entity<TourPlace>()
                .HasOne(tp => tp.Place)
                .WithMany(p => p.TourPlaces)
                .HasForeignKey(tp => tp.PlaceId);

            modelBuilder.Entity<TourPlace>()
                .HasOne(tp => tp.Tour)
                .WithMany(t => t.TourPlaces)
                .HasForeignKey(tp => tp.TourId);


            modelBuilder.Entity<MemberPurchasedTour>()
                .HasKey(mt => new { mt.MemberId, mt.TourId });

            modelBuilder.Entity<MemberPurchasedTour>()
                .HasOne(mt => mt.Tour)
                .WithMany(t => t.MemberPurchasedTours)
                .HasForeignKey(mt => mt.TourId);

            modelBuilder.Entity<MemberPurchasedTour>()
                .HasOne(mt => mt.Member)
                .WithMany(t => t.MemberPurchasedTours)
                .HasForeignKey(mt => mt.MemberId);


            modelBuilder.Entity<MemberFavoriteTour>()
                .HasKey(mt => new { mt.MemberId, mt.TourId });

            modelBuilder.Entity<MemberFavoriteTour>()
                .HasOne(mt => mt.Tour)
                .WithMany(t => t.MemberFavoriteTours)
                .HasForeignKey(mt => mt.TourId);

            modelBuilder.Entity<MemberFavoriteTour>()
                .HasOne(mt => mt.Member)
                .WithMany(t => t.MemberFavoriteTours)
                .HasForeignKey(mt => mt.MemberId);


            modelBuilder.Entity<MemberDesiredTour>()
                .HasKey(mt => new { mt.MemberId, mt.TourId });

            modelBuilder.Entity<MemberDesiredTour>()
                .HasOne(mt => mt.Tour)
                .WithMany(t => t.MemberDesiredTours)
                .HasForeignKey(mt => mt.TourId);

            modelBuilder.Entity<MemberDesiredTour>()
                .HasOne(mt => mt.Member)
                .WithMany(t => t.MemberDesiredTour)
                .HasForeignKey(mt => mt.MemberId);
        }
    }
}
