using crud_net_postgres.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace crud_net_postgres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select ""ID"", 
                        ""Name"", 
                        ""DepartmentID"", 
                        to_char(""DateOfJoining"", 'YYYY-MM-DD') as ""DateOfJoining"", 
                        ""PhotoFileName""
                from public.""Employees""
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee employee)
        {
            string query = @"
                insert into public.""Employees""(
                    ""ID"", 
                    ""Name"",
                    ""DepartmentID"",
                    ""DateOfJoining"",
                    ""PhotoFileName""   
                )
                values (
                    @GuidValue, 
                    @Name,
                    @DepartmentID,
                    @DateOfJoining,
                    @PhotoFileName
                )
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@GuidValue", Guid.NewGuid());
                    myCommand.Parameters.AddWithValue("@Name", employee.Name);
                    myCommand.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("Added Successfully!");
        }

        [HttpPut]
        public JsonResult Put(Employee employee)
        {
            string query = @"
                update public.""Employees""
                set ""Name"" = @Name, 
                    ""DepartmentID"" = @DepartmentID,
                    ""DateOfJoining"" = @DateOfJoining,
                    ""PhotoFileName"" = @PhotoFileName   
                where ""ID"" = @EmployeeGuid
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@Name", employee.Name);
                    myCommand.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", Convert.ToDateTime(employee.DateOfJoining));
                    myCommand.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);
                    myCommand.Parameters.AddWithValue("@EmployeeGuid", employee.ID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Updated Successfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(Guid id)
        {
            string query = @"
                delete from public.""Employees""
                where ""ID""=@EmployeeGuid
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeGuid", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Deleted Successfully!");
        }
    }
}
