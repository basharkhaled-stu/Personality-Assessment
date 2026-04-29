
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PersonalityAssessment.Api.Hosting;
using PersonalityAssessment.Api.Middleware;
using PersonalityAssessment.Api.Services;
using PersonalityAssessment.Application;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Infrastructure;
using PersonalityAssessment.Infrastructure.Data;
using PersonalityAssessment.Infrastructure.Implemention;
using PersonalityAssessment.Infrastructure.User;
using System.Security.Claims;
using System.Text;
///stu200
namespace PersonalityAssessment.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructureServices(
                 builder.Configuration.GetConnectionString("DefaultConnection")!);

            builder.Services.AddSingleton<IAdminRequestMetrics, AdminRequestMetricsService>();

            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddScoped<IIdentityUser, IdentityUser1>();
            builder.Services.AddScoped<IGoogleIdTokenValidator, GoogleIdTokenValidator>();
            builder.Services.AddScoped<PasswordHasher<AppUser>>();
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddHostedService<AdminRoleSeedHostedService>();

            builder.Services.AddApplicationServices();

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"]!;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
                        var userId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
                        if (string.IsNullOrEmpty(userId))
                            return;
                        var user = await userManager.FindByIdAsync(userId);
                        if (user == null)
                            return;
                        var roles = await userManager.GetRolesAsync(user);
                        if (context.Principal?.Identity is ClaimsIdentity identity)
                        {
                            foreach (var role in roles)
                            {
                                if (!identity.HasClaim(ClaimTypes.Role, role))
                                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                            }
                        }
                    }
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "API", Version = "v1" });

                // 🔐 إضافة JWT Auth
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token.\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "redis-11914.c73.us-east-1-2.ec2.cloud.redislabs.com:11914,password=HkCPfkEtFIlD7SVxHhSClt94nG7c5uYK";
            });
            builder.Services.AddScoped<ICacheService, RedisCacheService>();
            var app = builder.Build();
            app.UseMiddleware<AdminRequestCounterMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseStaticFiles();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseRouting();


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });



            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
