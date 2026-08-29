using System;
using System.Collections.Generic;
using System.Text;

namespace Students.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAllAsync(params string[] includes);

        Task<T?> GetByIdAsync(int id, params string[] includes);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<int> SaveChangesAsync();
    }
}
