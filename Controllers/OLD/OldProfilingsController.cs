using API2.Models;
using API2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldProfilingsController : ControllerBase
    {
        private OldProfilingRepository profilingRepository;
        public OldProfilingsController(OldProfilingRepository profilingRepository)
        {
            this.profilingRepository = profilingRepository;
        }

        [HttpPost]
        public ActionResult Post(Profiling profiling) //MENYIMPAN PROFILING BARU
        {
            var result = profilingRepository.Insert(profiling);
            if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Insert profiling data is successfully!" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Profiling data format invalid!" });
            }
        }

        [HttpGet]
        public ActionResult Get() //MENAMPILKAN
        {
            var result = profilingRepository.Get();
            /*if (result.Count() > 0)
            {*/
            return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Showing profiling data is successfully!" });
            /*}
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Showing data is not found!" });
            }*/
        }

        [HttpGet("{NIK}")]
        public ActionResult GetByNIK(string NIK)
        {
            var result = profilingRepository.Get(NIK);
            if (result != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Showing profiling data is successfully!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Showing profiling data is not found!" });
            }
        }

        [HttpPut]
        public ActionResult Put(Profiling profiling)
        {
            var result = profilingRepository.Update(profiling);
            if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Update profiling data is successfully!" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Profiling data format invalid!" });
            }
        }

        [HttpDelete]
        public ActionResult Delete(string NIK)
        {
            var result = profilingRepository.Delete(NIK);
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
