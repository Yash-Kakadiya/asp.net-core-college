-- CREATE DATABASE CRUDRevision

USE CRUDRevision

CREATE TABLE Department (
    DeptID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName VARCHAR(100) NOT NULL
);

CREATE TABLE Employee (
    EmpID INT PRIMARY KEY IDENTITY(1,1),
    EmpName VARCHAR(100) NOT NULL,
    Salary DECIMAL(10,2) NOT NULL,
    JoiningDate DATETIME NOT NULL,
    City VARCHAR(100) NOT NULL,
    DeptID INT NOT NULL FOREIGN KEY REFERENCES Department(DeptID)
);

CREATE OR ALTER PROCEDURE PR_Department_SelectAll
AS
BEGIN
    SELECT DeptID, DepartmentName
    FROM Department;
END
GO

CREATE OR ALTER PROCEDURE PR_Department_SelectByPK
    @DeptID INT
AS
BEGIN
    SELECT DeptID, DepartmentName
    FROM Department
    WHERE DeptID = @DeptID;
END
GO

CREATE OR ALTER PROCEDURE PR_Department_Insert
    @DepartmentName VARCHAR(100)
AS
BEGIN
    INSERT INTO Department(DepartmentName)
    VALUES(@DepartmentName);
END
GO

CREATE OR ALTER PROCEDURE PR_Department_Update
    @DeptID INT,
    @DepartmentName VARCHAR(100)
AS
BEGIN
    UPDATE Department
    SET DepartmentName = @DepartmentName
    WHERE DeptID = @DeptID;
END
GO

CREATE OR ALTER PROCEDURE PR_Department_Delete
    @DeptID INT
AS
BEGIN
    DELETE FROM Department WHERE DeptID = @DeptID;
END
GO

CREATE OR ALTER PROCEDURE PR_Employee_SelectAll
AS
BEGIN
    SELECT 
        E.EmpID,
        E.EmpName,
        E.Salary,
        E.JoiningDate,
        E.City,
        E.DeptID,
        D.DepartmentName
    FROM Employee E
    LEFT JOIN Department D ON D.DeptID = E.DeptID;
END
GO

CREATE OR ALTER PROCEDURE PR_Employee_SelectByPK
    @EmpID INT
AS
BEGIN
    SELECT 
        EmpID,
        EmpName,
        Salary,
        JoiningDate,
        City,
        DeptID
    FROM Employee
    WHERE EmpID = @EmpID;
END
GO

CREATE OR ALTER PROCEDURE PR_Employee_Insert
    @EmpName VARCHAR(100),
    @Salary DECIMAL(10,2),
    @JoiningDate DATETIME,
    @City VARCHAR(100),
    @DeptID INT
AS
BEGIN
    INSERT INTO Employee (EmpName, Salary, JoiningDate, City, DeptID)
    VALUES (@EmpName, @Salary, @JoiningDate, @City, @DeptID);
END
GO

CREATE OR ALTER PROCEDURE PR_Employee_Update
    @EmpID INT,
    @EmpName VARCHAR(100),
    @Salary DECIMAL(10,2),
    @JoiningDate DATETIME,
    @City VARCHAR(100),
    @DeptID INT
AS
BEGIN
    UPDATE Employee
    SET 
        EmpName = @EmpName,
        Salary = @Salary,
        JoiningDate = @JoiningDate,
        City = @City,
        DeptID = @DeptID
    WHERE EmpID = @EmpID;
END
GO


-- Dashboard SPs

-- City-wise Employees
CREATE OR ALTER PROCEDURE PR_Dashboard_CityWiseEmployees
AS
BEGIN
    SELECT 
        City,
        COUNT(*) AS TotalEmployees
    FROM Employee
    GROUP BY City
    ORDER BY TotalEmployees DESC;
END
GO

-- Department-wise Employees
CREATE OR ALTER PROCEDURE PR_Dashboard_DepartmentWiseEmployees
AS
BEGIN
    SELECT 
        D.DepartmentName,
        COUNT(E.EmpID) AS TotalEmployees
    FROM Department D
    LEFT JOIN Employee E ON E.DeptID = D.DeptID
    GROUP BY D.DepartmentName
    ORDER BY TotalEmployees DESC;
END
GO

-- Employees Joined This Month
CREATE OR ALTER PROCEDURE PR_Dashboard_EmployeesThisMonth
AS
BEGIN
    SELECT 
        EmpID,
        EmpName,
        Salary,
        JoiningDate,
        City,
        DeptID
    FROM Employee
    WHERE MONTH(JoiningDate) = MONTH(GETDATE())
      AND YEAR(JoiningDate) = YEAR(GETDATE());
END
GO

-- Departments Without Employees
CREATE OR ALTER PROCEDURE PR_Dashboard_EmptyDepartments
AS
BEGIN
    SELECT 
        D.DeptID,
        D.DepartmentName
    FROM Department D
    LEFT JOIN Employee E ON D.DeptID = E.DeptID
    WHERE E.EmpID IS NULL;
END
GO

---

--- Filter ---
CREATE PROCEDURE PR_Employee_Search
    @EmpName VARCHAR(100) = NULL,
    @MinSalary DECIMAL(10,2) = NULL,
    @MaxSalary DECIMAL(10,2) = NULL,
    @DeptID INT = 0,
    @JoiningDateFrom DATE = NULL,
    @JoiningDateTo DATE = NULL,
    @City VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        E.EmpID,
        E.EmpName,
        E.Salary,
        E.JoiningDate,
        E.City,
        E.DeptID,
        D.DepartmentName
    FROM Employee E
    INNER JOIN Department D ON E.DeptID = D.DeptID
    WHERE
        (@EmpName IS NULL OR @EmpName = '' OR E.EmpName LIKE '%' + @EmpName + '%')
        AND (@MinSalary IS NULL OR E.Salary >= @MinSalary)
        AND (@MaxSalary IS NULL OR E.Salary <= @MaxSalary)
        AND (@DeptID = 0 OR E.DeptID = @DeptID)
        AND (@JoiningDateFrom IS NULL OR CAST(E.JoiningDate AS DATE) >= @JoiningDateFrom)
        AND (@JoiningDateTo IS NULL OR CAST(E.JoiningDate AS DATE) <= @JoiningDateTo)
        AND (@City IS NULL OR @City = '' OR E.City LIKE '%' + @City + '%')
    ORDER BY E.EmpName;
END