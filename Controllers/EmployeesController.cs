using API2.Base;
using API2.Models;
using API2.Repository.Data;
using API2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost("{Register}")]
        public ActionResult<RegisterVM> Post(RegisterVM registerVM)
        {
            var result = employeeRepository.Register(registerVM);
            if (result != 0)
            {
                if (result == 2)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email already exists!" });
                }
                else if (result == 3)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Phone number already exists!" });
                }
                else if (result == 4)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email and Phone number already exists!" });
                }
                else
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Success, has been added!" });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Failed, not added!" });
            }
        }

        [Authorize(Roles = "Director, Manager")]
        [Route("Registerdata")]
        [HttpGet]
        public ActionResult<RegisterVM> GetRegisterData()
        {
            var result = employeeRepository.GetRegisterData();
            if (result != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Successed, search was found!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Failed, search not found!" });
            }
        }

        //CORS
        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Success, Test CORS!");
        }
    }
}