using ClosedXML.Excel;
using CRUDRevision.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static CRUDRevision.Models.EmployeeModel;

namespace CRUDRevision.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
        }

        // ------------------ SELECT ALL -------------------
        public IActionResult Index(
            string EmpName = "",
            decimal? MinSalary = null,
            decimal? MaxSalary = null,
            int DeptID = 0,
            DateTime? JoiningDateFrom = null,
            DateTime? JoiningDateTo = null,
            string City = "")
        {
            // Load dropdown list for Department
            LoadDepartmentDropdown(DeptID);

            DataTable dt = new DataTable();

            try
            {
                string connStr = _config.GetConnectionString("ConnectionString");

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "PR_Employee_Search";

                        // Passing Search Values to Stored Procedure
                        cmd.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = EmpName ?? "";
                        cmd.Parameters.Add("@MinSalary", SqlDbType.Decimal).Value = (object?)MinSalary ?? DBNull.Value;
                        cmd.Parameters.Add("@MaxSalary", SqlDbType.Decimal).Value = (object?)MaxSalary ?? DBNull.Value;
                        cmd.Parameters.Add("@DeptID", SqlDbType.Int).Value = DeptID;
                        cmd.Parameters.Add("@JoiningDateFrom", SqlDbType.Date).Value = (object?)JoiningDateFrom ?? DBNull.Value;
                        cmd.Parameters.Add("@JoiningDateTo", SqlDbType.Date).Value = (object?)JoiningDateTo ?? DBNull.Value;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = City ?? "";

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading employee list: " + ex.Message;
            }

            // Preserve search values
            ViewBag.EmpName = EmpName;
            ViewBag.MinSalary = MinSalary;
            ViewBag.MaxSalary = MaxSalary;
            ViewBag.SelectedDeptID = DeptID;
            ViewBag.JoiningDateFrom = JoiningDateFrom;
            ViewBag.JoiningDateTo = JoiningDateTo;
            ViewBag.City = City;

            return View(dt);
        }

        // Method to load Department dropdown list
        private void LoadDepartmentDropdown(int selectedDeptID = 0)
        {
            List<DepartmentDropDownModel> deptList = new List<DepartmentDropDownModel>();

            string conn = _config.GetConnectionString("ConnectionString");

            using (SqlConnection sql = new SqlConnection(conn))
            {
                sql.Open();
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PR_Department_SelectAll";

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        deptList.Add(new DepartmentDropDownModel
                        {
                            DeptID = Convert.ToInt32(dr["DeptID"]),
                            DepartmentName = dr["DepartmentName"].ToString()
                        });
                    }
                }
            }

            ViewBag.DeptList = deptList;
            ViewBag.DeptID = selectedDeptID;
        }

        // ------------------ ADD EDIT -------------------
        public IActionResult AddEdit(int? EmpID)
        {
            EmployeeModel model = new EmployeeModel();
            LoadDepartmentDropdown();

            if (EmpID != null)
            {
                string con = _config.GetConnectionString("ConnectionString");

                using (SqlConnection sql = new SqlConnection(con))
                {
                    sql.Open();
                    using (SqlCommand cmd = new SqlCommand("PR_Employee_SelectByPK", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpID", EmpID);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            model.EmpID = Convert.ToInt32(dr["EmpID"]);
                            model.EmpName = dr["EmpName"].ToString();
                            model.Salary = Convert.ToDecimal(dr["Salary"]);
                            model.JoiningDate = Convert.ToDateTime(dr["JoiningDate"]);
                            model.City = dr["City"].ToString();
                            model.DeptID = Convert.ToInt32(dr["DeptID"]);
                        }
                    }
                }
            }

            return View(model);
        }

        // ------------------ SAVE -------------------
        [HttpPost]
        public IActionResult Save(EmployeeModel model)
        {
            string con = _config.GetConnectionString("ConnectionString");

            using (SqlConnection sql = new SqlConnection(con))
            {
                sql.Open();
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (model.EmpID == 0)
                        cmd.CommandText = "PR_Employee_Insert";
                    else
                    {
                        cmd.CommandText = "PR_Employee_Update";
                        cmd.Parameters.AddWithValue("@EmpID", model.EmpID);
                    }

                    cmd.Parameters.AddWithValue("@EmpName", model.EmpName);
                    cmd.Parameters.AddWithValue("@Salary", model.Salary);
                    cmd.Parameters.AddWithValue("@JoiningDate", model.JoiningDate);
                    cmd.Parameters.AddWithValue("@City", model.City);
                    cmd.Parameters.AddWithValue("@DeptID", model.DeptID);

                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        // ------------------ DELETE -------------------
        public IActionResult Delete(int EmpID)
        {
            string con = _config.GetConnectionString("ConnectionString");

            using (SqlConnection sql = new SqlConnection(con))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand("PR_Employee_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpID", EmpID);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
        #region Export
        [HttpGet]
        public FileResult ExportToExcel()
        {
            // Step 1: Fetch data same as DepartmentList
            string ConnectionString = _config.GetConnectionString("ConnectionString");
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Employee_SelectAll";
            using SqlDataReader reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Employees");

            // Insert DataTable as an Excel table (structured table)
            var xlTable = worksheet.Cell(1, 1).InsertTable(table, "EmployeesTable", true);
            // Apply built-in Excel Table Style (Ice Blue - Medium 23)
            xlTable.Theme = XLTableTheme.TableStyleMedium23;

            // Autofit columns
            worksheet.Columns().AdjustToContents();

            // Apply borders and styling
            var rngTable = worksheet.RangeUsed();
            rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // Header styling
            var headerRow = rngTable.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Employees.xlsx");
        }

        #endregion
    }
}
