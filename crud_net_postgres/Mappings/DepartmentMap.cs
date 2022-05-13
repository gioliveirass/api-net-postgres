using crud_net_postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace crud_net_postgres.Mappings
{
	public class DepartmentMap : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.Property(property => property.Name).HasColumnType("varchar(50)").IsRequired();
			builder.HasData(new Department(Guid.NewGuid(), "RH"));
			builder.HasData(new Department(Guid.NewGuid(), "TI"));
		}
	}
}
