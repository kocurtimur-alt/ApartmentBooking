using ApartmentBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentBooking.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Apartment> Apartments => Set<Apartment>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>()
            .HasOne(a => a.Owner)
            .WithMany(u => u.Apartments)
            .HasForeignKey(a => a.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Tenant)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Apartment)
            .WithMany(a => a.Bookings)
            .HasForeignKey(b => b.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}