using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Entities;

namespace Repositories;

public partial class KoiCareSystemContext : DbContext
{
    public KoiCareSystemContext()
    {
    }

    public KoiCareSystemContext(DbContextOptions<KoiCareSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ExternalProduct> ExternalProducts { get; set; }

    public virtual DbSet<Koi> Kois { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<Pond> Ponds { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];


        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B6B934F14");

            entity.ToTable("Category");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ExternalProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__External__B40CC6CD9985C326");

            entity.ToTable("ExternalProduct");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ExternalUrl).HasMaxLength(2083);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Category).WithMany(p => p.ExternalProducts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_ExternalProduct_Category");
        });

        modelBuilder.Entity<Koi>(entity =>
        {
            entity.HasKey(e => e.KoiId).HasName("PK__Koi__E0343598BA656F8E");

            entity.ToTable("Koi");

            entity.Property(e => e.Color).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Origin).HasMaxLength(255);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Pond).WithMany(p => p.Kois)
                .HasForeignKey(d => d.PondId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Koi_Pond");

            entity.HasOne(d => d.User).WithMany(p => p.Kois)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Koi_User");
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.MeasureId).HasName("PK__Measurem__8C56D0802313D942");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PH).HasColumnName("pH");
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.Pond).WithMany(p => p.Measurements)
                .HasForeignKey(d => d.PondId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Measurements_PondId");

            entity.HasOne(d => d.User).WithMany(p => p.Measurements)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Measurements_UserId");
        });

        modelBuilder.Entity<Pond>(entity =>
        {
            entity.HasKey(e => e.PondId).HasName("PK__Pond__D18BF8343EE63F57");

            entity.ToTable("Pond");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Thumbnail).IsUnicode(false);
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.Ponds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pond_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CF2F94BED");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D10534A0535E77").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
