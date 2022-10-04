using Grpc.Core;
using MediatR;
using Microservices.Application.Queries.Products.FindProductById;
using Microservices.Grpc;

namespace Microservices.Api.Services
{
    public class ProductService : ProductsService.ProductsServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IMediator mediator
            //, ILogger<ProductService> logger
            )
        {
            _mediator = mediator;
            //_logger = logger;
        }

        public override async Task<Product> GetProductById(ProductRowIdFilter request, ServerCallContext context)
        {
            return await _mediator.Send(new FindProductByIdQuery(request));
            //return new ProductResponse()
            //{
            //    Description = "",
            //    Name = "",
            //    Price = 0,
            //    ProductId = 0
            //};
        }

        //public override async  Task<>
    }
}