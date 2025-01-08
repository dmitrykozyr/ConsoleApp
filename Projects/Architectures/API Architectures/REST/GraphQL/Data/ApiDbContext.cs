using GraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    public virtual DbSet<ItemData> Items { get; set; }
    public virtual DbSet<ItemList> Lists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ItemData>(entity =>
        {
            entity.HasOne(z => z.ItemList)
                  .WithMany(z => z.ItemDatas)
                  .HasForeignKey(z => z.ListId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_ItemData_ItemList");
        });
    }
}
