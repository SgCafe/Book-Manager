using BookManager.Application.Services.Books;
using BookManager.Application.Services.Loans;
using BookManager.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace BookManager.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBooksServices, BooksServices>();
            services.AddScoped<ILoansServices, LoansServices>();
            services.AddScoped<IUsersServices, UsersServices>();

            return services;
        }
    }
}
