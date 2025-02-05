using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SeuGilbertoBot.Data;
using SeuGilbertoBot.Repositories;

namespace SeuGilbertoBot.Configurations
{
    public static class ServiceConfig
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, string connectionString)
        {
            // 🔹 Configuração do banco de dados (SQLite ou MySQL)
            services.AddDbContext<BotDbContext>(options =>
                options.UseSqlite(connectionString)); // Altere para UseMySql() se precisar usar MySQL

            // 🔹 Injeção de dependências dos repositórios
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
