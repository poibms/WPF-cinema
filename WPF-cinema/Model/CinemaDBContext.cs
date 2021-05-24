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
        public virtual DbSet<Session> Sessions { get; set; }
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
                    .HasName("PK__Films__DC901811B1A6B7C2");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

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
                    .HasName("PK__Halls__771AFBE376196EA5");

                entity.Property(e => e.HallsName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<OrderTicket>(entity =>
            {
                entity.HasOne(d => d.Tickets)
                    .WithMany(p => p.OrderTickets)
                    .HasForeignKey(d => d.TicketsId)
                    .HasConstraintName("FK__OrderTick__Ticke__32E0915F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderTickets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__OrderTick__UserI__31EC6D26");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.HasOne(d => d.Films)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.FilmsId)
                    .HasConstraintName("FK__Session__FilmsId__29572725");

                entity.HasOne(d => d.Halls)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.HallsId)
                    .HasConstraintName("FK__Session__HallsId__286302EC");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicketsId)
                    .HasName("PK__Tickets__EE5BBABB805E573A");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK__Tickets__Session__2C3393D0");
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
