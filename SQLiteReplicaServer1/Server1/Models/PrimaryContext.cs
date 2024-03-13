using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SQLiteReplica.Server1.Models;

public partial class PrimaryContext : DbContext
{
    public PrimaryContext()
    {
    }

    public PrimaryContext(DbContextOptions<PrimaryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alarm> Alarms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=.\\Server1\\Database\\primary.sqlite");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alarm>(entity =>
        {
            entity.ToTable("alarms");

            entity.Property(e => e.AlarmId)
                .ValueGeneratedNever()
                .HasColumnName("alarm_id");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
