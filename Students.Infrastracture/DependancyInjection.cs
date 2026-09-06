using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Students.Application.Interfaces;
using Students.Application.Services;
using Students.Infrastracture.Data;
using Students.Infrastracture.Repositories;
using Students.Infrastracture.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Students.Infrastracture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StudentsDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
