using API2.Context;
using API2.Models;
using API2.Repository.Interface;

namespace API2.Repository.Data
{
    public class EducationRepository : GeneralRepository<MyContext, Education, int>
    {
        public EducationRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
