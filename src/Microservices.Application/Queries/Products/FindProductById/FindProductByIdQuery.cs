using MediatR;
using Microservices.Grpc;

namespace Microservices.Application.Queries.Products.FindProductById
{
    public class FindProductByIdQuery : IRequest<Product>
    {
        public FindProductByIdQuery(ProductRowIdFilter getProductRequest)
        {
            GetProductRequest = getProductRequest;
        }

        public ProductRowIdFilter GetProductRequest { get; set; }
    }
}