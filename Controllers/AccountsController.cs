using API2.Base;
using API2.Models;
using API2.Repository.Data;
using API2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System;
using API2.Context;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;
        public MyContext context;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration, MyContext context) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
            this.context = context;
         }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(RegisterVM registerVM)
        {
            var result = accountRepository.Login(registerVM);
            if (result != 0)
            {
                if (result == 2)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Password invalid!" });
                }
                else if (result == 3)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Failed, Account not found!" });
                }
                else
                {
                    var getUserData = context.Employees.Where(e => e.Email == registerVM.Email || e.Phone == registerVM.Phone).FirstOrDefault(); //get email & role name
                    //var account = context.Accounts.Where(a => a.NIK == getUserData.NIK).FirstOrDefault();
                    //var role = context.AccountRoles.Where(a => a.NIK == account.NIK).FirstOrDefault();
                    var role = context.Roles.Where(a => a.AccountRole.Any(ar => ar.Account.NIK == getUserData.NIK)).ToList();

                    var claims = new List<Claim>
                    {
                        new Claim("Email", getUserData.Email),
                        //new Claim(ClaimTypes.Email, getUserData.Email) //PAYLOAD
                    };
                    foreach (var item in role)
                    {
                        claims.Add(new Claim("roles", item.Name));
                    }

                    /*var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        SigningCredentials = new SigningCredentials
                        (
                            new SymmetricSecurityKey(tokenKey),
                            SecurityAlgorithms.HmacSha256Signature
                        )
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var idToken = tokenHandler.WriteToken(token);*/
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //HEADER
                    var token = new JwtSecurityToken
                                (
                                    _configuration["Jwt:Issuer"],
                                    _configuration["Jwt:Audience"],
                                    claims,
                                    expires: DateTime.UtcNow.AddMinutes(10),
                                    signingCredentials: signIn
                                );
                    var idToken = new JwtSecurityTokenHandler().WriteToken(token); //Generate Token
                    claims.Add(new Claim("TokenSecurity", idToken.ToString()));
                    return StatusCode(200, new { status = HttpStatusCode.OK, idToken, message = "Success, login!" });  
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email and Password invalid!" });
            }
        }

        [Authorize(Roles = "Director")]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Success, Test JWT!");
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
