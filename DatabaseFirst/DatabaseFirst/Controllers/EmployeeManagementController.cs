using DatabaseFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }

}
