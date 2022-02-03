using API2.Context;
using API2.Models;
using API2.Repository.Interface;
using API2.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace API2.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext Context;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.Context = myContext;
        }

        public int Login(RegisterVM registerVM)
        {
            var cekEmailPhone = Context.Employees.Where(emp => emp.Email == registerVM.Email || emp.Phone == registerVM.Phone).FirstOrDefault();
            if (cekEmailPhone != null)
            {
                var obj = Context.Accounts.Where(emp => emp.NIK == cekEmailPhone.NIK).FirstOrDefault();
                if (BCrypt.Net.BCrypt.Verify(registerVM.Password, obj.Password))
                {
                    return 1;//Sukses
                }
                else
                {
                    return 2;//Password salah
                }
            }
            else
            {
                return 3; //Email no
            }
        }

        public int SentOTP(RegisterVM registerVM)
        {
            var cekEmail = Context.Employees.Where(emp => emp.Email == registerVM.Email).FirstOrDefault();
            if (cekEmail != null)
            {
                
                Random random = new Random();
                int OTP = random.Next(100000, 999999);

                var obj = Context.Accounts.Where(e => e.NIK == cekEmail.NIK).FirstOrDefault();
                obj.OTP = OTP;
                obj.Expired_Token = DateTime.Now.AddMinutes(5);
                obj.Is_Used = false;

                Context.Entry(obj).State = EntityState.Modified;
                Context.SaveChanges();

                var fromEmail = new MailAddress("kgula5050@gmail.com", "CS");
                var toEmail = new MailAddress(registerVM.Email, "Client");                
                const string fromPassword = "kembang1234";
                var subject = "OTP Reset Password" + DateTime.Now.ToString();
                var body = "Helloo! According to your OTP is: " + OTP.ToString();

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromPassword),
                };

                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return 1;
            }
            else
            {
                return 0; //Email no
            }
        }

        public int ChangePass(ChangeVM changeVM)
        {
            var cekEmail = Context.Employees.Where(e => e.Email == changeVM.Email).FirstOrDefault();
            if (cekEmail != null)
            {
                var obj = Context.Accounts.Where(n => n.NIK == cekEmail.NIK).FirstOrDefault();
                if (DateTime.Now < obj.Expired_Token)
                {
                    if (obj.OTP == changeVM.OTP)
                    {
                        if (obj.Is_Used == false)
                        {
                            if (changeVM.Password == changeVM.ConfirmPassword)
                            {
                                obj.Password = BCrypt.Net.BCrypt.HashPassword(changeVM.ConfirmPassword);
                                obj.Is_Used = true;
                                Context.Entry(obj).State = EntityState.Modified;
                                Context.SaveChanges();
                                return 1;
                            }
                            else
                            {
                                return 2;
                            }
                        }
                        else
                        {
                            return 3;
                        }
                    }
                    return 4;
                }
                else
                {
                    return 5;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
