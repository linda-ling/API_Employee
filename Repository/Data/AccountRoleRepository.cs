using API2.Context;
using API2.Models;
using API2.Repository.Interface;
using API2.ViewModel;

namespace API2.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, string>
    {
        private readonly MyContext Context;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.Context = myContext;
        }

        public int SignManager(AccountRoleVM accountRoleVM)
        {
            AccountRole acro = new AccountRole()
            {
                Role_Id = 2,
                NIK = accountRoleVM.NIK
            };
            Context.AccountRoles.Add(acro);
            var result = Context.SaveChanges();

            return result;
        }
    }
}
