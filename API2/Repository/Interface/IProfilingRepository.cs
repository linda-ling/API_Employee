using API2.Models;
using System.Collections.Generic;

namespace API2.Repository.Interface
{
    public interface IProfilingRepository
    {
        IEnumerable<Profiling> Get();

        Employee Get(string NIK);
        int Insert(Profiling profiling);
        int Update(Profiling profiling);
        int Delete(string NIK);
    }
}
