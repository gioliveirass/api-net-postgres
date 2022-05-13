namespace crud_net_postgres.Models
{
	public class Department
	{
		protected Department() => Employees = new List<Employee>();
		public Department(Guid id, string name) : this()
		{
			ID = id;
			Name = name;
		}

		public Guid ID { get; set; }
		public string Name { get; set; }
		public virtual ICollection<Employee> Employees { get; set; }

	}
}
