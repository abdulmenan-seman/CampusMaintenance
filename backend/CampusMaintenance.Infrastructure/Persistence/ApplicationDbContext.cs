using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CampusMaintenance.Domain.Entities;
using CampusMaintenance.Infrastructure.Identity;

namespace CampusMaintenance.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Campus> Campuses => Set<Campus>();
    public DbSet<Building> Buildings => Set<Building>();
    public DbSet<TechnicianProfile> TechnicianProfiles => Set<TechnicianProfile>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketAssignment> TicketAssignments => Set<TicketAssignment>();
    public DbSet<TicketHistory> TicketHistories => Set<TicketHistory>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Configures Identity tables (AspNetUsers, AspNetRoles, etc.)

        // Map ApplicationUser to table and configure relationships
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users"); // Optional: clean table name instead of AspNetUsers
            
            entity.HasOne<Campus>()
                  .WithMany()
                  .HasForeignKey(u => u.CampusId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // TechnicianProfile configuration (1-to-1 with ApplicationUser)
        builder.Entity<TechnicianProfile>(entity =>
        {
            entity.HasKey(tp => tp.Id);
            entity.HasOne<ApplicationUser>()
                  .WithOne()
                  .HasForeignKey<TechnicianProfile>(tp => tp.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Campus & Building Configuration
        builder.Entity<Campus>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(150);
        });

        builder.Entity<Building>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.HasOne(b => b.Campus)
                  .WithMany(c => c.Buildings)
                  .HasForeignKey(b => b.CampusId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Ticket Configuration
        builder.Entity<Ticket>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasOne(t => t.Campus)
                  .WithMany(c => c.Tickets)
                  .HasForeignKey(t => t.CampusId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Building)
                  .WithMany(b => b.Tickets)
                  .HasForeignKey(t => t.BuildingId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<ApplicationUser>()
                  .WithMany()
                  .HasForeignKey(t => t.ReporterId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // TicketAssignment Configuration
        builder.Entity<TicketAssignment>(entity =>
        {
            entity.HasKey(ta => ta.Id);
            entity.HasOne(ta => ta.Ticket)
                  .WithMany(t => t.Assignments)
                  .HasForeignKey(ta => ta.TicketId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ta => ta.Technician)
                  .WithMany(tp => tp.Assignments)
                  .HasForeignKey(ta => ta.TechnicianId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<ApplicationUser>()
                  .WithMany()
                  .HasForeignKey(ta => ta.AssignedById)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // TicketHistory Configuration
        builder.Entity<TicketHistory>(entity =>
        {
            entity.HasKey(th => th.Id);
            entity.HasOne(th => th.Ticket)
                  .WithMany(t => t.Histories)
                  .HasForeignKey(th => th.TicketId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<ApplicationUser>()
                  .WithMany()
                  .HasForeignKey(th => th.ActorId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<RefreshToken>(entity =>
{
    entity.HasKey(rt => rt.Id);
      entity.HasOne<ApplicationUser>()
          .WithMany()
          .HasForeignKey(rt => rt.UserId)
          .OnDelete(DeleteBehavior.Cascade);
});
    }
}