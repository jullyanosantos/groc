using Microservices.Domain.Entities;
using Microservices.Domain.Interfaces;
using Microservices.Infrastructure.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider, AppDbContext context)
        {
            CreateProducts(serviceProvider);
        }

        private async static void CreateProducts(IServiceProvider serviceProvider)
        {
            var _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            if (!products.Any())
            {
                var productEntity = new ProductEntity
                {
                    Description = "White Rice",
                    Name = "Rice",
                    Category = "Food",
                    Price = 19.50F
                };

                await _unitOfWork.ProductRepository.AddAsync(productEntity);
                await _unitOfWork.Commit();
            }
        }
    }
}