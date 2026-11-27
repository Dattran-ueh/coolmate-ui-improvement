using DatPaper.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatPaper.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<HoaDon> HoaDons { get; set; }

    public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    public DbSet<ShippingDetail> ShippingDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ChiTietHoaDon>()
            .HasOne(c => c.ProductObj)
            .WithMany()
            .HasForeignKey(c => c.MaProduct)
            .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<ShippingDetail>()
    .HasOne(s => s.HoaDonObj)
    .WithOne()
    .HasForeignKey<ShippingDetail>(s => s.MaHoaDon)
    .OnDelete(DeleteBehavior.Restrict);
    }
}