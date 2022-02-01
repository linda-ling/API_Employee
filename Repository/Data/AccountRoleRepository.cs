using API2.Context;
using API2.Models;
using API2.Repository.Interface;

namespace API2.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, string>
    {
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
