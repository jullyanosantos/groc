using AutoMapper;
using Grpc.Core;
using MediatR;
using Microservices.Application.Commands.Products.CreateProduct;
using Microservices.Application.Queries.Products.FindProductById;
using Microservices.Application.Queries.Products.GetAllProducts;
using Microservices.Grpc;

namespace Microservices.GrpcServices
{
    public class ProductService : ProductsService.ProductsServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(
            IMediator mediator,
            ILogger<ProductService> logger,
            IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<Product> GetProductById(ProductRowIdFilter request, ServerCallContext context)
        {
            _logger.LogInformation("GetProductById: ", request.ProductId);

            return await _mediator.Send(new FindProductByIdQuery(request)); ;
        }

        public override async Task<ProductPagedList?> GetAllProducts(GetAllProductsRequest request, ServerCallContext context)
        {
            _logger.LogInformation("GetAllProducts: ", request);

            var products = await _mediator.Send(new GetAllProducsQuery(request)); ;

            return products;
        }

        public override async Task<Product> Post(Product request, ServerCallContext context)
        {
            _logger.LogInformation("Post: ", request);

            var product = await _mediator.Send(new CreateProductCommand(request)); ;

            return product;
        }
    }
}