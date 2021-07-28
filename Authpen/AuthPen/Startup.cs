using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

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
using AuthPen.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AuthPen.Services;
using System.Text;

namespace AuthPen
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisismySecretKey"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,

                ValidateIssuer = true,
                ValidIssuer = "UserWebApi",

                ValidateAudience = true,
                ValidAudience = "CustomerWebApi"
            });
            services.AddControllers();
            services.AddDbContext<PensionerDbContext>(op=>op.UseSqlServer(Configuration.GetConnectionString("PenDbContext")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthPen", Version = "v1" });
            });
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthPen v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
