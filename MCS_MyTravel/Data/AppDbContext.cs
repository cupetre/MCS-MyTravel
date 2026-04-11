using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;
using Microsoft.EntityFrameworkCore;

namespace MCS_MyTravel.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Document> Documents => Set<Document>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clients");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Phone).HasMaxLength(50);
                entity.Property(x => x.PassportId).HasMaxLength(100);
                entity.Property(x => x.Notes).HasMaxLength(2000);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Bookings");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Destination).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Notes).HasMaxLength(2000);

                entity.Property(x => x.HotelPrice).HasColumnType("numeric(12,2)");
                entity.Property(x => x.TravelPrice).HasColumnType("numeric(12,2)");
                entity.Property(x => x.InsurancePrice).HasColumnType("numeric(12,2)");
                entity.Property(x => x.TaxesPrice).HasColumnType("numeric(12,2)");

                entity.HasOne(x => x.Client)
                      .WithMany(x => x.Bookings)
                      .HasForeignKey(x => x.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.ToTable("Passengers");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                entity.Property(x => x.PassportId).HasMaxLength(100);

                entity.HasOne(x => x.Booking)
                      .WithMany(x => x.Passengers)
                      .HasForeignKey(x => x.BookingId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Amount).HasColumnType("numeric(12,2)");
                entity.Property(x => x.Notes).HasMaxLength(2000);

                entity.HasOne(x => x.Booking)
                      .WithMany(x => x.Payments)
                      .HasForeignKey(x => x.BookingId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Documents");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.DocumentNumber).HasMaxLength(100);
                entity.Property(x => x.TotalPrice).HasColumnType("numeric(12,2)");
                entity.Property(x => x.Notes).HasMaxLength(2000);

                entity.HasOne(x => x.Booking)
                      .WithMany(x => x.Documents)
                      .HasForeignKey(x => x.BookingId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
