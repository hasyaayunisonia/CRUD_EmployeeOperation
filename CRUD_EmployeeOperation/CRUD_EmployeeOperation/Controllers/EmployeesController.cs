using CRUD_EmployeeOperation.DTO;
using CRUD_EmployeeOperation.Models;
using CRUD_EmployeeOperation.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_EmployeeOperation.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployee _employee;

        public EmployeesController(IEmployee employee)
        {
            _employee = employee;
        }

        /* GET: /api/employee */
        [HttpGet]
        public ActionResult <IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                return Ok(_employee.GetEmployees());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving employee data. Please try again later.");
            }
        }

        /* GET: /api/employee/{employeeId} */
        [HttpGet("{employeeId}")]
        public ActionResult<Employee> GetEmployeeById(string employeeId)
        {
            try
            {
                var employee = _employee.GetEmployeeDTOById(employeeId);
                if (employee == null)
                {
                    // If the employee is not found, return 404 Not Found with a message
                    return NotFound(new { message = $"Employee with ID {employeeId} not found." });
                };
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving employee data. Please try again later.");
            }
        }

        /* POST: /api/employee */
        [HttpPost]
        public ActionResult<EmployeeDTO> CreateEmployee([FromBody] EmployeeRequestDTO createEmployeeDto)
        {
            try
            {
                if (!DateOnly.TryParseExact(createEmployeeDto.BirthDate, "dd-MM-yyyy", out var birthDate))
                {
                    return BadRequest(new { message = "Invalid date format. Please use DD-MM-YYYY." });
                }

                var existingEmployee = _employee.GetEmployeesExist()
           .FirstOrDefault(e => e.FullName == createEmployeeDto.FullName
                                 && DateOnly.ParseExact(e.BirthDate, "dd-MM-yy", null) == birthDate);

                if (existingEmployee != null)
                {
                    return Conflict(new { message = "Employee with the same name and birth date already exists." });
                }

                var newEmployee = new Employee
                {
                    FullName = createEmployeeDto.FullName,
                    BirthDate = DateOnly.ParseExact(createEmployeeDto.BirthDate, "dd-MM-yyyy", null)
                };

                string newEmployeeId = _employee.CreateEmployee(newEmployee);

                return Ok(_employee.GetEmployeeDTOById(newEmployeeId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        /* PUT: /api/employee */
        [HttpPut]
        public IActionResult updateEmployee(string employeeId, [FromBody] EmployeeRequestDTO updateEmployeeDto)
        {
            try
            {
                if (!DateOnly.TryParseExact(updateEmployeeDto.BirthDate, "dd-MM-yyyy", out var birthDate))
                {
                    return BadRequest(new { message = "Invalid date format. Please use DD-MM-YYYY." });
                }

                var existingEmployee = _employee.GetEmployeeById(employeeId);
                if (existingEmployee == null)
                {
                    // If the employee is not found, return 404 Not Found with a message
                    return NotFound(new { message = $"Employee with ID {employeeId} not found." });
                }

                // Check for duplicate employee
                var duplicateEmployee = _employee.GetEmployeesExist()
           .FirstOrDefault(e => e.FullName == updateEmployeeDto.FullName
                                 && DateOnly.ParseExact(e.BirthDate, "dd-MM-yy", null) == birthDate);

                if (duplicateEmployee != null)
                {
                    return Conflict(new { message = "Employee with the same name and birth date already exists." });
                }

                var updateEmployee = new Employee
                {
                    EmployeeID = employeeId,
                    FullName = updateEmployeeDto.FullName,
                    BirthDate = DateOnly.ParseExact(updateEmployeeDto.BirthDate, "dd-MM-yyyy", null)
                };
                _employee.UpdateEmployee(updateEmployee);

                return Ok(_employee.GetEmployeeDTOById(employeeId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        /* DELETE: /api/employee */
        [HttpDelete]
        public IActionResult DeleteEmployee(string employeeId) {
            try
            {
                var employee = _employee.GetEmployeeById(employeeId);
                
                if (employee == null)
                {
                    // If the employee is not found, return 404 Not Found with a message
                    return NotFound(new { message = $"Employee with ID {employeeId} not found." });
                }
                // If employee is found then delete data and return success message
                _employee.DeleteEmployeeById(employeeId);
                
                return Ok(new { message = $"Employee with ID {employeeId} has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }   
    }
}
