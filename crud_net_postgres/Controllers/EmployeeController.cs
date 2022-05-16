using crud_net_postgres.Context;
using crud_net_postgres.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace crud_net_postgres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _myContext;
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration, DataContext myContext)
        {
            _myContext = myContext;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _myContext.Employees.ToListAsync();
            /*
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
            */

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            employee.ID = Guid.NewGuid();
            await _myContext.AddAsync(employee);
            await _myContext.SaveChangesAsync();

            /*
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
            */

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Employee employee)
        {
            _myContext.Update(employee);
            await _myContext.SaveChangesAsync();

            /*
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
            */
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _myContext.Employees.
               AsQueryable()
               .FirstOrDefaultAsync(x => x.ID == id);

            if (response == null)
            {
                return NotFound();
            }

            _myContext.Remove(response);
            await _myContext.SaveChangesAsync();

            /*
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
            */
            return Ok();
        }
    }
}
