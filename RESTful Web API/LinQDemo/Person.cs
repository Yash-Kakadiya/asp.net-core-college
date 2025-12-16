using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQDemo
{
    internal class Person
    {
        // name,age,deptartment,city
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public string City { get; set; }

        public Person(string Name, int Age, string Department, string City)
        {
            this.Name = Name;
            this.Age = Age;
            this.Department = Department;
            this.City = City;
        }

        public static List<Person> GetPeople()
        {
            List<Person> people = new List<Person>
            {
                new Person("Alice", 30, "HR", "New York"),
                new Person("Bob", 25, "IT", "San Francisco"),
                new Person("Charlie", 35, "Finance", "Chicago")
            };
            return people;
        }
    }
}
