using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace ShelterApi.Models
{
    public partial class AnimalShelterContext : DbContext
    {
        public AnimalShelterContext()
        {
        }
        public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options): base(options)
        {
        }

        public virtual DbSet<Animals> Animals { get; set; }

        public virtual DbSet<Shelters> Shelters { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animals>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("animals");

                entity.Property(e => e.Name).HasMaxLength(40);
                entity.Property(e => e.Species).HasMaxLength(30);
                entity.Property(e => e.Age).HasColumnType("int");
                entity.Property(e => e.Weight).HasColumnType("double");
                entity.Property(e => e.RegTime)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp");
                entity.Property(e => e.ShelterId).HasMaxLength(36);
            });

            modelBuilder.Entity<Shelters>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("shelters");

                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.City).HasMaxLength(40);
                entity.Property(e => e.Capacity).HasColumnType("int");
                entity.Property(e => e.RegTime)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
