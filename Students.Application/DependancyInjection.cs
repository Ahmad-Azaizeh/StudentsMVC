using Microsoft.Extensions.DependencyInjection;
using Students.Application.Interfaces;
using Students.Application.Services;
using Students.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Students.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();

            return services;
        }
    }
}
