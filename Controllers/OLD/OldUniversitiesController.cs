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
    public class OldUniversitiesController : ControllerBase
    {
        private OldUniversityRepository universityRepository;
        public OldUniversitiesController(OldUniversityRepository universityRepository)
        {
            this.universityRepository = universityRepository;
        }

        [HttpPost]
        public ActionResult Post(University university) //MENYIMPAN EMPLOYEE BARU
        {
            var result = universityRepository.Insert(university);
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
            var result = universityRepository.Get();
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
            var result = universityRepository.Get(Id);
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
        public ActionResult Put(University university)
        {
            var result = universityRepository.Update(university);
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
            var result = universityRepository.Delete(Id);
            switch (result)
            {
                case 1:
                    return StatusCode(400, new { status = HttpStatusCode.NotFound, result, message = "Id not found!" });
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
