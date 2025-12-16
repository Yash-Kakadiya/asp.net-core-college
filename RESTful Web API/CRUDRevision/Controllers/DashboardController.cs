using CRUDRevision.Models.Dashboard;  // IMPORTANT FIX
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRUDRevision.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IConfiguration _config;

        public DashboardController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            // 🔥 Use DashboardModel (NOT DashboardData)
            DashboardModel data = new DashboardModel();

            // Load tables
            data.CityWise = GetData("PR_Dashboard_CityWiseEmployees");
            data.DepartmentWise = GetData("PR_Dashboard_DepartmentWiseEmployees");
            data.EmployeesThisMonth = GetData("PR_Dashboard_EmployeesThisMonth");
            data.EmptyDepartments = GetData("PR_Dashboard_EmptyDepartments");

            // New charts
            data.SalaryByDepartment = GetQuery(@"
                SELECT D.DepartmentName, AVG(E.Salary) AS AvgSalary
                FROM Department D
                LEFT JOIN Employee E ON D.DeptID = E.DeptID
                GROUP BY D.DepartmentName
            ");

            data.JoiningTrendLast6Months = GetQuery(@"
                SELECT 
                    DATENAME(MONTH, JoiningDate) AS MonthName,
                    COUNT(*) AS NewEmployees
                FROM Employee
                WHERE JoiningDate >= DATEADD(MONTH, -6, GETDATE())
                GROUP BY DATENAME(MONTH, JoiningDate), MONTH(JoiningDate)
                ORDER BY MONTH(JoiningDate)
            ");

            // Summary Cards
            data.TotalEmployees = GetScalar("SELECT COUNT(*) FROM Employee");
            data.TotalDepartments = GetScalar("SELECT COUNT(*) FROM Department");
            data.TotalThisMonth = data.EmployeesThisMonth.Rows.Count;
            data.TotalEmptyDepartments = data.EmptyDepartments.Rows.Count;

            return View(data);   // FIXED: Pass DashboardModel
        }

        // Fetch DataTable from stored procedure
        private DataTable GetData(string storedProcedure)
        {
            DataTable dt = new DataTable();
            string con = _config.GetConnectionString("ConnectionString");

            using SqlConnection sql = new SqlConnection(con);
            sql.Open();
            using SqlCommand cmd = new SqlCommand(storedProcedure, sql);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);

            return dt;
        }

        // For SQL query (not stored procedure)
        private DataTable GetQuery(string query)
        {
            DataTable dt = new DataTable();
            string con = _config.GetConnectionString("ConnectionString");

            using SqlConnection sql = new SqlConnection(con);
            sql.Open();
            using SqlCommand cmd = new SqlCommand(query, sql);
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);

            return dt;
        }

        private int GetScalar(string query)
        {
            string con = _config.GetConnectionString("ConnectionString");

            using SqlConnection sql = new SqlConnection(con);
            sql.Open();
            using SqlCommand cmd = new SqlCommand(query, sql);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
