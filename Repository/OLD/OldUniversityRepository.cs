using API2.Context;
using API2.Models;
using API2.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API2.Repository
{
    public class OldUniversityRepository : IUniversityRepository //MENUJU IUniversityRepository
    {
        //CONTRUCTOR
        private readonly MyContext context;
        public OldUniversityRepository(MyContext context)
        {
            this.context = context;
        }

        public int Delete(int Id) //DELETE
        {
            var entity = context.Universities.Find(Id);//MENCARI DATA
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

        public IEnumerable<University> Get() //READ OR MENAMPILKAN
        {
            return context.Universities.ToList();
        }

        public University Get(int Id)//REFERENSI kemarin CEK
        {
            var universityById = context.Universities.Find(Id);
            return universityById;
        }

        public int Insert(University university)
        {
            int countRow = this.Get().Count();

            if (countRow == 0)
            {
                university.Id = countRow + 1;
            }

            context.Universities.Add(university);

            var result = context.SaveChanges();
            return result;
        }

        public int Update(University university) //UPDATE
        {
            context.Entry(university).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }
    }
}