using Microsoft.EntityFrameworkCore;
using PetShop.DeveloperTesting._02.ServiceLayer.Contracts;
using PetShop.DeveloperTesting.DomainLayer.Context;
using PetShop.DeveloperTesting.ServiceLayer.Contracts;
using PetShop.DeveloperTesting.ServiceLayer.Services;
using RepositoryLayer;

namespace PetShop.DeveloperTesting
{
    public static class DependencyInjection
    {
        public static void InjectPetShopDependencies(this IServiceCollection services)
        {
            var sqlConnectionString = "Server=localhost\\SQLEXPRESS;Database=SummerPractice.PetShop;Trusted_Connection=True;TrustServerCertificate=True;";
            //var sqlConnectionString = builder.Configuration["ConnectionStrings:PetShopConnString"];
            services.AddDbContext<PetsDbContext>(options => options.UseSqlServer(sqlConnectionString));


            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPetCategoriesService, PetCategoriesService>();
        }
    }
}