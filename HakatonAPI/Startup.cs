using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hakaton.Infrastructure;
using Hakaton.Application.Common.Interfaces;
using Hakaton.Infrastructure.Services;
using Hakaton.Application;
using Hakaton.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hakaton.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace HakatonAPI
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
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddPersistance(Configuration);
            services.AddControllers();
            services.AddCors(o => o.AddPolicy("GlobalPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();


            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HakatonAPI", Version = "v1" });
            });
            services.AddScoped<ITokenService, TokenService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;
            })
               .AddRoles<AppRole>()
               .AddRoleManager<RoleManager<AppRole>>()
               .AddSignInManager<SignInManager<User>>()
               .AddRoleValidator<RoleValidator<AppRole>>()
               .AddEntityFrameworkStores<AppDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hakaton v1"));
            }
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hakaton v1"));
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("GlobalPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
