using API2.Context;
using API2.Models;
using API2.Repository.Interface;

namespace API2.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<MyContext, Profiling, string>
    {
        public ProfilingRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
