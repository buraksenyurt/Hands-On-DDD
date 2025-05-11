using App.Domain.MemberProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure;

public class MembershipDbContext(DbContextOptions<MembershipDbContext> options)
    : DbContext(options)
{
    public DbSet<Member> Members { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MemberConfiguration());
    }

    public class MemberConfiguration
        : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => m.Id);
            builder.OwnsOne(m => m.Id);
            builder.OwnsOne(m => m.Email);
            builder.OwnsOne(m => m.FullName);
        }
    }
}
