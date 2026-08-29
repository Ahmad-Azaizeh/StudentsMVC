using Microsoft.EntityFrameworkCore;
using Students.Application.Interfaces;
using Students.Infrastracture.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Students.Infrastracture.Repositories
{
    
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly StudentsDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(StudentsDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
