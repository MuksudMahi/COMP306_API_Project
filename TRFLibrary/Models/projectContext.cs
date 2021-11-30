using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TRFLibrary.Models
{
    public partial class projectContext : DbContext
    {
        public projectContext()
        {
        }

        public projectContext(DbContextOptions<projectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food");

                entity.Property(e => e.FoodDescription)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FoodName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FoodPrice).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("fk_res_id");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");

                entity.Property(e => e.RestaurantName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RestaurantType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
