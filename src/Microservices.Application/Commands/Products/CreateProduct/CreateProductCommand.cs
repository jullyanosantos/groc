using MediatR;
using Microservices.Grpc;

namespace Microservices.Application.Commands.Products.CreateProduct
{
    public class CreateProductCommand : IRequest<Product>
    {
        public CreateProductCommand(Product productRequest)
        {
            GetProductRequest = productRequest;
        }
        public Product GetProductRequest { get; set; }
    }
}