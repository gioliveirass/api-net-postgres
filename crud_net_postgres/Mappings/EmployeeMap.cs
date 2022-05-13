using crud_net_postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace crud_net_postgres.Mappings
{
	public class EmployeeMap : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(property => property.Name).HasColumnType("varchar(50)").IsRequired();
			builder.Property(property => property.PhotoFileName).HasColumnType("varchar(500)").IsRequired();
			builder.Property(property => property.DateOfJoining).IsRequired();

			builder.HasOne(property => property.Department)
				.WithMany(property => property.Employees)
				.HasForeignKey(property => property.DepartmentID)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
