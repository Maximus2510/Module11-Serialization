using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepCloning
{
    [Serializable]
    public class Employee
    {
        public string EmployeeName { get; set; }
    }

    [Serializable]
    public class Department
    {
        public string DepartmentName { get; set; }
        public List<Employee> Employees { get; set; }

        public Department DeepCopy()
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            var copy = formatter.Deserialize(stream) as Department;
            return copy;
        }
    }

    class DeepCloningSerialization
    {
        static void Main(string[] args)
        {
            var department = new Department
            {
                DepartmentName = "AQA",
                Employees = new List<Employee>
                {
                    new Employee { EmployeeName = "Max" },
                    new Employee { EmployeeName = "Alex" }
                }
            };

            var copy = department.DeepCopy();
            copy.DepartmentName = "AQA";
            copy.Employees[0].EmployeeName = "Max";

            Console.WriteLine($"Original Department Name: {department.DepartmentName}");

            foreach (var employee in department.Employees)
            {
                Console.WriteLine($"Original Employee Name: {employee.EmployeeName}");
            }

            Console.WriteLine($"Copied Department Name: {copy.DepartmentName}");

            foreach (var employee in copy.Employees)
            {
                Console.WriteLine($"Copied Employee Name: {employee.EmployeeName}");
            }

            Console.ReadKey();
        }
    }
}