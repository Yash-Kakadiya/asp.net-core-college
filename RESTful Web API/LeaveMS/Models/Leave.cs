using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LeaveMS.Models;

[Table("Leave")]
public partial class Leave
{
    [Key]
    public int LeaveId { get; set; }

    public int LeaveTypeId { get; set; }

    public int TotalLeaves { get; set; }

    public int? Year { get; set; }

    [ForeignKey("LeaveTypeId")]
    [InverseProperty("Leaves")]
    public virtual LeaveType LeaveType { get; set; } = null!;
}
