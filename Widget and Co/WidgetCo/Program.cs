using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Widget_and_Co.Logic;
using Widget_and_Co.Logic.Interfaces;
using Widget_and_Co.Model;
using Widget_and_Co.Repository;
using Widget_and_Co.Repository.Interfaces;

namespace Widget_and_Co
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(Configure)
                .ConfigureOpenApi()
                .Build();
            host.Run();
        }

        static void Configure(HostBuilderContext Builder, IServiceCollection Services)
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString")!;

            Services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(connectionString);
                option.EnableSensitiveDataLogging();
            });

            Services.AddScoped<IOrderRepository, OrderRepository>();
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IProductRepository, ProductRepositroy>();
            Services.AddScoped<IForumRepository, ForumRepository>();

            Services.AddAutoMapper(typeof(Program));

            Services.AddScoped<IOrderLogic, OrderLogic>();
            Services.AddScoped<IUserLogic, UserLogic>();
            Services.AddScoped<IProductLogic, ProductLogic>();
            Services.AddScoped<IForumLogic, ForumLogic>();
        }
    }
}
