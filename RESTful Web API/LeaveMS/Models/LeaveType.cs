using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LeaveMS.Models;

[Table("LeaveType")]
public partial class LeaveType
{
    [Key]
    public int LeaveTypeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string LeaveName { get; set; } = null!;

    [Column("remarks")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Remarks { get; set; }

    [InverseProperty("LeaveType")]
    public virtual ICollection<Leave> Leaves { get; set; } = new List<Leave>();
}
