using API2.Context;
using API2.Models;
using API2.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API2.Repository
{
    public class OldEducationRepository : IEducationRepository //MENUJU IEducationRepository
    {
        //CONTRUCTOR
        private readonly MyContext context;
        public OldEducationRepository(MyContext context)
        {
            this.context = context;
        }

        public int Delete(int Id) //DELETE
        {
            var entity = context.Educations.Find(Id);//MENCARI DATA
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

        public IEnumerable<Education> Get() //READ OR MENAMPILKAN
        {
            return context.Educations.ToList();
        }

        public Education Get(int Id)//REFERENSI kemarin CEK
        {
            var educationById = context.Educations.Find(Id);
            return educationById;
        }

        public int Insert(Education education)
        {
            int countRow = this.Get().Count();

                if (countRow == 0)
                {
                    education.Id = countRow + 1;
                }
                
                context.Educations.Add(education);
            
            var result = context.SaveChanges();
            return result;
        }

        public int Update(Education education) //UPDATE
        {
            context.Entry(education).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }
    }
}
