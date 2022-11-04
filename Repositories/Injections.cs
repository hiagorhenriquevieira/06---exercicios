using DependencyStore.Repositories.Contracts;
using DependencyStore.Service;
using DependencyStore.Service.Contracts;

namespace DependencyStore.Repositories
{
    public static class Injections
    {
        public static void AddService(this IServiceCollection services){
            services.AddScoped<IBookPaymentService, BookPaymentService>();
        }

        public static void AddRepository(this IServiceCollection services){
            services.AddScoped<IBookRepository, BookRepository>();
        }
    }
}