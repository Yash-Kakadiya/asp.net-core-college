-- create database EventManagementDb
use EventManagementDb

create table EventRegistration(
	RegistrationId int primary key identity(1,1),
	ParticipantName varchar(100) not null,
	Email varchar(150) not null,
	EventName varchar(150) not null,
	Age int,
	RegistrationDate DateTime not null
);