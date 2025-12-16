using LinQLab;

var departments = new List<Department>
{
    new Department { DeptId = 1, DeptName = "HR" },
    new Department { DeptId = 2, DeptName = "IT" },
    new Department { DeptId = 3, DeptName = "Finance" },
    new Department { DeptId = 4, DeptName = "Marketing" }
};

var employees = new List<Employee>
{
    new Employee { Id = 101, Name = "Amit",   Age = 28, Salary = 75000, DeptId = 2, Skills = new List<string>{ "C#", "SQL", "Angular" } },
    new Employee { Id = 102, Name = "Neha",   Age = 34, Salary = 95000, DeptId = 2, Skills = new List<string>{ "Java", "C#", "React" } },
    new Employee { Id = 103, Name = "Raj",    Age = 45, Salary = 60000, DeptId = 1, Skills = new List<string>{ "Excel", "Communication" } },
    new Employee { Id = 104, Name = "Priya",  Age = 29, Salary = 82000, DeptId = 3, Skills = new List<string>{ "Accounting", "SQL" } },
    new Employee { Id = 105, Name = "Karan",  Age = 31, Salary = 88000, DeptId = 2, Skills = new List<string>{ "C#", "Azure", "Docker" } },
    new Employee { Id = 106, Name = "Simran", Age = 26, Salary = 72000, DeptId = 4, Skills = new List<string>{ "Design", "Photoshop" } }
};

////1. Get a list containing only the names of all employees.
//Console.WriteLine("1.");
//var employeeNames = employees.Select(e => e.Name).ToList();
//foreach (var name in employeeNames)
//{
//    Console.WriteLine(name);
//}
//Console.WriteLine("----------------------------------------------------");

////2. Create a list of anonymous objects with each employee’s Name and Annual Salary (Salary × 12).
//Console.WriteLine("2.");
//var nameAndAnnualSalary = employees.Select(e => new { e.Name, AnnualSalary = e.Salary * 12 }).ToList();
//foreach (var item in nameAndAnnualSalary)
//{
//    Console.WriteLine($"Name: {item.Name}, Annual Salary: {item.AnnualSalary}");
//}
//Console.WriteLine("----------------------------------------------------");

////3. Retrieve Name and Salary of all employees older than 30 years.
//Console.WriteLine("3.");
//var olderThan30 = employees.Where(e => e.Age > 30).Select(e => new { e.Name, e.Salary }).ToList();
//foreach (var item in olderThan30)
//{
//    Console.WriteLine($"Name: {item.Name}, Salary: {item.Salary}");
//}
//Console.WriteLine("----------------------------------------------------");

////4. Show complete details of all employees who belong to the IT department.
//Console.WriteLine("4.");
//var itDeptId = departments.First(d => d.DeptName == "IT").DeptId;
//var itEmployees = employees.Where(e => e.DeptId == itDeptId).ToList();
//foreach (var emp in itEmployees)
//{
//    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Age: {emp.Age}, Salary: {emp.Salary}, DeptId: {emp.DeptId}, Skills: {string.Join(", ", emp.Skills)}");
//}
//Console.WriteLine("----------------------------------------------------");

////5. Produce a single flat list of every skill known by any employee.
//Console.WriteLine("5.");
//var allSkills = employees.SelectMany(e => e.Skills).ToList();
//foreach (var skill in allSkills)
//{
//    Console.WriteLine(skill);
//}
//Console.WriteLine("----------------------------------------------------");

////6. Get a list of all unique skills present in the company (no duplicates).
//Console.WriteLine("6.");
//var uniqueSkills = employees.SelectMany(e => e.Skills).Distinct().ToList();
//foreach (var skill in uniqueSkills)
//{
//    Console.WriteLine(skill);
//}
//Console.WriteLine("----------------------------------------------------");

////7. List all skills known by employees earning more than 80,000.
//Console.WriteLine("7.");
//var highEarnerSkills = employees.Where(e => e.Salary > 80000).SelectMany(e => e.Skills).Distinct().ToList();
//foreach (var skill in highEarnerSkills)
//{
//    Console.WriteLine(skill);
//}
//Console.WriteLine("----------------------------------------------------");

////8. Given this mixed list: List<object> mixed = new List<object> { employees[0], "hello", employees[1], 999, employees[2] };  Extract only the actual Employee objects.
//Console.WriteLine("8.");
//var mixed = new List<object> { employees[0], "hello", employees[1], 999, employees[2] };
//var extractedEmployees = mixed.OfType<Employee>().ToList();
//foreach (var emp in extractedEmployees)
//{
//    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Age: {emp.Age}, Salary: {emp.Salary}, DeptId: {emp.DeptId}, Skills: {string.Join(", ", emp.Skills)}");
//}
//Console.WriteLine("----------------------------------------------------");

////9. From the mixed list above, extract only the names of the Employee objects.
//Console.WriteLine("9.");
//var extractedEmployeeNames = mixed.OfType<Employee>().Select(e => e.Name).ToList();
//foreach (var name in extractedEmployeeNames)
//{
//    Console.WriteLine(name);
//}
//Console.WriteLine("----------------------------------------------------");

////10. Return the first three employees in the current order of the list.
//Console.WriteLine("10.");
//var firstThreeEmployees = employees.Take(3).ToList();
//foreach (var emp in firstThreeEmployees)
//{
//    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Age: {emp.Age}, Salary: {emp.Salary}, DeptId: {emp.DeptId}, Skills: {string.Join(", ", emp.Skills)}");
//}
//Console.WriteLine("----------------------------------------------------");

