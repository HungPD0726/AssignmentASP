using ASPAssignment.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
namespace ASPAssignment
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
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "FuHouseFinder API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });

            builder.Services.AddDbContext<FuHouseFinderContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ASPAssignment.DataAccess.Repositories.IHouseRepository, ASPAssignment.DataAccess.Repositories.HouseRepository>();
            builder.Services.AddScoped<ASPAssignment.DataAccess.Repositories.IUserRepository, ASPAssignment.DataAccess.Repositories.UserRepository>();
            builder.Services.AddScoped<ASPAssignment.DataAccess.Repositories.IAdminRepository, ASPAssignment.DataAccess.Repositories.AdminRepository>();
            builder.Services.AddScoped<ASPAssignment.DataAccess.Repositories.ILandlordRepository, ASPAssignment.DataAccess.Repositories.LandlordRepository>();
            builder.Services.AddScoped<ASPAssignment.DataAccess.Repositories.IRefreshTokenRepository, ASPAssignment.DataAccess.Repositories.RefreshTokenRepository>();

            builder.Services.AddScoped<ASPAssignment.Business.IHouseService, ASPAssignment.Business.HouseService>();
            builder.Services.AddScoped<ASPAssignment.Business.IAuthService, ASPAssignment.Business.AuthService>();
            builder.Services.AddScoped<ASPAssignment.Business.ITokenService, ASPAssignment.Business.TokenService>();
            builder.Services.AddScoped<ASPAssignment.Business.IAdminService, ASPAssignment.Business.AdminService>();
            builder.Services.AddScoped<ASPAssignment.Business.ILandlordService, ASPAssignment.Business.LandlordService>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
