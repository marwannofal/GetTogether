using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using API.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtnsions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            //connect the database with data entities and data in sqlite <3:
            services.AddDbContext<DataContext>(options =>{ options.UseSqlServer(config.GetConnectionString("DefaultConnection"));});
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));    
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();            
            services.AddSingleton<PresenceTracker>();
            services.AddScoped<LogUserActivity>();
            services.AddSignalR();
            services.AddCors();
            return services;
        }
    }
}