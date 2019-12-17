using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceControl.API.CrossCutting.IocRegister;
using AttendanceControl.API.DataAccess;
using AttendanceControl.API.DataAccess.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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
            /// Register Application services 

            IOCRegister.AddServices(services);

            /// Register Entity Framework db context

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            IOCRegister.AddDBContext(services, connectionString);



            /// Register data repositories

            IOCRegister.AddRepositories(services);

            /// Register Controllers

            services.AddControllers();

            //JWT AUTHORIZATION
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults
                    .AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();
            });
            //JWT AUTHENTIFICATION
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidIssuer = "viva la pepa",
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding
                            .ASCII
                            .GetBytes("AAfjoègfjèjf`jeof`jeòfjpo`jfo51561f456a4f"))
                    };
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile("Logs/Log-{Date}.txt");

            app.UseRouting();

            //app.UseAuthorization();

            //HTTPS
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //MVC
            // app.UseMvc();
        }
    }
}
