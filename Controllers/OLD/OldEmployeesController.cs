using API2.Context;
using API2.Models;
using API2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldEmployeesController : ControllerBase
    {
        private OldEmployeeRepository employeeRepository;
        public OldEmployeesController(OldEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public ActionResult Post(Employee employee) //MENYIMPAN EMPLOYEE BARU
        {
            var result = employeeRepository.Insert(employee);
            if (result != 0)
            {
                if (result == 1)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email already exists!" });
                }
                else if (result == 2)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Phone number already exists!" });
                }
                else if (result == 3)
                {
                    return StatusCode(200, new { status = HttpStatusCode.BadRequest, result, message = "Email and Phone number already exists!" });
                }
                else
                {
                    return StatusCode(400, new { status = HttpStatusCode.OK, result, message = "Insert employee is successfully!" });
                }
            }
            else { 
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Insert employee is failed!" });
            }
        }

        [HttpGet]
        public ActionResult Get() //MENAMPILKAN
        {
            var result = employeeRepository.Get();
            if (result.Count() > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Showing data is successfully!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Showing data is not found!" });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult GetByNIK(string NIK)
        {
            var result = employeeRepository.Get(NIK);
            if(result != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Showing data is successfully!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Showing data is not found!" });
            }
        }

        [HttpPut]
        public ActionResult Put(Employee employee)
        {
            var result = employeeRepository.Update(employee);
            if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Update data is successfully!" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Data format invalid!" });
            }
            /*switch (result)
            {
                case 1:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email cannot be empty!" });
                case 2:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Phone number cannot be empty!" });
                case 3:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email address format invalid!" });
                case 4:
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Update successfully!" });
                case 5:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Update failed!" });
                default:
                    return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result, message = "Internal server error!" });
            }*/
        }

        [HttpDelete]
        public ActionResult Delete(string NIK)
        {
            var result = employeeRepository.Delete(NIK);
            switch (result)
            {
                case 1:
                    return StatusCode(400, new { status = HttpStatusCode.NotFound, result, message = "NIK not found!" });
                case 2:
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Delete successfully!" });
                case 3:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Delete failed!" });
                default :
                    return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result, message = "Internal server error!" });
            }
        }

    }
}
 