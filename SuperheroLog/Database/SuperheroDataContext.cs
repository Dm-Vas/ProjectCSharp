using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SuperheroLog.Database
{
    public partial class SuperheroDataContext : DbContext
    {
        public SuperheroDataContext()
        {
        }

        public SuperheroDataContext(DbContextOptions<SuperheroDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Mission> Missions { get; set; }
        public virtual DbSet<Missionstatus> Missionstatuses { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Universe> Universes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database=SuperheroData; trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("CHARACTER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.ImagePath).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.UniverseId).HasColumnName("UniverseID");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHARACTER_TEAM");

                entity.HasOne(d => d.Universe)
                    .WithMany(p => p.Characters)
                    .HasForeignKey(d => d.UniverseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHARACTER_UNIVERSE");
            });

            modelBuilder.Entity<Mission>(entity =>
            {
                entity.ToTable("MISSION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CharacterId).HasColumnName("CharacterID");

                entity.Property(e => e.MissionDescription)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.MissionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.Missions)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MISSION_CHARACTER");

                entity.HasOne(d => d.MissionStatusNavigation)
                    .WithMany(p => p.Missions)
                    .HasForeignKey(d => d.MissionStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MISSION_MISSIONSTATUS");
            });

            modelBuilder.Entity<Missionstatus>(entity =>
            {
                entity.ToTable("MISSIONSTATUS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("TEAM");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UniverseId).HasColumnName("UniverseID");

                entity.HasOne(d => d.Universe)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.UniverseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEAM_UNIVERSE");
            });

            modelBuilder.Entity<Universe>(entity =>
            {
                entity.ToTable("UNIVERSE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UniverseName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
