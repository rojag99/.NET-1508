using DatabaseFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeManagementController : ControllerBase
    {
        private readonly EmployeeManagementContext employeeManagementContext;

        public EmployeeManagementController(EmployeeManagementContext employeeManagementContext)
        {
            this.employeeManagementContext = employeeManagementContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesData()
        {
            try
            {
                // Use ToListAsync to perform the operation asynchronously
                var allData = await employeeManagementContext.Employeedata.ToListAsync();
                return Ok(allData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeData(int Id)
        {
            try
            {
                
                var employee = await employeeManagementContext.Employeedata
                                               .Where(e => e.Id == Id)
                                               .FirstOrDefaultAsync();

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");


                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
/*
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeData([FromBody] Employeedatum newEmployee)
        {
            if (newEmployee == null)
            {
                return BadRequest("Employee data cannot be null.");
            }

            try
            {
                newEmployee.Id = int.NewGuid();
                employeeManagementContext.Employeedata.Add(newEmployee);
                await employeeManagementContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEmployeeData), new { Id = newEmployee.Id }, newEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
       */

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateEmployeeData(int Id, [FromBody] Employeedatum updatedEmployee)
        {
            if (Id != updatedEmployee.Id)
            {
                return BadRequest("Employee ID mismatch.");
            }
            try
            {

                var existingEmployee = await employeeManagementContext.Employeedata
                                                       .Where(e => e.Id == Id)
                                                       .FirstOrDefaultAsync();

                if (existingEmployee == null)
                {
                    return NotFound();
                }

                existingEmployee.Name = updatedEmployee.Name;
                existingEmployee.Email = updatedEmployee.Email;
                employeeManagementContext.Employeedata.Update(existingEmployee);

                await employeeManagementContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");


                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeData(int Id)
        {
            try
            {
                var employee = await employeeManagementContext.Employeedata
                                               .Where(e => e.Id == Id)
                                               .FirstOrDefaultAsync();

                if (employee == null)
                {
                    return NotFound();
                }


                employeeManagementContext.Employeedata.Remove(employee);


                await employeeManagementContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");


                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
