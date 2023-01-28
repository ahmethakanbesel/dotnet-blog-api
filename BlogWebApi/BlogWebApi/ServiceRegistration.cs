using Microsoft.Extensions.DependencyInjection;

namespace BlogWebApi
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddDependecies(this IServiceCollection services)
		{
			services.AddTransient(serviceProvider => new PostDbContext());

			return services;
		}
	}
}