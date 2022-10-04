using AutoMapper;
using MediatR;
using Microservices.Application.Exceptions;
using Microservices.Domain.Interfaces;
using Microservices.Grpc;

namespace Microservices.Application.Queries.Products.FindProductById
{
    public class FindProductByIdQueryHandler : IRequestHandler<FindProductByIdQuery, Product?>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;

        public FindProductByIdQueryHandler(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Product?> Handle(FindProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _iUnitOfWork.ProductRepository.GetAsync(request.GetProductRequest.ProductId);

            if (result == null)
            {
                throw new EntityNotFoundException($"No Product found for Id {request.GetProductRequest.ProductId}");
                //throw new RpcException(new Status(StatusCode.Internal, $"No Product found for Id {request.GetProductRequest.ProductId}"));
            }

            return _mapper.Map<Product>(result);
        }
    }
}