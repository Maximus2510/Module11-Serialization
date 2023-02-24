using System.Runtime.Serialization.Formatters.Binary;

namespace BinarySerialization
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
    }

    class Binary
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

            var formatter = new BinaryFormatter();
            using (var stream = new FileStream("department.bin", FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, department);
            }

            // Deserialize the department object from the file
            using (var stream = new FileStream("department.bin", FileMode.Open, FileAccess.Read))
            {
                var deserializedDepartment = (Department)formatter.Deserialize(stream);
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