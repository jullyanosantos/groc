using Microservices.Domain.Entities;
using Microservices.Domain.Interfaces;
using Microservices.Infrastructure.Data.Context;
using System.Linq.Expressions;

namespace Microservices.Infrastructure.Data.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        public AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
