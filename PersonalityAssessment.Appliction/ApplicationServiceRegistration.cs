using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PersonalityAssessment.Application.Features.AssessmentStatuses.Commands.Validators;
using PersonalityAssessment.Application.Features.AssessmentStatuses.Mappings;
using PersonalityAssessment.Application.Features.AssessmentStatuses.Queries.Handlers;
using PersonalityAssessment.Application.Features.Behaviors;
using PersonalityAssessment.Application.Services;

namespace PersonalityAssessment.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(GetAllAssessmentStatusesQueryHandler).Assembly));

            // AutoMapper
            services.AddAutoMapper(typeof(AssessmentStatusProfile).Assembly);
            services.AddValidatorsFromAssemblyContaining<CreateAssessmentStatusValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IGoogleLoginAppService, GoogleLoginAppService>();

            services.AddScoped<IAdminModuleAppService, AdminModuleAppService>();

            services.AddScoped<IPersonalityCalculationService, PersonalityCalculationService>();

            return services;
        }
    }
}
