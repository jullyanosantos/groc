using MediatR;
using Microservices.Application.Helpers;
using Microservices.Grpc;

namespace Microservices.Application.Queries.Products.GetAllProducts
{
    public class GetAllProducsQuery : IRequest<ProductPagedList>
    {
        public GetAllProducsQuery(GetAllProductsRequest getAllProductRequest)
        {
            GetAllProductRequest = getAllProductRequest;
            getAllProductRequest.SortBy = getAllProductRequest.SortBy.IsNullOrWhiteSpace()
                        ? "name"
                        : getAllProductRequest.SortBy;
        }

        public GetAllProductsRequest GetAllProductRequest { get; set; }
    }
}