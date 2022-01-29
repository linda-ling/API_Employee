using API2.Models;
using API2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldAccountsController : ControllerBase
    {
        private OldAccountRepository accountRepository;
        public OldAccountsController(OldAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost]
        public ActionResult Post(Account account) //MENYIMPAN ACCOUNT BARU
        {
            var result = accountRepository.Insert(account);
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
            var result = accountRepository.Get();
            /*if (result.Count() > 0)
            {*/
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Showing data is successfully!" });
            /*}
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Showing data is not found!" });
            }*/
        }

        [HttpGet("{NIK}")]
        public ActionResult GetByNIK(string NIK)
        {
            var result = accountRepository.Get(NIK);
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
        public ActionResult Put(Account account)
        {
            var result = accountRepository.Update(account);
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
        public ActionResult Delete(string NIK)
        {
            var result = accountRepository.Delete(NIK);
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
