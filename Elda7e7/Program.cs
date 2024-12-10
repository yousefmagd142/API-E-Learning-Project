
using BusinessLayer.Repository;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Elda7e7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"),
                b => b.MigrationsAssembly("DataAccessLayer"))); // Ensure this matches your migrations project

            //register usermanager ,rolemanager ==> userstore, rolestor
            builder.Services.AddIdentity<UserAccount, IdentityRole>().
                AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // [authorize]
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//will give u unauthorize not moving u to another action
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => // verified key 
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:IssuerIP"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:AudienceIP"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecritKey"]))
                };
            });


            builder.Services.AddScoped<IUserAccountRegisteration, UserAccountRegisteration>();
            builder.Services.AddScoped<ILessonService, LessonService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IProfileDataService, ProfileDataService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseCors("AllowAllOrigins");

            //app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
