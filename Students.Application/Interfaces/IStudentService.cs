using Students.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Students.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> GetAllAsync(params string[] includes);

        Task<StudentModel?> GetByIdAsync(int id, params string[] includes);

        Task AddAsync(StudentModel entity);

        Task UpdateAsync(StudentModel entity);

        Task DeleteAsync(int id);
    }
}
