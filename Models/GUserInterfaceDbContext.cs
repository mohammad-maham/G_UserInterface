using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace G_UserInterface.Models;

public partial class GUserInterfaceDbContext : DbContext
{
    public GUserInterfaceDbContext()
    {
    }

    public GUserInterfaceDbContext(DbContextOptions<GUserInterfaceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<BannerLocation> BannerLocations { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=185.173.104.253:4523;Database=G_UserInterface_DB;Username=postgres;Password=T@l@7est;SearchPath=public", x => x.UseNodaTime());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Baner_pkey");

            entity.ToTable("Banner");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImagePath).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<BannerLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BannerLocation_pkey");

            entity.ToTable("BannerLocation");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Size).HasColumnType("json");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Service_pkey");

            entity.ToTable("Service");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccessInfo).HasColumnType("json");
            entity.Property(e => e.Caption).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Setting_pkey");

            entity.ToTable("Setting");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Caption).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Nsme).HasMaxLength(100);
            entity.Property(e => e.Value).HasMaxLength(200);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Status_pkey");

            entity.ToTable("Status");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Caption).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });
        modelBuilder.HasSequence("BannerSeq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
