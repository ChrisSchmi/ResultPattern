using Microsoft.AspNetCore.Mvc;
using ResultPatternDemo.Common;

namespace ResultPatternDemo.Employee
{
    [ApiController]
    [Route("employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        // GET /employees/123      -> 200 OK { id, name }
        // GET /employees/404      -> 404 ProblemDetails
        [HttpGet("{id}")]
        public ActionResult<Employee> GetById(string id)
            => _service.GetById(id).ToActionResult();

        // DELETE /employees/123   -> 204 NoContent
        // DELETE /employees/404   -> 404 ProblemDetails
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
            => _service.Delete(id).ToActionResult();
    }
}
