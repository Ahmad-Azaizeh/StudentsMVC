using Microsoft.EntityFrameworkCore;
using Students.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Students.Infrastracture.Data
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
    }
}
