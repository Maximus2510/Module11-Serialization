using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JsonSerialization
{
    public class Employee
    {
        public string EmployeeName { get; set; }
    }

    public class Department
    {
        public string DepartmentName { get; set; }
        public List<Employee> Employees { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var department = new Department
            {
                DepartmentName = "QA",
                Employees = new List<Employee>
                {
                    new Employee { EmployeeName = "Max" },
                    new Employee { EmployeeName = "Alex" }
                }
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(department, options);
            File.WriteAllText("department.json", jsonString);

            var deserializedDepartment = JsonSerializer.Deserialize<Department>(File.ReadAllText("department.json"));
            Console.WriteLine($"Department Name: {deserializedDepartment.DepartmentName}");

            foreach (var employee in deserializedDepartment.Employees)
            {
                Console.WriteLine($"Employee Name: {employee.EmployeeName}");
            }

            Console.ReadKey();
        }
    }
}