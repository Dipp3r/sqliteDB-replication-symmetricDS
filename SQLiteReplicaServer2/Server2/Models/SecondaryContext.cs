using Microsoft.EntityFrameworkCore;

namespace SQLiteReplicaServer2.Server2.Models;

public partial class SecondaryContext : DbContext
{
    public SecondaryContext()
    {
    }

    public SecondaryContext(DbContextOptions<SecondaryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alarm> Alarms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\INARK12\\OneDrive - ABB\\Documents\\SQLiteReplicatonDemo\\SQLiteReplicaServer2\\Server2\\Database\\secondary.sqlite");
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
