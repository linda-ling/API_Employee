using API2.Models;
using System.Collections.Generic;

namespace API2.Repository.Interface
{
    public interface IUniversityRepository
    {
        IEnumerable<University> Get();

        University Get(int Id);
        int Insert(University university);
        int Update(University university);
        int Delete(int Id);
    }
}
