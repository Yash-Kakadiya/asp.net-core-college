using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TicketManagementSystem.Models;

public partial class TicketStatusLog
{
    [Key]
    public int Id { get; set; }

    public int TicketId { get; set; }

    [StringLength(20)]
    public string OldStatus { get; set; } = null!;

    [StringLength(20)]
    public string NewStatus { get; set; } = null!;

    public int ChangedBy { get; set; }

    public DateTime ChangedAt { get; set; }

    [ForeignKey("ChangedBy")]
    [InverseProperty("TicketStatusLogs")]
    public virtual User ChangedByNavigation { get; set; } = null!;

    [ForeignKey("TicketId")]
    [InverseProperty("TicketStatusLogs")]
    public virtual Ticket Ticket { get; set; } = null!;
}
