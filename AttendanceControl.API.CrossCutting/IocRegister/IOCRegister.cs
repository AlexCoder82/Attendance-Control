using AttendanceControl.API.Application.Auth;
using AttendanceControl.API.Application.Contracts.IAuth;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Services;
using AttendanceControl.API.DataAccess;
using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using AttendanceControl.API.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


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
            services.AddTransient<IShiftService, ShiftService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ICycleService, CycleService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<ISchoolClassService, SchoolClassService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IAbsenceService, AbsenceService>();
            services.AddTransient<ICallListService, CallListService>();

            return services;
        }

        //Repositories IOC 
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IShiftRepository, ShiftRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<ICycleRepository, CycleRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<ISchoolClassRepository, SchoolClassRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDatabaseTransaction, DatabaseTransaction>();
            services.AddTransient<IAbsenceRepository, AbsenceRepository>();
            
            


            return services;
        }
    }
}
