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

    public virtual DbSet<Gymnast> Gymnasts { get; set; }

    public virtual DbSet<GymnastClass> GymnastClasses { get; set; }

    public virtual DbSet<StudioClass> StudioClasses { get; set; }

    public virtual DbSet<GlobalStudioClasses> GlobalStudioClasses { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\יודלוב תמר\\source\\repos\\GymProject\\MVCProject\\DAL\\data\\GymDB.mdf;Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gymnast>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gymnast__3214EC07D3A06C40");

            entity.ToTable("Gymnast");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsFixedLength();
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Level)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.MedicalInsurance)
                .HasMaxLength(50);
            entity.Property(e => e.MemberShipType)
                .HasMaxLength(50);
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50);
            entity.Property(e => e.Email)
                    .HasMaxLength(50);
            entity.Property(e => e.Cell)
                .HasMaxLength(10);

            entity.Property(e => e.StudioClasses)
                .HasMaxLength(50);
        });

        modelBuilder.Entity<GymnastClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GymnastClasses");

            entity.Property(e => e.GymnastId)
                .HasMaxLength(9)
                .IsFixedLength();

            entity.HasOne(d => d.Class).WithMany()
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GymnastClasses_Class");

            entity.HasOne(d => d.Gymnast).WithMany()
                .HasForeignKey(d => d.GymnastId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GymnastClasses_Gymnast");
        });

        modelBuilder.Entity<StudioClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudioCl__3214EC0788DA0A64");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.GlobalId).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Level)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.CurrentNum).HasDefaultValue(0);

            entity.HasOne(d => d.GlobalStudioClass)
                .WithMany()
                .HasForeignKey(d => d.GlobalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudioClasses_GlobalStudioClasses");
        });

        modelBuilder.Entity<GlobalStudioClasses>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GlobalStudioClasses");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50);
            entity.Property(e => e.Price).IsRequired();
            entity.Property(e => e.MaxParticipantsNumber).IsRequired();

            entity.HasOne(d => d.Trainer)
                .WithMany()
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GlobalStudioClasses_Trainer");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trainer__3214EC078B74BF99");

            entity.ToTable("Trainer");

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsFixedLength();
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(50);
            entity.Property(e => e.Cell)
                .HasMaxLength(10);
            entity.Property(e => e.Level)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.Specialization)
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}