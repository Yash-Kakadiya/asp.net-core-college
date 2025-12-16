using LinQDemo;
using System;
using System.Collections.Generic;
using System.Linq;

var people = Person.GetPeople();
Console.WriteLine("All Persons:");
foreach (var person in people)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

// name,age,deptartment,city
//Q1
Console.WriteLine("Select Persons whose age is less than 30");
var greaterThan20 = people.Where(p => p.Age < 30).ToList();
foreach (var person in greaterThan20)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q2
Console.WriteLine("Select Persons whose age is greater than 30");
var greaterThan30 = people.Where(p => p.Age > 30).ToList();
foreach (var person in greaterThan30)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q3
var personFromITDept = people.Where(p => p.Department.ToUpper() == "IT");
Console.WriteLine("Select Persons from IT department");
foreach (var person in personFromITDept)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q4
var personFromITDeptNewYorkCity = people.Where(p => p.City.ToLower() == "new york");
Console.WriteLine("Select Persons from New York city");
foreach (var person in personFromITDept)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q5:
var personNameStartsWithA = people.Where(p => p.Name.ToLower().StartsWith("a"));
Console.WriteLine("Select Persons whose name starts with 'A'");
foreach (var person in personNameStartsWithA)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q6:
var personNameEndsWithE = people.Where(p => p.Name.ToLower().EndsWith("e"));
Console.WriteLine("Select Persons whose name ends with 'e'");
foreach (var person in personNameEndsWithE)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q6:
var personNameContainsEndA = people.Where(p => p.Name.ToLower().Contains("a"));
Console.WriteLine("Select Persons whose name contains with 'a'");
foreach (var person in personNameContainsEndA)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q7:
var personNameStartsWithAAndAgeMoreThan20 = people.Where(p => p.Name.ToLower().StartsWith("a") && p.Age > 20);
Console.WriteLine("Select Persons whose name starts with 'A' and age more than 20");
foreach (var person in personNameStartsWithAAndAgeMoreThan20)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q8:
var personNameStartsWithAOrAgeMoreThan20 = people.Where(p => p.Name.ToLower().StartsWith("a") || p.Age > 20);
Console.WriteLine("Select Persons whose name starts with 'A' or age more than 20");
foreach (var person in personNameStartsWithAOrAgeMoreThan20)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q9:
var personAgeBetween20and30 = people.Where(p => p.Age >= 20 && p.Age <= 30);
Console.WriteLine("Select Persons whose age between 20 and 30");
foreach (var person in personAgeBetween20and30)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q10:
var personAgeMax = people.Max(p => p.Age);
Console.WriteLine($"Max Age => {personAgeMax}");
Console.WriteLine("------------------------------------");

//Q11:
var containsEAndAge30 = people.Where(p => p.Name.Contains("e") && p.Age == 30).Select(p => new { p.Name, p.Age });
Console.WriteLine("Select Persons whose name contains 'e' and age is 30");
foreach (var person in containsEAndAge30)
{
    Console.WriteLine($"{person.Name}, {person.Age}");
}
Console.WriteLine("------------------------------------");

//Q12:
var orderbyAgeThenName = people.OrderBy(p => p.Age).ThenBy(p => p.Name);
Console.WriteLine("Persons ordered by Age then by Name");
foreach (var person in orderbyAgeThenName)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q13:
var firstpersonWithAgeGreaterThan20 = people.Where(p => p.Age > 20).FirstOrDefault();
Console.WriteLine("First person whose age is greater than 20");
Console.WriteLine($"{firstpersonWithAgeGreaterThan20.Name}, {firstpersonWithAgeGreaterThan20.Age}, {firstpersonWithAgeGreaterThan20.Department}, {firstpersonWithAgeGreaterThan20.City}");

//Q14:
var skipFirstThreeTakeNextThree = people.Skip(3).Take(3).ToList();
Console.WriteLine("Skip first 3 persons and take next 3 persons");
foreach (var person in skipFirstThreeTakeNextThree)
{
    Console.WriteLine($"{person.Name}, {person.Age}, {person.Department}, {person.City}");
}
Console.WriteLine("------------------------------------");

//Q15: 
var groupedByBranchAndSem = people.GroupBy(p => new { p.Age, p.City }).Select(p => new { Age = p.Key.Age, City = p.Key.City, Peoples = p.ToList() });
Console.WriteLine("Persons grouped by Age and City");
foreach (var person in groupedByBranchAndSem)
{
    Console.WriteLine($"{person.Age}, {person.City}, {string.Join(", ", person.Peoples.Select(p => p.Name))}");
}
Console.WriteLine("------------------------------------");