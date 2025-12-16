namespace LinQLab
{
    internal class _100QueriesTask
    {
        public List<Student> students = new List<Student>
            {
                new Student { Rno = 1, Name = "¥@$# Kakadiya", Branch = "CE", Sem = 3, CPI = 8.2, Age = 19 },
                new Student { Rno = 2, Name = "Priya", Branch = "IT", Sem = 5, CPI = 9.1, Age = 21 },
                new Student { Rno = 3, Name = "Rahul", Branch = "CE", Sem = 1, CPI = 7.5, Age = 18 },
                new Student { Rno = 4, Name = "Sneha", Branch = "ME", Sem = 7, CPI = 8.8, Age = 22 },
                new Student { Rno = 5, Name = "Karan", Branch = "IT", Sem = 3, CPI = 6.9, Age = 20 }
            };
        public List<Course> courses = new List<Course>
            {
                new Course { Rno = 1, CourseName = "DBMS", Credits = 4 },
                new Course { Rno = 1, CourseName = "C#", Credits = 3 },
                new Course { Rno = 2, CourseName = "Java", Credits = 4 },
                new Course { Rno = 3, CourseName = "Python", Credits = 3 },
                new Course { Rno = 5, CourseName = "AI", Credits = 5 }
            };
        public _100QueriesTask()
        {
            RunAllQueries();
        }
        public void RunAllQueries()
        {
            //# **SECTION 1 — FILTERING (Where) — 15 Questions**

            //1.Get all CE branch students.
            Console.WriteLine("1. Get all CE branch students.:");
            var ceStudents = students.Where(s => s.Branch == "CE").ToList();
            foreach (var student in ceStudents)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //2.Students having CPI > 8.
            Console.WriteLine("\n2. Students having CPI > 8.:");
            var highCPIStudents = students.Where(s => s.CPI > 8).ToList();
            foreach (var student in highCPIStudents)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //3.Students older than 20.
            Console.WriteLine("\n3. Students older than 20.:");
            var olderThan20 = students.Where(s => s.Age > 20).ToList();
            foreach (var student in olderThan20)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //4.Students in Semester 3.
            Console.WriteLine("\n4. Students in Semester 3.:");
            var sem3Students = students.Where(s => s.Sem == 3).ToList();
            foreach (var student in sem3Students)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //5.CPI between 7 and 9.
            Console.WriteLine("\n5. CPI between 7 and 9.:");
            var cpiBetween7And9 = students.Where(s => s.CPI >= 7 && s.CPI <= 9).ToList();
            foreach (var student in cpiBetween7And9)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //6.Name starting with 'A'.
            Console.WriteLine("\n6. Name starting with 'A'.:");
            var nameStartsWithA = students.Where(s => s.Name.StartsWith("A")).ToList();
            foreach (var student in nameStartsWithA)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //7.Branch = IT AND Sem = 3.
            Console.WriteLine("\n7. Branch = IT AND Sem = 3.:");
            var itSem3 = students.Where(s => s.Branch == "IT" && s.Sem == 3).ToList();
            foreach (var student in itSem3)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //8.Age < 20 OR CPI > 8.
            Console.WriteLine("\n8. Age < 20 OR CPI > 8.:");
            var ageLessThan20OrCPIGreaterThan8 = students.Where(s => s.Age < 20 || s.CPI > 8).ToList();
            foreach (var student in ageLessThan20OrCPIGreaterThan8)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //9.Names containing 'a'.
            Console.WriteLine("\n9. Names containing 'a'.:");
            var namesContainingA = students.Where(s => s.Name.Contains("a")).ToList();
            foreach (var student in namesContainingA)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //10.Students NOT in CE.
            Console.WriteLine("\n10. Students NOT in CE.:");
            var notCE = students.Where(s => s.Branch != "CE").ToList();
            foreach (var student in notCE)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //11.Sem in { 1,3,5}.
            Console.WriteLine("\n11. Sem in {1,3,5}.:");
            var semIn135 = students.Where(s => new[] { 1, 3, 5 }.Contains(s.Sem)).ToList();
            foreach (var student in semIn135)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //12.Students whose CPI is a whole number.
            Console.WriteLine("\n12. Students whose CPI is a whole number.:");
            var cpiWholeNumber = students.Where(s => s.CPI == Math.Floor(s.CPI)).ToList();
            foreach (var student in cpiWholeNumber)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //13.Students with even Roll No.
            Console.WriteLine("\n13. Students with even Roll No.:");
            var evenRollNo = students.Where(s => s.Rno % 2 == 0).ToList();
            foreach (var student in evenRollNo)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //14.Students whose age is between 18 and 21.
            Console.WriteLine("\n14. Students whose age is between 18 and 21.:");
            var ageBetween18And21 = students.Where(s => s.Age >= 18 && s.Age <= 21).ToList();
            foreach (var student in ageBetween18And21)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }

            //15.Students having name length > 4.
            Console.WriteLine("\n15. Students having name length > 4.:");
            var nameLengthGreaterThan4 = students.Where(s => s.Name.Length > 4).ToList();
            foreach (var student in nameLengthGreaterThan4)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}, Branch: {student.Branch}, Sem: {student.Sem}, CPI: {student.CPI}, Age: {student.Age}");
            }


            //---

            //# **SECTION 2 — SELECT (Projection) — 10 Questions**

            //16.Select only names.
            Console.WriteLine("\n16. Select only names.:");
            var names = students.Select(s => s.Name).ToList();
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }

            //17.Select Name + CPI.
            Console.WriteLine("\n17. Select Name + CPI.:");
            var nameCPI = students.Select(s => new { s.Name, s.CPI }).ToList();
            foreach (var item in nameCPI)
            {
                Console.WriteLine($"Name: {item.Name}, CPI: {item.CPI}");
            }

            //18.Select Roll No + Branch.
            Console.WriteLine("\n18. Select Roll No + Branch.:");
            var rnoBranch = students.Select(s => new { s.Rno, s.Branch }).ToList();
            foreach (var item in rnoBranch)
            {
                Console.WriteLine($"Rno: {item.Rno}, Branch: {item.Branch}");
            }

            //19.Select anonymous type: Name, Sem, Age.
            Console.WriteLine("\n19. Select anonymous type: Name, Sem, Age.:");
            var nameSemAge = students.Select(s => new { s.Name, s.Sem, s.Age }).ToList();
            foreach (var item in nameSemAge)
            {
                Console.WriteLine($"Name: {item.Name}, Sem: {item.Sem}, Age: {item.Age}");
            }

            //20.Create 'FullInfo' string(e.g., "Name (Branch)").
            Console.WriteLine("\n20. Create 'FullInfo' string (e.g., \"Name (Branch)\").:");
            var fullInfo = students.Select(s => $"{s.Name} ({s.Branch})").ToList();
            foreach (var info in fullInfo)
            {
                Console.WriteLine(info);
            }

            //21.Project all to CPI only.
            Console.WriteLine("\n21. Project all to CPI only.:");
            var cpiOnly = students.Select(s => s.CPI).ToList();
            foreach (var cpi in cpiOnly)
            {
                Console.WriteLine(cpi);
            }

            //22.Select Name in lowercase.
            Console.WriteLine("\n22. Select Name in lowercase.:");
            var nameLowercase = students.Select(s => s.Name.ToLower()).ToList();
            foreach (var name in nameLowercase)
            {
                Console.WriteLine(name);
            }

            //23.Select Name + Status based on CPI(Good / Average).
            Console.WriteLine("\n23. Select Name + Status based on CPI (Good / Average).:");
            var nameStatus = students.Select(s => new { s.Name, Status = s.CPI >= 8 ? "Good" : "Average" }).ToList();
            foreach (var item in nameStatus)
            {
                Console.WriteLine($"Name: {item.Name}, Status: {item.Status}");
            }

            //24.Extract only distinct branches.
            Console.WriteLine("\n24. Extract only distinct branches.:");
            var distinctBranches = students.Select(s => s.Branch).Distinct().ToList();
            foreach (var branch in distinctBranches)
            {
                Console.WriteLine(branch);
            }

            //25.Convert student to “DTO” format(Rno, Name).
            Console.WriteLine("\n25. Convert student to \"DTO\" format (Rno, Name).:");
            var dto = students.Select(s => new { s.Rno, s.Name }).ToList();
            foreach (var item in dto)
            {
                Console.WriteLine($"Rno: {item.Rno}, Name: {item.Name}");
            }


            //---

            //# **SECTION 3 — SORTING (OrderBy) — 10 Questions**

            //26.Sort names alphabetically.
            Console.WriteLine("\n26. Sort names alphabetically.:");
            var sortedNames = students.OrderBy(s => s.Name).ToList();
            foreach (var student in sortedNames)
            {
                Console.WriteLine($"Name: {student.Name}");
            }

            //27.Sort by CPI descending.
            Console.WriteLine("\n27. Sort by CPI descending.:");
            var sortedByCPIDesc = students.OrderByDescending(s => s.CPI).ToList();
            foreach (var student in sortedByCPIDesc)
            {
                Console.WriteLine($"Name: {student.Name}, CPI: {student.CPI}");
            }

            //28.Sort by Sem, then Name.
            Console.WriteLine("\n28. Sort by Sem, then Name.:");
            var sortedBySemThenName = students.OrderBy(s => s.Sem).ThenBy(s => s.Name).ToList();
            foreach (var student in sortedBySemThenName)
            {
                Console.WriteLine($"Sem: {student.Sem}, Name: {student.Name}");
            }

            //29.Sort by Age, then CPI desc.
            Console.WriteLine("\n29. Sort by Age, then CPI desc.:");
            var sortedByAgeThenCPIDesc = students.OrderBy(s => s.Age).ThenByDescending(s => s.CPI).ToList();
            foreach (var student in sortedByAgeThenCPIDesc)
            {
                Console.WriteLine($"Age: {student.Age}, CPI: {student.CPI}, Name: {student.Name}");
            }

            //30.Sort by Branch.
            Console.WriteLine("\n30. Sort by Branch.:");
            var sortedByBranch = students.OrderBy(s => s.Branch).ToList();
            foreach (var student in sortedByBranch)
            {
                Console.WriteLine($"Branch: {student.Branch}, Name: {student.Name}");
            }

            //31.Sort by Name length.
            Console.WriteLine("\n31. Sort by Name length.:");
            var sortedByNameLength = students.OrderBy(s => s.Name.Length).ToList();
            foreach (var student in sortedByNameLength)
            {
                Console.WriteLine($"Name: {student.Name}, Length: {student.Name.Length}");
            }

            //32.Sort by Sem DESC.
            Console.WriteLine("\n32. Sort by Sem DESC.:");
            var sortedBySemDesc = students.OrderByDescending(s => s.Sem).ToList();
            foreach (var student in sortedBySemDesc)
            {
                Console.WriteLine($"Sem: {student.Sem}, Name: {student.Name}");
            }

            //33.Sort by CPI then Age.
            Console.WriteLine("\n33. Sort by CPI then Age.:");
            var sortedByCPIThenAge = students.OrderBy(s => s.CPI).ThenBy(s => s.Age).ToList();
            foreach (var student in sortedByCPIThenAge)
            {
                Console.WriteLine($"CPI: {student.CPI}, Age: {student.Age}, Name: {student.Name}");
            }

            //34.Sort by Rno descending.
            Console.WriteLine("\n34. Sort by Rno descending.:");
            var sortedByRnoDesc = students.OrderByDescending(s => s.Rno).ToList();
            foreach (var student in sortedByRnoDesc)
            {
                Console.WriteLine($"Rno: {student.Rno}, Name: {student.Name}");
            }

            //35.Sort by Branch then Sem.
            Console.WriteLine("\n35. Sort by Branch then Sem.:");
            var sortedByBranchThenSem = students.OrderBy(s => s.Branch).ThenBy(s => s.Sem).ToList();
            foreach (var student in sortedByBranchThenSem)
            {
                Console.WriteLine($"Branch: {student.Branch}, Sem: {student.Sem}, Name: {student.Name}");
            }


            //---

            //# **SECTION 4 — AGGREGATION — 10 Questions**

            //36.Count total students.
            Console.WriteLine("\n36. Count total students.:");
            var totalStudents = students.Count();
            Console.WriteLine($"Total Students: {totalStudents}");

            //37.Count CE students.
            Console.WriteLine("\n37. Count CE students.:");
            var ceCount = students.Count(s => s.Branch == "CE");
            Console.WriteLine($"CE Students: {ceCount}");

            //38.Max CPI.
            Console.WriteLine("\n38. Max CPI.:");
            var maxCPI = students.Max(s => s.CPI);
            Console.WriteLine($"Max CPI: {maxCPI}");

            //39.Min CPI.
            Console.WriteLine("\n39. Min CPI.:");
            var minCPI = students.Min(s => s.CPI);
            Console.WriteLine($"Min CPI: {minCPI}");

            //40.Average CPI.
            Console.WriteLine("\n40. Average CPI.:");
            var avgCPI = students.Average(s => s.CPI);
            Console.WriteLine($"Average CPI: {avgCPI}");

            //41.Total credits for Rno = 1.
            Console.WriteLine("\n41. Total credits for Rno = 1.:");
            var totalCreditsRno1 = courses.Where(c => c.Rno == 1).Sum(c => c.Credits);
            Console.WriteLine($"Total Credits for Rno 1: {totalCreditsRno1}");

            //42.Oldest student's age.
            Console.WriteLine("\n42. Oldest student's age.:");
            var oldestAge = students.Max(s => s.Age);
            Console.WriteLine($"Oldest Student Age: {oldestAge}");

            //43.Youngest student's age.
            Console.WriteLine("\n43. Youngest student's age.:");
            var youngestAge = students.Min(s => s.Age);
            Console.WriteLine($"Youngest Student Age: {youngestAge}");

            //44.Highest Sem.
            Console.WriteLine("\n44. Highest Sem.:");
            var highestSem = students.Max(s => s.Sem);
            Console.WriteLine($"Highest Sem: {highestSem}");

            //45.Sum of all credits.
            Console.WriteLine("\n45. Sum of all credits.:");
            var totalCredits = courses.Sum(c => c.Credits);
            Console.WriteLine($"Total Credits: {totalCredits}");


            //---

            //# **SECTION 5 — ELEMENT OPERATIONS — 10 Questions**

            //46.Get first student.
            Console.WriteLine("\n46. Get first student.:");
            var firstStudent = students.First();
            Console.WriteLine($"Rno: {firstStudent.Rno}, Name: {firstStudent.Name}");

            //47.First student with CPI > 9.
            Console.WriteLine("\n47. First student with CPI > 9.:");
            var firstCPIGreaterThan9 = students.First(s => s.CPI > 9);
            Console.WriteLine($"Rno: {firstCPIGreaterThan9.Rno}, Name: {firstCPIGreaterThan9.Name}, CPI: {firstCPIGreaterThan9.CPI}");

            //48.Last student.
            Console.WriteLine("\n48. Last student.:");
            var lastStudent = students.Last();
            Console.WriteLine($"Rno: {lastStudent.Rno}, Name: {lastStudent.Name}");

            //49.Get student at index 2.
            Console.WriteLine("\n49. Get student at index 2.:");
            var studentAtIndex2 = students.ElementAt(2);
            Console.WriteLine($"Rno: {studentAtIndex2.Rno}, Name: {studentAtIndex2.Name}");

            //50.Single student with Rno = 3.
            Console.WriteLine("\n50. Single student with Rno = 3.:");
            var singleRno3 = students.Single(s => s.Rno == 3);
            Console.WriteLine($"Rno: {singleRno3.Rno}, Name: {singleRno3.Name}");

            //51.Safe single(e.g., Rno = 10).
            Console.WriteLine("\n51. Safe single (e.g., Rno = 10).:");
            var safeSingleRno10 = students.SingleOrDefault(s => s.Rno == 10);
            if (safeSingleRno10 != null)
                Console.WriteLine($"Rno: {safeSingleRno10.Rno}, Name: {safeSingleRno10.Name}");
            else
                Console.WriteLine("Student with Rno 10 not found.");

            //52.First IT student.
            Console.WriteLine("\n52. First IT student.:");
            var firstITStudent = students.First(s => s.Branch == "IT");
            Console.WriteLine($"Rno: {firstITStudent.Rno}, Name: {firstITStudent.Name}");
            //53.Last CE student.
            Console.WriteLine("\n53. Last CE student.:");
            var lastCEStudent = students.Last(s => s.Branch == "CE");
            Console.WriteLine($"Rno: {lastCEStudent.Rno}, Name: {lastCEStudent.Name}");

            //54.First student older than 18.
            Console.WriteLine("\n54. First student older than 18.:");
            var firstOlderThan18 = students.First(s => s.Age > 18);
            Console.WriteLine($"Rno: {firstOlderThan18.Rno}, Name: {firstOlderThan18.Name}, Age: {firstOlderThan18.Age}");

            //55.Element at index 0.
            Console.WriteLine("\n55. Element at index 0.:");
            var elementAtIndex0 = students.ElementAt(0);
            Console.WriteLine($"Rno: {elementAtIndex0.Rno}, Name: {elementAtIndex0.Name}");


            //---

            //# **SECTION 6 — ANY / ALL — 10 Questions**

            //56.Any CE students ?
            Console.WriteLine("\n56. Any CE students?:");
            var anyCE = students.Any(s => s.Branch == "CE");
            Console.WriteLine($"Any CE students: {anyCE}");

            //57.All students older than 17 ?
            Console.WriteLine("\n57. All students older than 17?:");
            var allOlderThan17 = students.All(s => s.Age > 17);
            Console.WriteLine($"All students older than 17: {allOlderThan17}");

            //58.Any CPI > 9 ?
            Console.WriteLine("\n58. Any CPI > 9?:");
            var anyCPIGreaterThan9 = students.Any(s => s.CPI > 9);
            Console.WriteLine($"Any CPI > 9: {anyCPIGreaterThan9}");

            //59.All semesters are > 0 ?
            Console.WriteLine("\n59. All semesters are > 0?:");
            var allSemGreaterThan0 = students.All(s => s.Sem > 0);
            Console.WriteLine($"All semesters > 0: {allSemGreaterThan0}");

            //60.Any student with name length > 6 ?
            Console.WriteLine("\n60. Any student with name length > 6?:");
            var anyNameLengthGreaterThan6 = students.Any(s => s.Name.Length > 6);
            Console.WriteLine($"Any name length > 6: {anyNameLengthGreaterThan6}");

            //61.All belong to CE ?
            Console.WriteLine("\n61. All belong to CE?:");
            var allCE = students.All(s => s.Branch == "CE");
            Console.WriteLine($"All belong to CE: {allCE}");

            //62.Any course with credits > 4 ?
            Console.WriteLine("\n62. Any course with credits > 4?:");
            var anyCreditsGreaterThan4 = courses.Any(c => c.Credits > 4);
            Console.WriteLine($"Any course with credits > 4: {anyCreditsGreaterThan4}");

            //63.All credits > 2 ?
            Console.WriteLine("\n63. All credits > 2?:");
            var allCreditsGreaterThan2 = courses.All(c => c.Credits > 2);
            Console.WriteLine($"All credits > 2: {allCreditsGreaterThan2}");

            //64.Any course named "Java" ?
            Console.WriteLine("\n64. Any course named \"Java\"?:");
            var anyCourseJava = courses.Any(c => c.CourseName == "Java");
            Console.WriteLine($"Any course named Java: {anyCourseJava}");

            //65.Any student younger than 18 ?
            Console.WriteLine("\n65. Any student younger than 18?:");
            var anyYoungerThan18 = students.Any(s => s.Age < 18);
            Console.WriteLine($"Any student younger than 18: {anyYoungerThan18}");


            //---

            //# **SECTION 7 — GROUPING — 10 Questions**

            //66.Group students by branch.
            Console.WriteLine("\n66. Group students by branch.:");
            var groupByBranch = students.GroupBy(s => s.Branch).ToList();
            foreach (var group in groupByBranch)
            {
                Console.WriteLine($"Branch: {group.Key}");
                foreach (var student in group)
                {
                    Console.WriteLine($"  Name: {student.Name}");
                }
            }

            //67.Group by Semester.
            Console.WriteLine("\n67. Group by Semester.:");
            var groupBySem = students.GroupBy(s => s.Sem).ToList();
            foreach (var group in groupBySem)
            {
                Console.WriteLine($"Semester: {group.Key}");
                foreach (var student in group)
                {
                    Console.WriteLine($"  Name: {student.Name}");
                }
            }

            //68.Group by Age.
            Console.WriteLine("\n68. Group by Age.:");
            var groupByAge = students.GroupBy(s => s.Age).ToList();
            foreach (var group in groupByAge)
            {
                Console.WriteLine($"Age: {group.Key}");
                foreach (var student in group)
                {
                    Console.WriteLine($"  Name: {student.Name}");
                }
            }

            //69.Group by CPI category(High / Low).
            Console.WriteLine("\n69. Group by CPI category (High / Low).:");
            var groupByCPICategory = students.GroupBy(s => s.CPI >= 8 ? "High" : "Low").ToList();
            foreach (var group in groupByCPICategory)
            {
                Console.WriteLine($"CPI Category: {group.Key}");
                foreach (var student in group)
                {
                    Console.WriteLine($"  Name: {student.Name}, CPI: {student.CPI}");
                }
            }

            //70.Group courses by Rno.
            Console.WriteLine("\n70. Group courses by Rno.:");
            var groupCoursesByRno = courses.GroupBy(c => c.Rno).ToList();
            foreach (var group in groupCoursesByRno)
            {
                Console.WriteLine($"Rno: {group.Key}");
                foreach (var course in group)
                {
                    Console.WriteLine($"  Course: {course.CourseName}, Credits: {course.Credits}");
                }
            }

            //71.Group students by first letter of name.
            Console.WriteLine("\n71. Group students by first letter of name.:");
            var groupByFirstLetter = students.GroupBy(s => s.Name[0]).ToList();
            foreach (var group in groupByFirstLetter)
            {
                Console.WriteLine($"First Letter: {group.Key}");
                foreach (var student in group)
                {
                    Console.WriteLine($"  Name: {student.Name}");
                }
            }

            //72.Group by Branch then Sem.
            Console.WriteLine("\n72. Group by Branch then Sem.:");
            var groupByBranchThenSem = students.GroupBy(s => new { s.Branch, s.Sem }).ToList();
            foreach (var group in groupByBranchThenSem)
            {
                Console.WriteLine($"Branch: {group.Key.Branch}, Sem: {group.Key.Sem}");
                foreach (var student in group)
                {
                    Console.WriteLine($"  Name: {student.Name}");
                }
            }

            //73.Group by age range(Teen / Adult).
            Console.WriteLine("\n73. Group by age range (Teen / Adult).:");
            var groupByAgeRange = students.GroupBy(s => s.Age < 20 ? "Teen" : "Adult").ToList();
            foreach (var group in groupByAgeRange)
            {
                Console.WriteLine($"Age Range: {group.Key}");
                foreach (var student in group)
                {
                    Console.WriteLine($"  Name: {student.Name}, Age: {student.Age}");
                }
            }

            //74.Group courses by Credits.
            Console.WriteLine("\n74. Group courses by Credits.:");
            var groupByCredits = courses.GroupBy(c => c.Credits).ToList();
            foreach (var group in groupByCredits)
            {
                Console.WriteLine($"Credits: {group.Key}");
                foreach (var course in group)
                {
                    Console.WriteLine($"  Course: {course.CourseName}");
                }
            }

            //75.Group students by CPI rounded.
            Console.WriteLine("\n75. Group students by CPI rounded.:");
            var groupByCPIRounded = students.GroupBy(s => Math.Round(s.CPI)).ToList();
            foreach (var group in groupByCPIRounded)
            {
                Console.WriteLine($"CPI (Rounded): {group.Key}");
                foreach (var student in group)
                {
                    Console.WriteLine($"  Name: {student.Name}, CPI: {student.CPI}");
                }
            }


            //---

            //# **SECTION 8 — JOIN — 10 Questions**

            //76.Inner Join students + courses.
            Console.WriteLine("\n76. Inner Join students + courses.:");
            var innerJoin = students.Join(courses, s => s.Rno, c => c.Rno, (s, c) => new { s.Name, c.CourseName }).ToList();
            foreach (var item in innerJoin)
            {
                Console.WriteLine($"Name: {item.Name}, Course: {item.CourseName}");
            }

            //77.Join to get total credits per student.
            Console.WriteLine("\n77. Join to get total credits per student.:");
            var totalCreditsPerStudent = students.GroupJoin(courses, s => s.Rno, c => c.Rno, (s, c) => new { s.Name, TotalCredits = c.Sum(x => x.Credits) }).ToList();
            foreach (var item in totalCreditsPerStudent)
            {
                Console.WriteLine($"Name: {item.Name}, Total Credits: {item.TotalCredits}");
            }

            //78.Students with courses(Name, CourseName, Credits).
            Console.WriteLine("\n78. Students with courses (Name, CourseName, Credits).:");
            var studentsWithCourses = students.Join(courses, s => s.Rno, c => c.Rno, (s, c) => new { s.Name, c.CourseName, c.Credits }).ToList();
            foreach (var item in studentsWithCourses)
            {
                Console.WriteLine($"Name: {item.Name}, Course: {item.CourseName}, Credits: {item.Credits}");
            }

            //79.Left join to include students without courses.
            Console.WriteLine("\n79. Left join to include students without courses.:");
            var leftJoin = students.GroupJoin(courses, s => s.Rno, c => c.Rno, (s, c) => new { s.Name, Courses = c }).SelectMany(x => x.Courses.DefaultIfEmpty(), (s, c) => new { s.Name, CourseName = c?.CourseName ?? "No Course" }).ToList();
            foreach (var item in leftJoin)
            {
                Console.WriteLine($"Name: {item.Name}, Course: {item.CourseName}");
            }

            //80.List of all distinct courses taken.
            Console.WriteLine("\n80. List of all distinct courses taken.:");
            var distinctCourses = courses.Select(c => c.CourseName).Distinct().ToList();
            foreach (var course in distinctCourses)
            {
                Console.WriteLine($"Course: {course}");
            }

            //81.Students having more than 1 course.
            Console.WriteLine("\n81. Students having more than 1 course.:");
            var studentsWithMoreThan1Course = students.GroupJoin(courses, s => s.Rno, c => c.Rno, (s, c) => new { s.Name, CourseCount = c.Count() }).Where(x => x.CourseCount > 1).ToList();
            foreach (var item in studentsWithMoreThan1Course)
            {
                Console.WriteLine($"Name: {item.Name}, Course Count: {item.CourseCount}");
            }

            //82.Join and order by credits.
            Console.WriteLine("\n82. Join and order by credits.:");
            var joinOrderByCredits = students.Join(courses, s => s.Rno, c => c.Rno, (s, c) => new { s.Name, c.CourseName, c.Credits }).OrderBy(x => x.Credits).ToList();
            foreach (var item in joinOrderByCredits)
            {
                Console.WriteLine($"Name: {item.Name}, Course: {item.CourseName}, Credits: {item.Credits}");
            }

            //83.Students of IT with courses.
            Console.WriteLine("\n83. Students of IT with courses.:");
            var itStudentsWithCourses = students.Where(s => s.Branch == "IT").Join(courses, s => s.Rno, c => c.Rno, (s, c) => new { s.Name, c.CourseName }).ToList();
            foreach (var item in itStudentsWithCourses)
            {
                Console.WriteLine($"Name: {item.Name}, Course: {item.CourseName}");
            }

            //84.Students who have no course.
            Console.WriteLine("\n84. Students who have no course.:");
            var studentsWithNoCourse = students.Where(s => !courses.Any(c => c.Rno == s.Rno)).ToList();
            foreach (var student in studentsWithNoCourse)
            {
                Console.WriteLine($"Name: {student.Name}");
            }

            //85.Students + number of courses.
            Console.WriteLine("\n85. Students + number of courses.:");
            var studentsWithCourseCount = students.GroupJoin(courses, s => s.Rno, c => c.Rno, (s, c) => new { s.Name, CourseCount = c.Count() }).ToList();
            foreach (var item in studentsWithCourseCount)
            {
                Console.WriteLine($"Name: {item.Name}, Course Count: {item.CourseCount}");
            }


            //---

            //# **SECTION 9 — SET OPERATIONS — 5 Questions**

            //86.Distinct branches.
            Console.WriteLine("\n86. Distinct branches.:");
            var distinctBranchesSet = students.Select(s => s.Branch).Distinct().ToList();
            foreach (var branch in distinctBranchesSet)
            {
                Console.WriteLine($"Branch: {branch}");
            }

            //87.Students in CE or IT(Union).
            Console.WriteLine("\n87. Students in CE or IT (Union).:");
            var ceStudentsSet = students.Where(s => s.Branch == "CE").ToList();
            var itStudentsSet = students.Where(s => s.Branch == "IT").ToList();
            var unionCEIT = ceStudentsSet.Union(itStudentsSet).ToList();
            foreach (var student in unionCEIT)
            {
                Console.WriteLine($"Name: {student.Name}, Branch: {student.Branch}");
            }

            //88.Students in CE but not IT(Except).
            Console.WriteLine("\n88. Students in CE but not IT (Except).:");
            var exceptCEIT = ceStudentsSet.Except(itStudentsSet).ToList();
            foreach (var student in exceptCEIT)
            {
                Console.WriteLine($"Name: {student.Name}, Branch: {student.Branch}");
            }

            //89.Common semesters between CE and IT students(Intersect).
            Console.WriteLine("\n89. Common semesters between CE and IT students (Intersect).:");
            var ceSemesters = students.Where(s => s.Branch == "CE").Select(s => s.Sem).ToList();
            var itSemesters = students.Where(s => s.Branch == "IT").Select(s => s.Sem).ToList();
            var intersectSemesters = ceSemesters.Intersect(itSemesters).ToList();
            foreach (var sem in intersectSemesters)
            {
                Console.WriteLine($"Semester: {sem}");
            }

            //90.Get all courses that have credits other than 3.
            Console.WriteLine("\n90. Get all courses that have credits other than 3.:");
            var coursesNotCredits3 = courses.Where(c => c.Credits != 3).ToList();
            foreach (var course in coursesNotCredits3)
            {
                Console.WriteLine($"Course: {course.CourseName}, Credits: {course.Credits}");
            }


            //---

            //# **SECTION 10 — CONVERSION (ToList, Dictionary) — 5 Questions**

            //91.Convert to list.
            Console.WriteLine("\n91. Convert to list.:");
            var studentsList = students.ToList();
            Console.WriteLine($"Total students in list: {studentsList.Count}");

            //92.Convert to dictionary(Rno → Name).
            Console.WriteLine("\n92. Convert to dictionary (Rno → Name).:");
            var studentsDictionary = students.ToDictionary(s => s.Rno, s => s.Name);
            foreach (var kvp in studentsDictionary)
            {
                Console.WriteLine($"Rno: {kvp.Key}, Name: {kvp.Value}");
            }

            //93.Convert projected type to array(Names → string[]).
            Console.WriteLine("\n93. Convert projected type to array (Names → string[]).:");
            var namesArray = students.Select(s => s.Name).ToArray();
            foreach (var name in namesArray)
            {
                Console.WriteLine($"Name: {name}");
            }

            //94.Create lookup(Rno → Courses).

            //95.Convert branch list to HashSet.


            //---

            //# **SECTION 11 — BASIC / MIXED — 5 Questions**

            //96.Top 2 highest CPI students.
            Console.WriteLine("\n96. Top 2 highest CPI students.:");
            var top2CPI = students.OrderByDescending(s => s.CPI).Take(2).ToList();
            foreach (var student in top2CPI)
            {
                Console.WriteLine($"Name: {student.Name}, CPI: {student.CPI}");
            }

            //97.Skip first 2, take next 2.
            Console.WriteLine("\n97. Skip first 2, take next 2.:");
            var skipTake = students.Skip(2).Take(2).ToList();
            foreach (var student in skipTake)
            {
                Console.WriteLine($"Name: {student.Name}");
            }

            //98.Find student with max CPI(full object).
            Console.WriteLine("\n98. Find student with max CPI (full object).:");
            var maxCPIStudent = students.OrderByDescending(s => s.CPI).First();
            Console.WriteLine($"Rno: {maxCPIStudent.Rno}, Name: {maxCPIStudent.Name}, Branch: {maxCPIStudent.Branch}, Sem: {maxCPIStudent.Sem}, CPI: {maxCPIStudent.CPI}, Age: {maxCPIStudent.Age}");

            //99.Get students with course count + sort by count.
            Console.WriteLine("\n99. Get students with course count + sort by count.:");
            var studentsWithCourseCountSorted = students.GroupJoin(courses, s => s.Rno, c => c.Rno, (s, c) => new { s.Name, CourseCount = c.Count() }).OrderBy(x => x.CourseCount).ToList();
            foreach (var item in studentsWithCourseCountSorted)
            {
                Console.WriteLine($"Name: {item.Name}, Course Count: {item.CourseCount}");
            }

            //100.Show students grouped by Branch and sorted by CPI.
            Console.WriteLine("\n100. Show students grouped by Branch and sorted by CPI.:");
            var groupedByBranchSortedByCPI = students.OrderBy(s => s.CPI).GroupBy(s => s.Branch).ToList();
            foreach (var group in groupedByBranchSortedByCPI)
            {
                Console.WriteLine($"Branch: {group.Key}");
                foreach (var student in group)
                {
                    Console.WriteLine($"  Name: {student.Name}, CPI: {student.CPI}");
                }
            }


            //---

            //# **ADVANCED LINQ QUERIES**

            //## **Deferred Execution — 5 Questions**

            //101.Show deferred execution: create a query, modify the source, then enumerate to show the change.
            //102.Convert deferred to immediate execution(force materialization).
            //103.Deferred + modifying list: show how changing an element affects query results.
            //104.Illustrate the multiple - enumeration problem and how to avoid it.
            //105.Deferred with filtering later: create a deferred query then apply further filters before enumeration.

            //---

            //## **Let Keyword — 3 Questions**

            //106.Use `let` to calculate squared CPI and select Name + squaredCPI.
            //107.Use `let` with filtering: compute ageCategory(Teen / Adult) and select names where category is "Adult".
            //108.Use `let` with multiple calculations(e.g., bonusCPI and isHigh) and project Name, bonusCPI, isHigh.

            //---

            //## **Into / Grouping Continuation — 3 Questions**

            //109.Group by branch into groups and select Branch + Count for groups having more than one student.
            //110.Group by Sem and select Sem with student names in that Sem.
            //111.Nested grouping: group by Branch, then within each branch group by Sem, and select Branch, Sem, and student names.

            //---

            //## **Complex Multi-Join — 3 Questions**

            //112. 3 - table join example(Student + Course + Departments) — select Student Name, CourseName, DeptHead.
            //113.For each student compute total credits and include the branch head by joining with Departments.
            //114.Left join students with Departments, selecting Student Name, Branch, and DeptHead(or "No Dept" if missing).

        }
    }
}
