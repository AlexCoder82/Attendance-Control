using System;
using System.Text;
using AttendanceControl.API.CrossCutting.IocRegister;
using AttendanceControl.API.Filters;
using AttendanceControl.API.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace AttendanceControl.UnitTests
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Agrega el filtro de validación de datos indicandole el paquete donde se encuentran 
            //todas las clases de validacion de datos
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(new ValidationFilter());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CycleValidator>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            /// Registra el contexto de la base de datos
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            IOCRegister.AddDBContext(services, connectionString);

            /// Registra todos los servicios
            IOCRegister.AddServices(services);

            /// Registra todos los repositorios
            IOCRegister.AddRepositories(services);

            /// Agrega los controladores
            services.AddControllers();
         

            //Agrega el cors para permitir peticiones desde el mismo ip local
            services.AddCors(Options =>
            {
                Options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials()
                       .WithExposedHeaders(HeaderNames.ContentDisposition)
                       .WithOrigins("http://192.168.0.102:4200", "http://192.168.0.100:4200")
                       .Build();
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            // CORS 
            app.UseCors("EnableCORS");

            //Logs
            loggerFactory.AddFile("Logs/Log-{Date}.txt");

            //Filtro de errores
            app.UseExceptionHandler("/error");

            
            //Routing
            app.UseRouting();
            //Endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //HTTPSuhi
            app.UseHttpsRedirection();
            //MVC
            app.UseMvc();
        }
    }
}
