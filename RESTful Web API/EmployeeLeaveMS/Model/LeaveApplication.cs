using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace EmployeeLeaveMS.Model
{

    //    Property
    //LeaveId
    //EmployeeId
    //Data Type
    //int
    //int
    //EmployeeName string
    //LeaveType
    //string
    //FromDate
    //ToDate
    //Reason
    //DateTime
    //DateTime
    //string
    public class LeaveApplication
    {
        [Key]
        public int LeaveId { get; set; }
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string LeaveType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Reason { get; set; }

        public enum LeaveTypeEnum
        {
            Casual,
            Sick,
            Earned
        }


    }
}
