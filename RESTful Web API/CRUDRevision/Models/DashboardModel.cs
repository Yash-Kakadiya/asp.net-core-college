using System;
using System.Data;

namespace CRUDRevision.Models.Dashboard
{
    public class DashboardModel
    {
        /// <summary>
        /// City, TotalEmployees
        /// </summary>
        public DataTable CityWise { get; set; }

        /// <summary>
        /// DepartmentName, TotalEmployees
        /// </summary>
        public DataTable DepartmentWise { get; set; }

        /// <summary>
        /// EmpID, EmpName, Salary, JoiningDate, City, DeptID
        /// </summary>
        public DataTable EmployeesThisMonth { get; set; }

        /// <summary>
        /// DeptID, DepartmentName -> departments with no employees
        /// </summary>
        public DataTable EmptyDepartments { get; set; }

        /// <summary>
        /// DepartmentName, AvgSalary
        /// </summary>
        public DataTable SalaryByDepartment { get; set; }

        /// <summary>
        /// MonthName, NewEmployees (last 6 months)
        /// </summary>
        public DataTable JoiningTrendLast6Months { get; set; }

        // Summary cards
        public int TotalEmployees { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalThisMonth { get; set; }
        public int TotalEmptyDepartments { get; set; }

        // Optional: convenience constructor that initializes empty DataTables to avoid null checks in view
        public DashboardModel()
        {
            CityWise = CreateEmptyTable("City", "TotalEmployees");
            DepartmentWise = CreateEmptyTable("DepartmentName", "TotalEmployees");
            EmployeesThisMonth = CreateEmptyTable("EmpID", "EmpName", "Salary", "JoiningDate", "City", "DeptID");
            EmptyDepartments = CreateEmptyTable("DeptID", "DepartmentName");
            SalaryByDepartment = CreateEmptyTable("DepartmentName", "AvgSalary");
            JoiningTrendLast6Months = CreateEmptyTable("MonthName", "NewEmployees");
        }

        private DataTable CreateEmptyTable(params string[] columns)
        {
            DataTable dt = new DataTable();
            foreach (var col in columns)
            {
                dt.Columns.Add(col);
            }
            return dt;
        }
    }
}
