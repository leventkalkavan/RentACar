using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars");

        builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
        builder.Property(x => x.ModelId).HasColumnName("ModelId").IsRequired();
        builder.Property(x => x.Km).HasColumnName("Km").IsRequired();
        builder.Property(x => x.CarState).HasColumnName("CarState").IsRequired();
        builder.Property(x => x.ModelYear).HasColumnName("ModelYear").IsRequired();
        
        builder.HasOne(b => b.Model);

        builder.HasQueryFilter(x => !x.DeletedDate.HasValue);
    }
}