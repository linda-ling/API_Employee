using API2.Models;
using System.Collections.Generic;

namespace API2.Repository.Interface
{
    public interface IEducationRepository
    {
        IEnumerable<Education> Get();

        Education Get(int Id);
        int Insert(Education education);
        int Update(Education education);
        int Delete(int Id);
    }
}
