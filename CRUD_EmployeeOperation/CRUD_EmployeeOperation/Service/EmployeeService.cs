using CRUD_EmployeeOperation.DTO;
using CRUD_EmployeeOperation.Models;
using CRUD_EmployeeOperation.Repositories;

namespace CRUD_EmployeeOperation.Service
{
    public class EmployeeService : IEmployee
    {
        //Static data for employees
        private static readonly List<Employee> Employees = new List<Employee>
        {
            new Employee {EmployeeID = "1001", FullName = "John Doe", BirthDate = new DateOnly(1992, 08, 17)},
            new Employee {EmployeeID = "1002", FullName = "Jane Smith", BirthDate = new DateOnly(1990, 06, 15)},
            new Employee {EmployeeID = "1003", FullName = "Michael Johnson", BirthDate = new DateOnly(1989, 09, 27)},
            new Employee {EmployeeID = "1004", FullName = "Emily Davis", BirthDate = new DateOnly(2004, 10, 16)},
            new Employee {EmployeeID = "1005", FullName = "Chris Evans", BirthDate = new DateOnly(1991, 12, 11)}
        };

        /* Retrieve all employee data */
        public IEnumerable<EmployeeDTO> GetEmployees() {
            return Employees.Select(e => new EmployeeDTO
            {
                EmployeeID = e.EmployeeID,
                FullName = e.FullName,
                BirthDate = e.BirthDate.ToString("dd-MMM-yy")
            });
        }

        /* Retrieve all employee data */
        public IEnumerable<EmployeeDTO> GetEmployeesExist()
        {
            return Employees.Select(e => new EmployeeDTO
            {
                EmployeeID = e.EmployeeID,
                FullName = e.FullName,
                BirthDate = e.BirthDate.ToString("dd-MM-yy")
            });
        }

        /* Retrieve employee data by ID and convert the date to dd-MMM-yy format */
        public EmployeeDTO GetEmployeeDTOById(string employeeID) {
            var employee = Employees.FirstOrDefault(e => e.EmployeeID == employeeID);
            if (employee == null)
            {
                return null; // or handle it in a way that makes sense for your application
            }

            // Return the DTO after finding the employee
            return new EmployeeDTO
            {
                EmployeeID = employee.EmployeeID,
                FullName = employee.FullName,
                BirthDate = employee.BirthDate.ToString("dd-MMM-yy")
            };
        }

        /* Retrieve employee data by ID */
        public Employee GetEmployeeById(string employeeID) => Employees.FirstOrDefault(e => e.EmployeeID == employeeID);

        /* Create new employee data*/
        public string CreateEmployee(Employee employee)
        {
            // Add a unique ID for new employees
            int maxId = Employees.Select(e => int.Parse(e.EmployeeID)).Max();
            employee.EmployeeID = (maxId + 1).ToString("D4"); 

            Employees.Add(employee);

            return employee.EmployeeID;
        }

        /* Delete employee data by ID */
        public void DeleteEmployeeById (string employeeID)
        {
            var employee = GetEmployeeById(employeeID);
            if (employee != null)
            {
                Employees.Remove(employee);
            }
        }

        /* Update employee data */
        public void UpdateEmployee(Employee employee)
        {
            var index = Employees.FindIndex(e => e.EmployeeID == employee.EmployeeID);
            if (index >= 0)
            {
                Employees[index] = employee;
            }
        }

    }
}
