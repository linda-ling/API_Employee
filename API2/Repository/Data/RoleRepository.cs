using API2.Context;
using API2.Models;
using API2.Repository.Interface;

namespace API2.Repository.Data
{
    public class RoleRepository: GeneralRepository<MyContext, Role, int>
    {
        public RoleRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
