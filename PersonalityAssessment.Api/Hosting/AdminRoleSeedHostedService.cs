using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PersonalityAssessment.Api.Hosting
{
    /// <summary>
    /// Ensures the Admin role exists. Assign users to Admin via UserManager (or database) to access admin APIs.
    /// </summary>
    public sealed class AdminRoleSeedHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public AdminRoleSeedHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
