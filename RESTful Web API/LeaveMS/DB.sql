-- create databse LeaveMS
use LeaveMS

create table LeaveType(
	LeaveTypeId INT Primary Key IDENTITY(1,1),
	LeaveName varchar(100) not null,
	remarks varchar(200),
)

create table Leave (
	LeaveId INT PRimary key Identity(1,1),
	LeaveTypeId INT NOT NULL Foreign Key References LeaveType(LeaveTypeId),
	TotalLeaves INT NOT NULL,
	Year INT
)