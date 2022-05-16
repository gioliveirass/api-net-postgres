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
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _myContext;
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration, DataContext myContext)
        {
            _myContext = myContext; 
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _myContext.Departments.ToListAsync();

            // var response = await _myContext.Departments.Where(x => x.Name == "Teste").ToListAsync();

            /*
            string query = @"
                select ""ID"", ""Name""
                from public.""Departments""
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
        public async Task<IActionResult> Post(Department department)
        {
            department.ID = Guid.NewGuid();
            await _myContext.AddAsync(department);
            await _myContext.SaveChangesAsync();

            /*
            string query = @"
                insert into public.""Departments""(""ID"", ""Name"")
                values (@GuidValue, @DepartmentName)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", department.Name);
                    myCommand.Parameters.AddWithValue("@GuidValue", Guid.NewGuid());
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
        public async Task<IActionResult> Put(Department department)
        {
            _myContext.Update(department);
            await _myContext.SaveChangesAsync();

            /*
            string query = @"
                update public.""Departments""
                set ""Name""=@DepartmentName
                where ""ID""=@DepartmentGuid
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", department.Name);
                    myCommand.Parameters.AddWithValue("@DepartmentGuid", department.ID);
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
            var response = await _myContext.Departments.
                AsQueryable()
                .FirstOrDefaultAsync(x => x.ID == id);

            if(response == null)
            {
                return NotFound();
            }

            _myContext.Remove(response);
            await _myContext.SaveChangesAsync();
            /*
            string query = @"
                delete from public.""Departments""
                where ""ID""=@DepartmentGuid
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myConn = new NpgsqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentGuid", id);
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
