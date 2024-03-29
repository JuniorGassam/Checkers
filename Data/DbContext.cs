using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetJeuDames.Players;

public class MyDbContext : DbContext
{
    public DbSet<Player> Players { get; set; }
 
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=C:\\Users\\user\\Desktop\\ProjetJeuDames\\DBJeuDames.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlayerConfiguration());
        modelBuilder.Entity<Player>().HasKey(p => p.PlayerId);
    }
}


public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.Property(p => p.Type)
               .HasConversion<string>(); // Convertit l'énumération en chaîne de caractères pour le stockage
    }
}