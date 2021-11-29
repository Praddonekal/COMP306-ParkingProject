using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Comp306Project.Models
{
    public partial class ParkAPIContext : DbContext
    {
        public ParkAPIContext()
        {
            Database.EnsureCreated();
        }

        public ParkAPIContext(DbContextOptions<ParkAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ParkingLots> ParkingLots { get; set; }
        public virtual DbSet<Spots> Spots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=parking.c2yicpzrj1p1.us-east-1.rds.amazonaws.com;Database=sampletest;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingLots>(entity =>
            {
                entity.HasKey(e => e.LotId);

                entity.Property(e => e.LotId)
                    .HasColumnName("LotID")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.LotCity)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LotDailyRate).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.LotHourlyRate).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.LotMonthlyRate).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.LotName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LotStreetName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LotWeeklyRate).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.LotYearlyRate).HasColumnType("decimal(9, 2)");
            });

            modelBuilder.Entity<Spots>(entity =>
            {
                entity.HasKey(e => e.SpotId);

                entity.Property(e => e.SpotId)
                    .HasColumnName("SpotID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LotId)
                    .IsRequired()
                    .HasColumnName("LotID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SpotAvailable)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SpotReserved)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.Lot)
                    .WithMany(p => p.Spots)
                    .HasForeignKey(d => d.LotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spots_ParkingLots");
            });
        }
    }
}
