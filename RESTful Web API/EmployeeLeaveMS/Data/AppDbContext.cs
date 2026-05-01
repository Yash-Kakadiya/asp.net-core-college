using EmployeeLeaveMS.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<LeaveApplication> LeaveApplications { get; set; }

    }
}