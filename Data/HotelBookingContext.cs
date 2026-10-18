
using HotelBookingManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingManagement.Data
{
    public class HotelBookingContext : DbContext
    {
        public HotelBookingContext(
            DbContextOptions<HotelBookingContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<RoomType> RoomTypes => Set<RoomType>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingDetail> BookingDetails => Set<BookingDetail>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomType)
                .WithMany(t => t.Rooms)
                .HasForeignKey(r => r.RoomTypeId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Creator)
                .WithMany(u => u.CreatedBookings)
                .HasForeignKey(b => b.CreatedBy)
                .IsRequired(false);

            modelBuilder.Entity<BookingDetail>()
                .HasOne(d => d.Booking)
                .WithMany(b => b.BookingDetails)
                .HasForeignKey(d => d.BookingId);

            modelBuilder.Entity<BookingDetail>()
                .HasOne(d => d.Room)
                .WithMany(r => r.BookingDetails)
                .HasForeignKey(d => d.RoomId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BookingId);

            modelBuilder.Entity<RoomType>()
                .Property(r => r.PricePerNight)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<BookingDetail>()
                .Property(d => d.PricePerNight)
                .HasPrecision(18, 2);

            modelBuilder.Entity<BookingDetail>()
                .Property(d => d.Subtotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);
        }
    }
}
