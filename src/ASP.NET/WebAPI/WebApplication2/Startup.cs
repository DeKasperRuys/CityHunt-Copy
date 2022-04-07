using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Context;
using WebApplication2.IService;
using WebApplication2.Models;
using WebApplication2.Service;
using System.Data.Entity;
using System.Configuration;
using BusinessLogicLayer.Extensions;
using DataAccessLayer.Extensions;
//using Microsoft.AspNetCore.Authentication.Google;

namespace WebApplication2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; 
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .RegisterServices()
                .RegisterORM(Configuration.GetConnectionString("DefaultConnection"))
                .RegisterRepositories();


            services.AddMvc();
            // 3 layer

            services.AddControllers();

            //toegevoegd voor login

            services.AddScoped<UserService>();

            //toegevoegd voor SQL server
            /*
            services.AddDbContext<DBFirstContext>(
               options => options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")
               )
           );
           */

            //var appSettings = System.Configuration.ConfigurationManager.AppSettings.Keys.ToString();

            services.AddDbContext<DBFirstContext>(options => {
                options.UseSqlServer("server=(localdb)\\mssqllocaldb;database=DB114;trusted_connection=true;");
            });

            string key = Configuration.GetSection("Jwt").ToString();

            services.AddSingleton<IJwtAuthManager>(new JwtAuthManager(key));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });
            
            /*
            services.AddAuthentication( options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            } ).AddCookie(options => { options.LoginPath = "/api/users"; })
                                .AddGoogle(options =>
                                {
                                    IConfigurationSection googleAuthNSection =
                                        Configuration.GetSection("Authentication:Google");

                                    options.ClientId = googleAuthNSection["183065994485-mscjmdjg2thqj8ms10ujj16frogegjoe.apps.googleusercontent.com"];
                                    options.ClientSecret = googleAuthNSection["I9E7Z572nEMo6iQzpYh1qnpI"];
                                    options.SignInScheme = IdentityConstants.ExternalScheme;
                                
                                   
                                })
                                ;
                                */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DBFirstContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // init DB
            DBInitialiser.Initialize(context);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
