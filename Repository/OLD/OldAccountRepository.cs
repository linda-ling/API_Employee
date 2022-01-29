using API2.Context;
using API2.Models;
using API2.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API2.Repository
{
    public class OldAccountRepository : IAccountRepository
    {
        //CONTRUCTOR
        private readonly MyContext context;
        public OldAccountRepository(MyContext context)
        {
            this.context = context;
        }

        public int Delete(string NIK) //DELETE
        {
            var entity = context.Accounts.Find(NIK);//MENCARI DATA
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

        public IEnumerable<Account> Get() //READ OR MENAMPILKAN
        {
            return context.Accounts.ToList();
        }

        public Account Get(string NIK)//REFERENSI kemarin CEK
        {
            var accountByNIK = context.Accounts.Find(NIK);
            return accountByNIK;
        }

        public int Insert(Account account)
        {
            var countRow = this.Get().Count();
            var year = DateTime.Now.ToString("yyyy");

            string maxNIK = context.Accounts.Max(e => e.NIK);
            int newNIK = Convert.ToInt32(maxNIK);
            newNIK += 1;
            string stringNIK = Convert.ToString(newNIK).Remove(0, 6);
                if (countRow == 0)
                {
                    account.NIK = $"{year}00{countRow + 1}";
                }
                else
                {
                    account.NIK = $"{year}00{stringNIK}";
                }
                context.Accounts.Add(account);
            var result = context.SaveChanges();
            return result;
        }

        public int Update(Account account) //UPDATE
        {
            context.Entry(account).State = EntityState.Modified;

            var result = context.SaveChanges();
            return result;
        }
    }
}
