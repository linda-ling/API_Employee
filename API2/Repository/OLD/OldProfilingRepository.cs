using API2.Context;
using API2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API2.Repository
{
    public class OldProfilingRepository
    {
        //CONTRUCTOR
        private readonly MyContext context;
        public OldProfilingRepository(MyContext context)
        {
            this.context = context;
        }

        public int Delete(string NIK) //DELETE
        {
            var entity = context.Profilings.Find(NIK);//MENCARI DATA
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

        public IEnumerable<Profiling> Get() //READ OR MENAMPILKAN
        {
            return context.Profilings.ToList();
        }

        public Profiling Get(string NIK)//REFERENSI kemarin CEK
        {
            var profilingByNIK = context.Profilings.Find(NIK);
            return profilingByNIK;
        }

        public int Insert(Profiling profiling)
        {
            var countRow = this.Get().Count();
            var year = DateTime.Now.ToString("yyyy");

            string maxNIK = context.Profilings.Max(e => e.NIK);
            int newNIK = Convert.ToInt32(maxNIK);
            newNIK += 1;
            string stringNIK = Convert.ToString(newNIK).Remove(0, 6);

                if (countRow == 0)
                {
                    profiling.NIK = $"{year}00{countRow + 1}";
                }
                else
                {
                    profiling.NIK = $"{year}00{stringNIK}";
                }
                context.Profilings.Add(profiling);

            var result = context.SaveChanges();
            return result;
        }

        public int Update(Profiling profiling) //UPDATE
        {
            context.Entry(profiling).State = EntityState.Modified;

            var result = context.SaveChanges();
            return result;
        }

    }
}
