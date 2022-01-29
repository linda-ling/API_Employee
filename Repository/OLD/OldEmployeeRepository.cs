using API2.Context;
using API2.Models;
using API2.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace API2.Repository
{
    public class OldEmployeeRepository : OldIEmployeeRepository //MENUJU IEmployeeRepository
    {
        //CONTRUCTOR
        private readonly MyContext context;
        public OldEmployeeRepository(MyContext context)
        {
            this.context = context;
        }

        public int Delete(string NIK) //DELETE
        {

            var entity = context.Employees.Find(NIK);//MENCARI DATA
            if (entity == null)
            {
                return 1;
            }
            else
            {
                context.Remove(entity);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }

        public IEnumerable<Employee> Get() //READ OR MENAMPILKAN
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)//REFERENSI kemarin CEK
        {
            var employeeByNIK = context.Employees.Find(NIK);
            return employeeByNIK;
        }

        public bool IsEmailExist(Employee employee)
        {
            var cekEmail = context.Employees.Where(e => e.Email == employee.Email).SingleOrDefault();
            if (cekEmail != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPhoneExist(Employee employee)
        {
            var cekPhone = context.Employees.Where(p => p.Phone == employee.Phone).SingleOrDefault();
            if (cekPhone != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public int newNIK()
        {
            var maxNIK = context.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();
            if (maxNIK == null)
            {
                return 0;
            }
            else
            {
                var lastNIK = maxNIK.NIK.Remove(0, 5);
                return int.Parse(lastNIK);
            }
        }

        public int Insert(Employee employee)
        {
            var emailExist = IsEmailExist(employee);
            var phoneExist = IsPhoneExist(employee);

            if (emailExist == false)
            {
                if (phoneExist == false)
                {
                    var NIK = newNIK() + 1;
                    var Year = DateTime.Now.Year;

                    employee.NIK = Year + "00" + NIK.ToString();

                    context.Employees.Add(employee);
                    var result = context.SaveChanges();
                    return result;
                }
                else
                {
                    return 2; //Phone ADA
                }
            }
            else if (emailExist == true && phoneExist == true)
            {
                return 3; //Email - Phone ADA
            }
            else
            {
                return 1; //Email ADA
            }
        }

        public int Update(Employee employee) //UPDATE
        {
            var emailExist = context.Employees.Where(e => e.Email == employee.Email).SingleOrDefault();
            var phoneExist = context.Employees.Where(e => e.Phone == employee.Phone).SingleOrDefault();

            if (emailExist == null && phoneExist == null)
            {
                context.Entry(employee).State = EntityState.Modified;
            }
            var result = context.SaveChanges();
            return result;

            /*string pattern = @"^(?!.)(""([^""\r\]|\[""\r\])""|" + @"([-a-z0-9!#$%&'+/=?^_`{|}~]|(?<!.).))(?<!.)" + @"@[a-z0-9][\w.-][a-z0-9].[a-z][a-z.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            if (employee.Email == null)
            {
                return 1;
            }
            else if (employee.Phone == null)
            {
                return 2;
            }
            else if (!regex.IsMatch(employee.Email))
            {
                return 3;
            }
            else
            {
                context.Entry(employee).State = EntityState.Modified;
                var result = context.SaveChanges();
                if (result > 0)
                {
                    return 4;
                }
                else
                {
                    return 5;
                }
            }*/
        }

        int OldIEmployeeRepository.Delete(string NIK)
        {
            throw new NotImplementedException();
        }

        int OldIEmployeeRepository.Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
    //UNDER DEVELOPMENT
    /*public Employee Search (string searchPhrase)
    {
        var search = Context.Employee.Where(e => searchPhrase.Contains(e.FirstName));
        return (Employee)search;
    }*/
}
