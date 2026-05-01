using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveMS.Model
{
    public class ApplyForLeaveDto
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee Name is required.")]
        public string EmployeeName { get; set; }

        //Casual, Sick, Earned
        [Required(ErrorMessage = "Leave Type is required.")]
        [EnumDataType(typeof(LeaveTypeEnum), ErrorMessage = "Invalid Leave Type.")]
        public string LeaveType { get; set; }

        //earlier than ToDate
        [Required(ErrorMessage = "From Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid From Date format.")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "To Date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid To Date format.")]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Reason is required.")]
        [MinLength(10)]
        public string Reason { get; set; }

        public enum LeaveTypeEnum
        {
            Casual,
            Sick,
            Earned
        }

    }
}
