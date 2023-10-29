using Microsoft.EntityFrameworkCore;
//using MySQL.Data.EntityFrameworkCore;
using WatchCart.Model;

namespace WatchCart.Data.Watchcartchallenge;

public partial class WatchcartchallengeContext : DbContext
{
    public WatchcartchallengeContext()
    {
    }

    public WatchcartchallengeContext(DbContextOptions<WatchcartchallengeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Watch> Watches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("Server=localhost;Database=Watchcartchallenge;Uid=root;Pwd=Kagawa@123",new MySqlServerVersion(new Version(8,0,22)));



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PRIMARY");

            entity.ToTable("discount", "Watchcartchallenge");

            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.DiscountPrice)
                .HasPrecision(10)
                .HasColumnName("discount_price");
            entity.Property(e => e.DiscountQuantity).HasColumnName("discount_quantity");
        });

        modelBuilder.Entity<Watch>(entity =>
        {
            entity.HasKey(e => e.WatchId).HasName("PRIMARY");

            entity.ToTable("watch", "Watchcartchallenge");

            entity.HasIndex(e => e.DiscountId, "discount_id_idx");

            entity.Property(e => e.WatchId).HasColumnName("watch_id");
            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10)
                .HasColumnName("unit_price");
            entity.Property(e => e.WatchName)
                .HasMaxLength(50)
                .HasColumnName("watch_name");

            entity.HasOne(d => d.Discount).WithMany(p => p.Watches)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("discount_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
