using BL.Implementations;
using BL.Interfaces;
using BL.Services;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace Web.Extensions
{
    public static class ServiceExtension
    {
        public static void ServicesRegistry(this IServiceCollection services, IConfiguration configuration)
        {
            services.Repositories(configuration);
            //services.Services(configuration);
            services.Database(configuration);
            //services.Misc(configuration);
        }

        public static void Repositories(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IMessageRepo, MessageRepo>();
            services.AddScoped<IChatBotService, ChatBotService>();
        }

        public static void Database(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("cs")));
        }
    }
}
