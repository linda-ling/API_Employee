using API2.Models;
using API2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldEducationsController : ControllerBase
    { 
        private OldEducationRepository educationRepository;
        public OldEducationsController(OldEducationRepository educationRepository)
        {
            this.educationRepository = educationRepository;
        }

        [HttpPost]
        public ActionResult Post(Education education) //MENYIMPAN EMPLOYEE BARU
        {
            var result = educationRepository.Insert(education);
            if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Insert data is successfully!" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Data format invalid!" });
            }
        }

        [HttpGet]
        public ActionResult Get() //MENAMPILKAN
        {
            var result = educationRepository.Get();
            if (result.Count() > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Showing data is successfully!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Showing data is not found!" });
            }
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            var result = educationRepository.Get(Id);
            if (result != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Showing data is successfully!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Showing data is not found!" });
            }
        }

        [HttpPut]
        public ActionResult Put(Education education)
        {
            var result = educationRepository.Update(education);
            if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Update data is successfully!" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Data format invalid!" });
            }
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            var result = educationRepository.Delete(Id);
            switch (result)
            {
                case 1:
                    return StatusCode(400, new { status = HttpStatusCode.NotFound, result, message = "NIK not found!" });
                case 2:
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Delete successfully!" });
                case 3:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Delete failed!" });
                default:
                    return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result, message = "Internal server error!" });
            }
        }

    }
}
 