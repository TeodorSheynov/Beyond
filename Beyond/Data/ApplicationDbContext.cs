using Beyond.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Beyond.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(
                typeBuilder =>
                {
                    typeBuilder.HasMany(host => host.Tickets)
                        .WithOne(guest => guest.User)
                        .HasForeignKey(guest => guest.UserId);

                });
            builder.Entity<Ticket>(
                typeBuilder =>
                {
                    typeBuilder.HasOne(guest => guest.User)
                        .WithMany(host => host.Tickets)
                        .HasForeignKey(guest => guest.UserId);
                });
        }
    }
}
