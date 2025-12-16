using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LeaveMS.Models;

public partial class LeaveMSContext : DbContext
{
    public LeaveMSContext()
    {
    }

    public LeaveMSContext(DbContextOptions<LeaveMSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Leave> Leaves { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Leave>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__Leave__796DB95907775D70");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.Leaves)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Leave__LeaveType__4BAC3F29");
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(e => e.LeaveTypeId).HasName("PK__LeaveTyp__43BE8F14F9C101DD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
