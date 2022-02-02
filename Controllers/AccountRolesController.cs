using API2.Base;
using API2.Context;
using API2.Models;
using API2.Repository.Data;
using API2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, string>
    {
        public AccountRoleRepository accountRoleRepository;
        public IConfiguration _configuration;
        public MyContext context;
        public AccountRolesController(AccountRoleRepository accountRoleRepository, IConfiguration configuration, MyContext context) : base(accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
            this._configuration = configuration;
            this.context = context;
        }

        //
        //[Route("SignManager")]
        [Authorize(Roles = "Director")]
        [HttpPost("SignManager")]
        public ActionResult<AccountRoleVM> SignManager(AccountRoleVM accountRoleVM)
        {
            var result = accountRoleRepository.SignManager(accountRoleVM);
            return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Success, manager has been added!" });
        }
    }
}
