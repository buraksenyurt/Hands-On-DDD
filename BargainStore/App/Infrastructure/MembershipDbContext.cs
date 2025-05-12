using App.Domain.MemberProfile;
using App.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure;

public class MembershipDbContext(DbContextOptions<MembershipDbContext> options)
    : DbContext(options)
{
    public DbSet<Member> Members { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(builder =>
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .HasConversion(
                        id => id.ToString(),
                        id => new MemberId(Guid.Parse(id)))
                   .HasColumnName("Id")
                   .IsRequired();

            builder.OwnsOne(m => m.Email, email =>
            {
                email.Property(e => e.Value)
                     .HasColumnName("Email")
                     .IsRequired();
            });

            builder.OwnsOne(m => m.FullName, fullName =>
            {
                fullName.Property(f => f.Value)
                        .HasColumnName("FullName")
                        .IsRequired();
            });

            builder.Property(m => m.CreatedAt).IsRequired();
            builder.Property(m => m.UpdatedAt).IsRequired();
        });
    }
}
