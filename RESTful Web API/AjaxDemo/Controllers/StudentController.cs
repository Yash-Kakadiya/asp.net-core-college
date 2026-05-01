using AjaxDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace AjaxDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        public static List<StudentModel> students = new List<StudentModel>();

            public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;

            // Seed only once (previous implementation reinitialized on every controller instance)
            if (students.Count == 0)
            {
                students.Add(new StudentModel()
                {
                    Id = 1,
                    Email = "ankush1802@outlook.com",
                    Name = "Ankush"
                });
                students.Add(new StudentModel()
                {
                    Id = 2,
                    Email = "rohit@outlook.com",
                    Name = "Rohit"
                });
                students.Add(new StudentModel()
                {
                    Id = 3,
                    Email = "sunny@outlook.com",
                    Name = "Sunny"
                });
                students.Add(new StudentModel()
                {
                    Id = 4,
                    Email = "amit@outlook.com",
                    Name = "Amit"
                });
            }
        }

        public IActionResult Index()
        {
            return View(students);
        }

        [HttpGet]
        public JsonResult GetDetailsById(int id)
        {
            var student = students.FirstOrDefault(d => d.Id == id);
            JsonResponseViewModel model = new JsonResponseViewModel();
            if (student != null)
            {
                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(student);
            }
            else
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "No record available";
            }
            return Json(model);
        }

        [HttpPost]
        public JsonResult InsertStudent(IFormCollection formcollection)
        {
            JsonResponseViewModel model = new JsonResponseViewModel();

            string email = formcollection["email"];
            string name = formcollection["name"];

            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(email))
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "Invalid input";
                return Json(model);
            }

            // Generate next id
            int nextId = students.Any() ? students.Max(s => s.Id) + 1 : 1;

            StudentModel student = new StudentModel
            {
                Id = nextId,
                Email = email,
                Name = name
            };

            students.Add(student);

            model.ResponseCode = 0;
            model.ResponseMessage = JsonConvert.SerializeObject(student);
            return Json(model);
        }

        [HttpPut]
        public JsonResult UpdateStudent(IFormCollection formcollection)
        {
            JsonResponseViewModel model = new JsonResponseViewModel();

            if (!int.TryParse(formcollection["id"], out int id))
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "Invalid id";
                return Json(model);
            }

            var existing = students.FirstOrDefault(s => s.Id == id);
            if (existing == null)
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "No record available";
                return Json(model);
            }

            string email = formcollection["email"];
            string name = formcollection["name"];

            // Update fields if provided
            if (!string.IsNullOrWhiteSpace(email))
                existing.Email = email;
            if (!string.IsNullOrWhiteSpace(name))
                existing.Name = name;

            model.ResponseCode = 0;
            model.ResponseMessage = JsonConvert.SerializeObject(existing);
            return Json(model);
        }

        /// <summary>
        /// Bulk insert students from a CSV or Excel (.xlsx) file.
        /// CSV format: Each row should have Name and Email columns (header row required).
        /// Excel format: First sheet is read. First row should have Name and Email headers.
        /// </summary>
        [HttpPost]
        public JsonResult BulkInsertStudents(IFormFile file)
        {
            JsonResponseViewModel model = new JsonResponseViewModel();

            // Step 1: Check if a file was uploaded
            if (file == null || file.Length == 0)
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "Please upload a file.";
                return Json(model);
            }

            // Step 2: Get the file extension to decide how to read it
            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            // This list will hold all the new students we read from the file
            List<StudentModel> newStudents = new List<StudentModel>();

            try
            {
                if (fileExtension == ".csv")
                {
                    // ---------- READ CSV FILE ----------
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        bool isFirstLine = true;
                        int nameIndex = -1;
                        int emailIndex = -1;

                        while (!reader.EndOfStream)
                        {
                            string? line = reader.ReadLine();
                            if (string.IsNullOrWhiteSpace(line)) continue;

                            // Split the line by comma
                            string[] columns = line.Split(',');

                            if (isFirstLine)
                            {
                                // First line is the header row — find which column is Name and Email
                                for (int i = 0; i < columns.Length; i++)
                                {
                                    string header = columns[i].Trim().ToLower();
                                    if (header == "name") nameIndex = i;
                                    else if (header == "email") emailIndex = i;
                                }

                                // Make sure both columns exist
                                if (nameIndex == -1 || emailIndex == -1)
                                {
                                    model.ResponseCode = 1;
                                    model.ResponseMessage = "CSV file must have 'Name' and 'Email' columns in the header row.";
                                    return Json(model);
                                }

                                isFirstLine = false;
                                continue; // Skip header row
                            }

                            // Read Name and Email from the correct columns
                            string name = columns.Length > nameIndex ? columns[nameIndex].Trim() : "";
                            string email = columns.Length > emailIndex ? columns[emailIndex].Trim() : "";

                            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email))
                            {
                                newStudents.Add(new StudentModel { Name = name, Email = email });
                            }
                        }
                    }
                }
                else if (fileExtension == ".xlsx")
                {
                    // ---------- READ EXCEL FILE ----------
                    // EPPlus requires a license context (NonCommercial for free use)
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var stream = file.OpenReadStream())
                    using (var package = new ExcelPackage(stream))
                    {
                        // Read the first worksheet
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        // Find total rows and columns
                        int totalRows = worksheet.Dimension.Rows;
                        int totalCols = worksheet.Dimension.Columns;

                        // Find which columns are Name and Email from the header row (row 1)
                        int nameCol = -1;
                        int emailCol = -1;

                        for (int col = 1; col <= totalCols; col++)
                        {
                            string header = worksheet.Cells[1, col].Text.Trim().ToLower();
                            if (header == "name") nameCol = col;
                            else if (header == "email") emailCol = col;
                        }

                        if (nameCol == -1 || emailCol == -1)
                        {
                            model.ResponseCode = 1;
                            model.ResponseMessage = "Excel file must have 'Name' and 'Email' columns in the first row.";
                            return Json(model);
                        }

                        // Read data rows (starting from row 2)
                        for (int row = 2; row <= totalRows; row++)
                        {
                            string name = worksheet.Cells[row, nameCol].Text.Trim();
                            string email = worksheet.Cells[row, emailCol].Text.Trim();

                            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email))
                            {
                                newStudents.Add(new StudentModel { Name = name, Email = email });
                            }
                        }
                    }
                }
                else
                {
                    model.ResponseCode = 1;
                    model.ResponseMessage = "Only .csv and .xlsx files are supported.";
                    return Json(model);
                }

                // Step 3: Check if we got any valid students
                if (newStudents.Count == 0)
                {
                    model.ResponseCode = 1;
                    model.ResponseMessage = "No valid student records found in the file.";
                    return Json(model);
                }

                // Step 4: Assign IDs and add to the list
                int nextId = students.Any() ? students.Max(s => s.Id) + 1 : 1;

                foreach (var student in newStudents)
                {
                    student.Id = nextId;
                    nextId++;
                    students.Add(student);
                }

                model.ResponseCode = 0;
                model.ResponseMessage = $"Successfully inserted {newStudents.Count} student(s).";
            }
            catch (Exception ex)
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "Error reading file: " + ex.Message;
            }

            return Json(model);
        }

        [HttpDelete]
        public JsonResult DeleteStudent(IFormCollection formcollection)
        {
            JsonResponseViewModel model = new JsonResponseViewModel();

            if (!int.TryParse(formcollection["id"], out int id))
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "Invalid id";
                return Json(model);
            }

            var existing = students.FirstOrDefault(s => s.Id == id);
            if (existing == null)
            {
                model.ResponseCode = 1;
                model.ResponseMessage = "No record available";
                return Json(model);
            }

            students.Remove(existing);

            model.ResponseCode = 0;
            model.ResponseMessage = JsonConvert.SerializeObject(existing);
            return Json(model);
        }
    }
}
