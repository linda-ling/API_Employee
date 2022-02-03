using API2.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace API2.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            if (result.Count() > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Successed, search was found!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Failed, search not found!" });
            }
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var result = repository.Get(key);
            //return Ok(result);
            if (result != null)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Successed, search was found!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Failed, search not found!" });
            }
        }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            var result = repository.Insert(entity);
            if (result != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Success, has been added!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Failed, not ready to add!" });
            }
        }

        [HttpPut]
        public ActionResult Put(Entity entity)
        {
            var result = repository.Update(entity);
            if (result != 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Success, has changed!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Failed, not ready to change!" });
            }
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            var result = repository.Delete(key);
            //return Ok(result);
            switch (result)
            {
                case 1:
                    return StatusCode(400, new { status = HttpStatusCode.NotFound, result, message = "Data not found!" });
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
