using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using SMS.API.Hubs;
using SMS.Authentication.Setting_Models;
using SMS.BLL.Auto_Mapper;
using SMS.BLL.Services.Contracts;
using SMS.DAL;
using SMS.DAL.Data.Database_Context;
using SMS.DAL.Repositories;
using SMS.DAL.Repositories.Contracts;
using SMS.Tools.Tools;
using SMS.WebTools.Tools;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace SMS.API
{
    public class Startup : StartupTemplate
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });/*.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.Load("SMS.BLL")));*/

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAutoMapper(Assembly.Load("SMS.BLL"), Assembly.Load("SMS.DAL"), Assembly.Load("SMS.Authentication"));
            services.AddInjectibles();
            services.AddHttpContextAccessor();

            services.AddDbContext<CoreDbContext>(options =>
            {
                var _connString = Configuration.GetConnectionString("TestConnection4");
                options.UseNpgsql(_connString);
            });

            services.Configure<JwtSettings>(Configuration!.GetSection("JwtSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration.GetSection("JwtSettings")["Issuer"],
                    ValidAudience = Configuration.GetSection("JwtSettings")["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Key"]))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Binded Users Only", policyBuilder =>
                {
                    policyBuilder.RequireClaim("userType", new[] { "Student", "Teacher" });
                });
            });

            services.AddSignalR();
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
            });
        }

        public override void ConfigureApp(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });


            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
