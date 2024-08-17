using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeedataController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public EmployeedataController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesData()
        {
            try
            {
                // Use ToListAsync to perform the operation asynchronously
                var allData = await dbContext.Employeedata.ToListAsync();
                return Ok(allData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // GET: api/employeedata/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeData(Guid id)
        {
            try
            {
                // Use ToListAsync() for asynchronous operation
                var employee = await dbContext.Employeedata
                                               .Where(e => e.Id == id)
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


        // POST: api/employeedata
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeData([FromBody] Employeedata newEmployee)
        {
            if (newEmployee == null)
            {
                return BadRequest("Employee data cannot be null.");
            }

            try
            {
                newEmployee.Id = Guid.NewGuid();
                dbContext.Employeedata.Add(newEmployee);
                await dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEmployeeData), new { id = newEmployee.Id }, newEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
        // PUT: api/employeedata/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeData(Guid id, [FromBody] Employeedata updatedEmployee)
        {
            if (id != updatedEmployee.Id)
            {
                return BadRequest("Employee ID mismatch.");
            }

            try
            {
                
                var existingEmployee = await dbContext.Employeedata
                                                       .Where(e => e.Id == id)
                                                       .FirstOrDefaultAsync();

                if (existingEmployee == null)
                {
                    return NotFound();
                }

                existingEmployee.Name = updatedEmployee.Name;
                existingEmployee.email = updatedEmployee.email; 
                dbContext.Employeedata.Update(existingEmployee);

                await dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred: {ex.Message}");

               
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }



       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeData(Guid id)
        {
            try
            {
                var employee = await dbContext.Employeedata
                                               .Where(e => e.Id == id)
                                               .FirstOrDefaultAsync();

                if (employee == null)
                {
                    return NotFound();
                }

                
                dbContext.Employeedata.Remove(employee);

                
                await dbContext.SaveChangesAsync();

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
