using API2.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace API2.ViewModel
{
    public class RegisterVM
    {
        //EMPLOYEE
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }

        //ACCOUNT
        public string Password { get; set; }

        //ROLE
        public string Name { get; set; }

        //EDUCATION
        public string Degree { get; set; }
        public string GPA { get; set; }
       
        //UNIVERSITY
        public int University_Id { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
