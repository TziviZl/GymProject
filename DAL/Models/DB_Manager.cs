using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class DB_Manager : DbContext
{
    public DB_Manager()
    {
    }

    public DB_Manager(DbContextOptions<DB_Manager> options)
        : base(options)
    {
    }

    public virtual DbSet<BackupTrainer> BackupTrainers { get; set; }

    public virtual DbSet<GlobalStudioClass> GlobalStudioClasses { get; set; }

    public virtual DbSet<Gymnast> Gymnasts { get; set; }

    public virtual DbSet<GymnastClass> GymnastClasses { get; set; }

    public virtual DbSet<StudioClass> StudioClasses { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\USER\\source\\repos\\GymProject\\DAL\\data\\GymDB.mdf;Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BackupTrainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BackupTr__3214EC07C19C0F38");
        });

        modelBuilder.Entity<GlobalStudioClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GlobalSt__3214EC07AB0D3B0B");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.TrainerId).IsFixedLength();

            entity.HasOne(d => d.Trainer).WithMany(p => p.GlobalStudioClasses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GlobalStudioClasses_Trainer");
        });

        modelBuilder.Entity<Gymnast>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gymnast__3214EC07D3A06C40");

            entity.Property(e => e.Id).IsFixedLength();
            entity.Property(e => e.Cell).IsFixedLength();
            entity.Property(e => e.FirstName).IsFixedLength();
            entity.Property(e => e.LastName).IsFixedLength();
            entity.Property(e => e.Level).IsFixedLength();
            entity.Property(e => e.WeeklyCounter).HasDefaultValue(2);
        });

        modelBuilder.Entity<GymnastClass>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.GymnastId).IsFixedLength();

            entity.HasOne(d => d.Class).WithMany(p => p.GymnastClasses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GymnastClasses_Class");

            entity.HasOne(d => d.Gymnast).WithMany(p => p.GymnastClasses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GymnastClasses_Gymnast");
        });

        modelBuilder.Entity<StudioClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC070865CCD0");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CurrentNum).HasDefaultValue(20);
            entity.Property(e => e.Level).IsFixedLength();

            entity.HasOne(d => d.Global).WithMany(p => p.StudioClasses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudioClasses_GlobalStudioClasses");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trainer__3214EC07AF4B93BC");

            entity.Property(e => e.Id).IsFixedLength();
            entity.Property(e => e.Cell).IsFixedLength();
            entity.Property(e => e.FirstName).IsFixedLength();
            entity.Property(e => e.LastName).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
