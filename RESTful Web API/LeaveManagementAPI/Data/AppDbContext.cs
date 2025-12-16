using LeaveManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API examples (optional - Data Annotations already handle most)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<LeaveBalance>()
                .HasIndex(lb => new { lb.UserId, lb.LeaveTypeId, lb.Year })
                .IsUnique();

            // Data Seeding (Section 11)
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Employee" }
            );

            // Seed Leave Types
            modelBuilder.Entity<LeaveType>().HasData(
                new LeaveType { Id = 1, Name = "Vacation", Description = "Annual paid vacation" },
                new LeaveType { Id = 2, Name = "Sick", Description = "Paid sick leave" }
            );

            // Seed Users (requires Roles to exist)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john@company.com",
                    Password = "hashedpassword123",
                    RoleId = 2,
                    PhoneNumber = "123-456-7890"
                }
            );
        }
    }
}
