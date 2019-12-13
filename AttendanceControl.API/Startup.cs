using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceControl.API.CrossCutting.IocRegister;
using AttendanceControl.API.DataAccess;
using AttendanceControl.API.DataAccess.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API
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
            /// Register Entity Framework db context

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            IOCRegister.AddDBContext(services, connectionString);

            /// Register Application services 

            IOCRegister.AddServices(services);

            /// Register data repositories

            IOCRegister.AddRepositories(services);

            /// Register Controllers

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
