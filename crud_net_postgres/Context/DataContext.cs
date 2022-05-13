using crud_net_postgres.Mappings;
using crud_net_postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace crud_net_postgres.Context
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new DepartmentMap());
			modelBuilder.ApplyConfiguration(new EmployeeMap());
		}
		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }
	}
}
