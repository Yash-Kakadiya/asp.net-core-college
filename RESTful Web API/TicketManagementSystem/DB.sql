CREATE DATABASE TicketDB;
GO

USE TicketDB;
GO

-- Roles
CREATE TABLE Roles (
    Id      INT PRIMARY KEY IDENTITY(1,1),
    Name    NVARCHAR(50) NOT NULL,
    CONSTRAINT UQ_Roles_Name UNIQUE (Name),
    CONSTRAINT CK_Roles_Name CHECK (Name IN ('MANAGER', 'SUPPORT', 'USER'))
);
GO

-- Users
CREATE TABLE Users (
    Id           INT PRIMARY KEY IDENTITY(1,1),
    Name         NVARCHAR(255) NOT NULL,
    Email        NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    RoleId       INT NOT NULL,
    CreatedAt    DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT UQ_Users_Email UNIQUE (Email),
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);
GO

-- Tickets
CREATE TABLE Tickets (
    Id          INT PRIMARY KEY IDENTITY(1,1),
    Title       NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Status      NVARCHAR(20) NOT NULL DEFAULT 'OPEN',
    Priority    NVARCHAR(10) NOT NULL DEFAULT 'MEDIUM',
    CreatedBy   INT NOT NULL,
    AssignedTo  INT NULL,
    CreatedAt   DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT CK_Tickets_Status CHECK (Status IN ('OPEN', 'IN_PROGRESS', 'RESOLVED', 'CLOSED')),
    CONSTRAINT CK_Tickets_Priority CHECK (Priority IN ('LOW', 'MEDIUM', 'HIGH')),
    CONSTRAINT FK_Tickets_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    CONSTRAINT FK_Tickets_AssignedTo FOREIGN KEY (AssignedTo) REFERENCES Users(Id)
);
GO

-- TicketComments
CREATE TABLE TicketComments (
    Id        INT PRIMARY KEY IDENTITY(1,1),
    TicketId  INT NOT NULL,
    UserId    INT NOT NULL,
    Comment   NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT FK_TicketComments_Tickets FOREIGN KEY (TicketId) REFERENCES Tickets(Id) ON DELETE CASCADE,
    CONSTRAINT FK_TicketComments_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);
GO

-- TicketStatusLogs
CREATE TABLE TicketStatusLogs (
    Id         INT PRIMARY KEY IDENTITY(1,1),
    TicketId   INT NOT NULL,
    OldStatus  NVARCHAR(20) NOT NULL,
    NewStatus  NVARCHAR(20) NOT NULL,
    ChangedBy  INT NOT NULL,
    ChangedAt  DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT CK_TicketStatusLogs_OldStatus CHECK (OldStatus IN ('OPEN', 'IN_PROGRESS', 'RESOLVED', 'CLOSED')),
    CONSTRAINT CK_TicketStatusLogs_NewStatus CHECK (NewStatus IN ('OPEN', 'IN_PROGRESS', 'RESOLVED', 'CLOSED')),
    CONSTRAINT FK_TicketStatusLogs_Tickets FOREIGN KEY (TicketId) REFERENCES Tickets(Id) ON DELETE CASCADE,
    CONSTRAINT FK_TicketStatusLogs_Users FOREIGN KEY (ChangedBy) REFERENCES Users(Id)
);
GO

-- insert
INSERT INTO Roles VALUES ('MANAGER'),('SUPPORT'),('USER');
GO

INSERT INTO Users (Name, Email, PasswordHash, RoleId)
VALUES (
    'Admin Manager',
    'manager@company.com',
    '$2a$11$GCwPm5IknRJki4xZ0Ue3COEM0si7mSVXgB0Q/lXeGUA8ibAo7K2zq',
    1
);
GO
SELECT * FROM Roles;
SELECT * FROM Users;
GO
