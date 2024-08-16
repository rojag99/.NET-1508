using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAllEmployeesData()
        {
         var alldata=   dbContext.Employeedata.ToList();
            return Ok(alldata);
            

        }
        // GET: api/employeedata/{id}
        [HttpGet("{id}")]
        public IActionResult GetEmployeeData(Guid id)
        {
            // Use LINQ to find the employee by id
            var employee = dbContext.Employeedata
                                    .Where(e => e.Id == id)
                                    .FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        // POST: api/employeedata
        [HttpPost]
        public IActionResult CreateEmployeeData([FromBody] Employeedata newEmployee)
        {
            if (newEmployee == null)
            {
                return BadRequest();
            }

            newEmployee.Id = Guid.NewGuid(); // Ensure new ID
            dbContext.Employeedata.Add(newEmployee);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetEmployeeData), new { id = newEmployee.Id }, newEmployee);
        }
        // PUT: api/employeedata/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEmployeeData(Guid id, [FromBody] Employeedata updatedEmployee)
        {
            if (id != updatedEmployee.Id)
            {
                return BadRequest();
            }

            // Use LINQ to find the employee by id
            var existingEmployee = dbContext.Employeedata
                                            .Where(e => e.Id == id)
                                            .FirstOrDefault();
            if (existingEmployee == null)
            {
                return NotFound();
            }

            // Update the employee details
            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.email = updatedEmployee.email;

            dbContext.Employeedata.Update(existingEmployee);
            dbContext.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeData(Guid id)
        {
            // Use LINQ to find the employee by id
            var employee = dbContext.Employeedata
                                    .Where(e => e.Id == id)
                                    .FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }

            dbContext.Employeedata.Remove(employee);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
