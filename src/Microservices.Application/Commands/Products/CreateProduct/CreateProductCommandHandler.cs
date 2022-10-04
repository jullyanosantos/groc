using AutoMapper;
using MediatR;
using Microservices.Domain.Entities;
using Microservices.Domain.Interfaces;
using Microservices.Grpc;

namespace Microservices.Application.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<ProductEntity>(request.GetProductRequest);

            var result = await _iUnitOfWork.ProductRepository.AddAsync(productEntity);
            await _iUnitOfWork.Commit();

            return _mapper.Map<Product>(result);
        }
    }
}