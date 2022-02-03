using API2.Base;
using API2.Models;
using API2.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingsController : BaseController<Profiling, ProfilingRepository, string>
    {
        public ProfilingsController(ProfilingRepository profilingRepository) : base(profilingRepository)
        {

        }
    }
}
