using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.Core.Persistence.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LegendsTeamVN.BadmintonClub.Persistence;

public class BadmintonDbContext(DbContextOptions<BadmintonDbContext> options, IPublisher publisher) : DbContextUnitOfWork<BadmintonDbContext>(options, publisher)
{
    public DbSet<Venue> Venues => Set<Venue>();
    public DbSet<VenueStaff> VenueStaffs => Set<VenueStaff>();
    public DbSet<VenueSchedule> VenueSchedules => Set<VenueSchedule>();
    public DbSet<VenueImage> VenueImages => Set<VenueImage>();
    public DbSet<SportType> SportTypes => Set<SportType>();
    public DbSet<VenueSportType> VenueSportTypes => Set<VenueSportType>();
    public DbSet<Court> Courts => Set<Court>();
    public DbSet<CourtPricing> CourtPricings => Set<CourtPricing>();
    public DbSet<CourtMaintenance> CourtMaintenances => Set<CourtMaintenance>();
    public DbSet<RentalItem> RentalItems => Set<RentalItem>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Favourite> Favourites => Set<Favourite>();
    public DbSet<Voucher> Vouchers => Set<Voucher>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingDetail> BookingDetails => Set<BookingDetail>();
    public DbSet<RentalDetail> RentalDetails => Set<RentalDetail>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<BookingCancellation> BookingCancellations => Set<BookingCancellation>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<MatchPlayer> MatchPlayers => Set<MatchPlayer>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BadmintonDbContext).Assembly);
    }
}
