using Microservices.Domain.Interfaces;
using Microservices.Infrastructure.Data.Context;
using Microservices.Infrastructure.Data.Repositories;

namespace Microservices.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public ProductRepository Products => new(_dbContext);

        public IProductRepository ProductRepository => new ProductRepository(_dbContext);

        public async Task<bool> Commit()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}