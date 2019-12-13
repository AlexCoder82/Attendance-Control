using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Services;
using AttendanceControl.API.DataAccess;
using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using AttendanceControl.API.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.CrossCutting.IocRegister
{
    public static class IOCRegister
    {
        //DBContext IOC
        public static IServiceCollection AddDBContext(this IServiceCollection services, string connection)
        {
            services.AddScoped<IAttendanceControlDBContext, AttendanceControlDBContext>();
            services.AddDbContext<AttendanceControlDBContext>(options =>
                    options.UseMySql(connection)
                );

            return services;
        }

        //Application services IOC 
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAdminService, AdminService>();

            return services;
        }

        //Repositories IOC 
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAdminRepository, AdminRepository>();

            return services;
        }
    }
}
