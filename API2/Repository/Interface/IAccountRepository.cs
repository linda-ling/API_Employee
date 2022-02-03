using API2.Models;
using System.Collections.Generic;

namespace API2.Repository.Interface
{
    public interface IAccountRepository
    {
        IEnumerable<Account> Get();

        Account Get(string NIK);
        int Insert(Account account);
        int Update(Account account);
        int Delete(string NIK);
    }
}