////11. Display the names and salaries of the four highest-paid employees.
//Console.WriteLine("11.");
//var top4Salaries = employees
//    .OrderByDescending(e => e.Salary)
//    .Take(4)
//    .Select(e => new { e.Name, e.Salary })
//    .ToList();
//foreach (var item in top4Salaries)
//{
//    Console.WriteLine($"Name: {item.Name}, Salary: {item.Salary}");
//}
//Console.WriteLine("----------------------------------------------------");

////12. Show employees in positions 3 to 5 (pagination – third, fourth, and fifth employees).
//Console.WriteLine("12.");
//var pagedEmployees = employees.Skip(2).Take(3).ToList();
//foreach (var emp in pagedEmployees)
//{
//    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Age: {emp.Age}, Salary: {emp.Salary}, DeptId: {emp.DeptId}, Skills: {string.Join(", ", emp.Skills)}");
//}
//Console.WriteLine("----------------------------------------------------");

////13. Assuming employees are sorted by increasing Age, return employees until you reach the first one aged 32 or older (do not include that person).
//Console.WriteLine("13.");
//var byAge = employees.OrderBy(e => e.Age)
//    .TakeWhile(e => e.Age < 32)
//    .ToList();
//foreach (var emp in byAge)
//{
//    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Age: {emp.Age}");
//}
//Console.WriteLine("----------------------------------------------------");

////14. Skip all employees earning 80,000 or more, then return the remaining employees.
//Console.WriteLine("14.");
//var below80k = employees
//    .SkipWhile(e => e.Salary >= 80000)
//    .ToList();
//foreach (var emp in below80k)
//{
//    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Salary: {emp.Salary}");
//}
//Console.WriteLine("----------------------------------------------------");

////15. Get the distinct department names that employees belong to.
//Console.WriteLine("15.");
//var deptNames = employees
//    .Select(e => e.DeptId)
//    .Distinct()
//    .Join(departments, id => id, d => d.DeptId, (id, d) => d.DeptName)
//    .ToList();
//foreach (var name in deptNames)
//{
//    Console.WriteLine(name);
//}
//Console.WriteLine("----------------------------------------------------");

////16. Find the first employee whose name starts with the letter 'P'.
//Console.WriteLine("16.");
//var firstP = employees.FirstOrDefault(e => e.Name.StartsWith("P"));
//if (firstP != null)
//    Console.WriteLine($"Id: {firstP.Id}, Name: {firstP.Name}");
//else
//    Console.WriteLine("No employee found.");
//Console.WriteLine("----------------------------------------------------");

////17. Find the first employee in the Finance department; return null if none exists.
//Console.WriteLine("17.");
//var financeDeptId = departments.First(d => d.DeptName == "Finance").DeptId;
//var firstFinance = employees.FirstOrDefault(e => e.DeptId == financeDeptId);
//if (firstFinance != null)
//    Console.WriteLine($"Id: {firstFinance.Id}, Name: {firstFinance.Name}");
//else
//    Console.WriteLine("No employee found.");
//Console.WriteLine("----------------------------------------------------");

////18. Retrieve the employee with Id = 103 (Id is unique).
//Console.WriteLine("18.");
//var emp103 = employees.FirstOrDefault(e => e.Id == 103);
//if (emp103 != null)
//    Console.WriteLine($"Id: {emp103.Id}, Name: {emp103.Name}");
//else
//    Console.WriteLine("No employee found.");
//Console.WriteLine("----------------------------------------------------");

////19. Try to find an employee with Id = 999; return null if not found.
//Console.WriteLine("19.");
//var emp999 = employees.FirstOrDefault(e => e.Id == 999);
//if (emp999 != null)
//    Console.WriteLine($"Id: {emp999.Id}, Name: {emp999.Name}");
//else
//    Console.WriteLine("No employee found.");
//Console.WriteLine("----------------------------------------------------");

////20. Check whether there is at least one employee younger than 28 years.
//Console.WriteLine("20.");
//bool anyYoungerThan28 = employees.Any(e => e.Age < 28);
//Console.WriteLine(anyYoungerThan28);
//Console.WriteLine("----------------------------------------------------");

////21. Check whether any employee in the IT department knows the skill "React".
//Console.WriteLine("21.");
//bool itKnowsReact = employees
//    .Where(e => e.DeptId == itDeptId)
//    .Any(e => e.Skills.Contains("React"));
//Console.WriteLine(itKnowsReact);
//Console.WriteLine("----------------------------------------------------");

////22. Verify whether every employee in the IT department earns more than 70,000.
//Console.WriteLine("22.");
//bool allItAbove70k = employees
//    .Where(e => e.DeptId == itDeptId)
//    .All(e => e.Salary > 70000);
//Console.WriteLine(allItAbove70k);
//Console.WriteLine("----------------------------------------------------");

////23. Verify whether every employee has at least one skill in their Skills list.
//Console.WriteLine("23.");
//bool allHaveSkills = employees.All(e => e.Skills != null && e.Skills.Count > 0);
//Console.WriteLine(allHaveSkills);
//Console.WriteLine("----------------------------------------------------");

////24. Determine whether the skill "Docker" is known by at least one employee's skill list.
//Console.WriteLine("24.");
//bool anyKnowsDocker = employees.Any(e => e.Skills.Contains("Docker"));
//Console.WriteLine(anyKnowsDocker);
//Console.WriteLine("----------------------------------------------------");

////25. Get the names of the three youngest employees who know "C#", sorted from youngest to oldest.
//Console.WriteLine("25.");
//var youngestWithCSharp = employees
//    .Where(e => e.Skills.Contains("C#"))
//    .OrderBy(e => e.Age)
//    .Take(3)
//    .Select(e => e.Name)
//    .ToList();
//foreach (var name in youngestWithCSharp)
//{
//    Console.WriteLine(name);
//}
//Console.WriteLine("----------------------------------------------------");

// 100 Tasks Lab-6
_100QueriesTask obj= new _100QueriesTask();