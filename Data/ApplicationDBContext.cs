namespace API.Data;

using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDBContext : IdentityDbContext<AppUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Portfolio>()
            .HasKey(p => new { p.AppUserId, p.StockId });

        builder.Entity<Portfolio>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.AppUserId);

        builder.Entity<Portfolio>()
            .HasOne(u => u.Stock)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.StockId);

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = "d3b2f3e5-5d3d-4e3b-9f3d-3b3d3b3d3b01",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "a1b2c3d4-e5f6-7890-1234-567890abcdef" // اضافه شد
            },
            new IdentityRole
            {
                Id = "d3b2f3e5-5d3d-4e3b-9f3d-3b3d3b3d3b02",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "b1c2d3e4-f5a6-7890-1234-567890abcdef" // اضافه شد
            },
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}