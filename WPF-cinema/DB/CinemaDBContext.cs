using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WPF_cinema
{
    public partial class CinemaDBContext : DbContext
    {
        public CinemaDBContext()
        {
            Database.EnsureCreated();
        }

        public CinemaDBContext(DbContextOptions<CinemaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<OrderTicket> OrderTickets { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=DESKTOP-LLPVUOF;Database=CinemaDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.FilmsId)
                    .HasName("PK__Films__DC90181180066439");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Director)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FilmsName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.HasKey(e => e.HallsId)
                    .HasName("PK__Halls__771AFBE3A7A884F2");

                entity.Property(e => e.HallsName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<OrderTicket>(entity =>
            {
                entity.HasOne(d => d.Tickets)
                    .WithMany(p => p.OrderTickets)
                    .HasForeignKey(d => d.TicketsId)
                    .HasConstraintName("FK__OrderTick__Ticke__300424B4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderTickets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__OrderTick__UserI__2F10007B");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicketsId)
                    .HasName("PK__Tickets__EE5BBABB75779298");

                entity.HasOne(d => d.Films)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.FilmsId)
                    .HasConstraintName("FK__Tickets__FilmsId__29572725");

                entity.HasOne(d => d.Halls)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.HallsId)
                    .HasConstraintName("FK__Tickets__HallsId__286302EC");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Role).HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
