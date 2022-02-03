using API2.Context;
using API2.Models;
using API2.Repository.Interface;

namespace API2.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        public UniversityRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
