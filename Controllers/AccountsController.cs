using API2.Base;
using API2.Models;
using API2.Repository.Data;
using API2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;


namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [Route("Login")]
        [HttpGet]
        public ActionResult<RegisterVM> Login(RegisterVM registerVM)
        {
            var result = accountRepository.Login(registerVM);
            if (result > 0)
            {
                if (result == 1)
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Success, login!" });
                }
                else
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Password invalid!" });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email and Password invalid!" });
            }
        }

        [Route("Forget")]
        [HttpPut]
        public ActionResult<RegisterVM> SentOTP(RegisterVM registerVM)
        {
            var result = accountRepository.SentOTP(registerVM);
            if (result > 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Success, email sent!" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Failed, email not sent!" });
            }
        }

        [Route("ChangePassword")]
        [HttpPut]
        public ActionResult<ChangeVM> ChangePass(ChangeVM changeVM)
        {
            var result = accountRepository.ChangePass(changeVM);

            if (result == 1)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Success, password has been changed!" });
            }
            else if (result == 2)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Confirmation password does not match!" });
            }
            else if (result == 3)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "OTP code already used!" });
            }
            else if (result == 4)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "OTP code does not match!" });
            }
            else if (result == 5)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "OTP code has expired!" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "Email is not registered!" });
            }
        }
    }
}
