namespace crud_net_postgres.Models
{
	public class Employee
	{
		public Employee() { }
		public Employee(Guid id, string name, Guid departmentID, DateTime dateOfJoining, string photoFileName) : this()
		{
			ID = id;
			Name = name;
			DepartmentID = departmentID;
			DateOfJoining = dateOfJoining;
			PhotoFileName = photoFileName;
		}

		public Guid ID { get; set; }
		public string Name { get; set; }
		public Guid DepartmentID { get; set; }
		public DateTime DateOfJoining { get; set; }
		public string PhotoFileName { get; set; }
		public Department Department { get; set; }
	}
}
