using API2.Context;
using API2.Models;
using API2.Repository.Interface;
using API2.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace API2.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext Context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.Context = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            var EmailExist = IsEmailExist(registerVM);    //ini dari void dibawah
            var PhoneExist = IsPhoneExist(registerVM);    //ini juga
            if (EmailExist == false)    //cek email gak ada
            {
                if (PhoneExist == false) //cek nomor hp gak ada
                {
                    //EMPLOYEE
                    Employee emp = new Employee();
                    var NIK = GetLastNIK() + 1; //ngambil nik terakhir, ini dijalankan
                    var Year = DateTime.Now.Year;
                    emp.NIK = Year + "00" + NIK.ToString();
                    emp.FirstName = registerVM.FirstName;
                    emp.LastName = registerVM.LastName;
                    emp.Phone = registerVM.Phone;
                    emp.BirthDate = registerVM.BirthDate;
                    emp.Salary = registerVM.Salary;
                    emp.Email = registerVM.Email;
                    Context.Employees.Add(emp);
                    Context.SaveChanges();

                    //ACCOUNT
                    Account acc = new Account();
                    acc.NIK = emp.NIK;
                    acc.Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password);
                    Context.Accounts.Add(acc);
                    Context.SaveChanges();

                    //ACCOUNTROLE
                    AccountRole acro = new AccountRole();
                    acro.NIK = acc.NIK;
                    acro.Role_Id = 1;
                    Context.AccountRoles.Add(acro);
                    Context.SaveChanges();

                    //EDUCATION
                    Education ed = new Education();
                    ed.Degree = registerVM.Degree;
                    ed.GPA = registerVM.GPA;
                    ed.University_Id = registerVM.University_Id;
                    Context.Educations.Add(ed);
                    Context.SaveChanges();

                    Profiling pro = new Profiling();
                    pro.NIK = acc.NIK;
                    pro.Education_Id = ed.Id;
                    Context.Profilings.Add(pro);
                    Context.SaveChanges();

                    return 1;
                }
                else
                {
                    return 3; //nomor telepon sudah ada
                }
            }
            else if (EmailExist == true && PhoneExist == true)
            {
                return 4; //email dan nomor telepon sudah ada
            }
            else
            {
                return 2; //email sudah ada
            }
            
            
        }

        public bool IsEmailExist(RegisterVM registerVM)
        {
            var CekEmail = Context.Employees.Where(emp => emp.Email == registerVM.Email).FirstOrDefault(); //0 atau 1 diambil pertama
            if (CekEmail != null)
            {
                return true; //kalau menemukan email sama
            }
            else
            {
                return false; //tidak menemukan email sama
            }
        }
        public bool IsPhoneExist(RegisterVM registerVM)
        {
            var CekPhone = Context.Employees.Where(emp => emp.Phone == registerVM.Phone).FirstOrDefault(); //null atau 1 diambil pertama
            if (CekPhone != null)
            {
                return true; //kalau menemukan nomor hp sama
            }
            else
            {
                return false; //kalau menemukan email sama
            }
        }
        public int GetLastNIK()
        {
            var lastEmp = Context.Employees.OrderByDescending(emp => emp.NIK).FirstOrDefault(); //diurutkan dari nik terakhir
            if (lastEmp == null)
            {
                return 0; //tidak ditemukan atau belum ada nik
            }
            else
            {
                var lastNIK = lastEmp.NIK.Remove(0, 5); //kalau ada nik yang terakhir 
                return int.Parse(lastNIK);
            }
        }

        public IEnumerable GetRegisterData()
        {
            var employee = Context.Employees;
            var account = Context.Accounts;
            var profiling = Context.Profilings;
            var education = Context.Educations;
            var university = Context.Universities;
            var role = Context.Roles;
            var accountrole = Context.AccountRoles;

            var result = (from emp in employee
                          join acc in account on emp.NIK equals acc.NIK
                          join acro in accountrole on acc.NIK equals acro.NIK
                          join rol in role on acro.Role_Id equals rol.Id
                          join pro in profiling on acc.NIK equals pro.NIK
                          join edu in education on pro.Education_Id equals edu.Id
                          join univ in university on edu.University_Id equals univ.Id

                          select new
                          {
                              FullName = emp.FirstName + " " + emp.LastName,
                              Phone = emp.Phone,
                              BirthDate = emp.BirthDate,
                              Salary = emp.Salary,
                              Email = emp.Email,
                              Degree = edu.Degree,
                              GPA = edu.GPA,
                              UnivName = univ.Name,
                              RoleName = rol.Name,
                          }).ToList();

            return result;
        }
    }
}
