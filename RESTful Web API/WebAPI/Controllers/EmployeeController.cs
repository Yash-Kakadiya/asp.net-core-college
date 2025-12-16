using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            DataTable dt = new DataTable();
            string con = _config.GetConnectionString("ConnectionString");

            using (SqlConnection sql = new SqlConnection(con))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand("PR_Employee_SelectAll", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }
            string res = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return Ok(res);
        }
    }
}
