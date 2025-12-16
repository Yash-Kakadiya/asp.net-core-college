using ClosedXML.Excel;
using CRUDRevision.Models;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;

namespace CRUDRevision.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IConfiguration _config;

        public DepartmentController(IConfiguration config)
        {
            _config = config;
        }

        // ----------------- SELECT ALL -----------------
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            string con = _config.GetConnectionString("ConnectionString");

            using (SqlConnection sql = new SqlConnection(con))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand("PR_Department_SelectAll", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }

            return View(dt);
        }

        // ----------------- ADD/EDIT -----------------
        public IActionResult AddEdit(int? DeptID)
        {
            DepartmentModel model = new DepartmentModel();

            if (DeptID != null)
            {
                string con = _config.GetConnectionString("ConnectionString");

                using (SqlConnection sql = new SqlConnection(con))
                {
                    sql.Open();
                    using (SqlCommand cmd = new SqlCommand("PR_Department_SelectByPK", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DeptID", DeptID);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            model.DeptID = Convert.ToInt32(dr["DeptID"]);
                            model.DepartmentName = dr["DepartmentName"].ToString();
                        }
                    }
                }
            }

            return View(model);
        }

        // ----------------- SAVE -----------------
        [HttpPost]
        public IActionResult Save(DepartmentModel model)
        {
            string con = _config.GetConnectionString("ConnectionString");

            using (SqlConnection sql = new SqlConnection(con))
            {
                sql.Open();
                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (model.DeptID == 0)
                        cmd.CommandText = "PR_Department_Insert";
                    else
                    {
                        cmd.CommandText = "PR_Department_Update";
                        cmd.Parameters.AddWithValue("@DeptID", model.DeptID);
                    }

                    cmd.Parameters.AddWithValue("@DepartmentName", model.DepartmentName);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        // ----------------- DELETE -----------------
        public IActionResult Delete(int DeptID)
        {
            string con = _config.GetConnectionString("ConnectionString");

            using (SqlConnection sql = new SqlConnection(con))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand("PR_Department_Delete", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DeptID", DeptID);
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
            command.CommandText = "PR_Department_SelectAll";
            using SqlDataReader reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Departments");

            // Insert DataTable as an Excel table (structured table)
            var xlTable = worksheet.Cell(1, 1).InsertTable(table, "DepartmentsTable", true);
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
                        "Departments.xlsx");
        }

        #endregion

    }
}
