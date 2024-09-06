using CRUD_EmployeeOperation.DTO;
using CRUD_EmployeeOperation.Models;

namespace CRUD_EmployeeOperation.Repositories
{
    public interface IEmployee
    {

        IEnumerable<EmployeeDTO> GetEmployeesExist();
        IEnumerable<EmployeeDTO> GetEmployees();
        Employee GetEmployeeById(string EmployeeID);
        EmployeeDTO GetEmployeeDTOById(string EmployeeID);
        string CreateEmployee(Employee employee);
        void DeleteEmployeeById(string EmployeeID);
        void UpdateEmployee(Employee employee);
    }
}
