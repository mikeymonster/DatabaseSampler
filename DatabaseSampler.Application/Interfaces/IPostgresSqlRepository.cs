
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseSampler.Application.Models;

namespace DatabaseSampler.Application.Interfaces
{
    public interface IPostgresSqlRepository
    {
        Task<int> AddStudentAsync(Student student);
        Task<IList<Student>> GetStudentsAsync();
    }
}
