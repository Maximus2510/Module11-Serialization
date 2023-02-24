using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Module_08_Serialization
{
    public class XML
    {
        [XmlRoot("Department")]
        public class Department
        {
            [XmlElement("DepartmentName")]
            public string DepartmentName { get; set; }

            [XmlArray("Employees")]
            [XmlArrayItem("Employee")]
            public List<Employee> Employees { get; set; }
        }

        public class Employee
        {
            [XmlAttribute("EmployeeName")]
            public string EmployeeName { get; set; }
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

                var serializer = new XmlSerializer(typeof(Department));
                using (var stream = new FileStream("department.xml", FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(stream, department);
                }

                using (var stream = new FileStream("department.xml", FileMode.Open, FileAccess.Read))
                {
                    var deserializedDepartment = (Department)serializer.Deserialize(stream);
                    Console.WriteLine($"Department Name: {deserializedDepartment.DepartmentName}");

                    foreach (var employee in deserializedDepartment.Employees)
                    {
                        Console.WriteLine($"Employee Name: {employee.EmployeeName}");
                    }
                }

                Console.ReadKey();
            }
        }
    }

}
