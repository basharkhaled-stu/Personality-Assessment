using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalityAssessment.Core.Interface;
using PersonalityAssessment.Core.Repository;
using PersonalityAssessment.Core.UnitOfWork;
using PersonalityAssessment.Infrastructure.Data;
using PersonalityAssessment.Infrastructure.Implemention;
using PersonalityAssessment.Infrastructure.Repositories;
using PersonalityAssessment.Infrastructure.Repository;
using PersonalityAssessment.Infrastructure.UnitOfWorkes;
namespace PersonalityAssessment.Infrastructure
{
    public static class InfrastructureRegistrations
    {
        public static IServiceCollection AddInfrastructureServices(
             this IServiceCollection services, string connectionString)
        {

            /* services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(connectionString));*/


            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAdminDataService, AdminDataService>();

            services.AddScoped<IUsersAssessmentRepository, UsersAssessmentRepository>();

            return services;
        }
    }
}