using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventManagementApiExam.Models;

[Table("EventRegistration")]
public partial class EventRegistration
{
    [Key]
    public int RegistrationId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ParticipantName { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string EventName { get; set; } = null!;

    public int? Age { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RegistrationDate { get; set; }
}
