using AutoMapper;
using MediatR;
using Microservices.Application.Helpers;
using Microservices.Application.Helpers.PaginationHelper;
using Microservices.Domain.Entities;
using Microservices.Domain.Interfaces;
using Microservices.Grpc;

namespace Microservices.Application.Queries.Products.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProducsQuery, ProductPagedList>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUnitOfWork iUnitOfWork, IMapper mapper)
        {
            _iUnitOfWork = iUnitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductPagedList> Handle(GetAllProducsQuery request, CancellationToken cancellationToken)
        {
            var productsPagedlist = new ProductPagedList();

            var filterName = request.GetAllProductRequest.Name;

            var pagedEntityResult = await _iUnitOfWork.ProductRepository
                                           .GetAllAsQueryAsync()
                                           .WhereIf(!filterName.IsNullOrWhiteSpace(), r => r.Name.ToLower().Equals(filterName.ToLower()) || r.Category.ToLower().Equals(filterName.ToLower()))
                                           .SortBy(request.GetAllProductRequest.SortBy)
                                           .GetPaged(request.GetAllProductRequest.PageNumber,
                                                   request.GetAllProductRequest.PageSize);

            MapReturnToPaged(pagedEntityResult, productsPagedlist);

            return productsPagedlist;
        }

        private void MapReturnToPaged(PagedResult<ProductEntity> from, ProductPagedList to)
        {
            to.PageNumber = from.CurrentPage;
            to.PageSize = from.PageSize;
            to.TotalItems = from.TotalItems;
            to.TotalPages = from.TotalPages;
            to.Items.AddRange(_mapper.Map<List<Product>>(from.Items));
        }

        //public async Task<ProductPagedList> Handle(GetAllProducsRequest request, CancellationToken cancellationToken)
        //{
        //    var filterName = request.GetAllProductRequest.Name;

        //    var query = _iUnitOfWork.ProductRepository
        //                                   .GetAllAsQueryAsync()
        //                                   .WhereIf(!filterName.IsNullOrWhiteSpace(),
        //                                     r => r.Name.ToLower().Equals(filterName.ToLower()) ||
        //                                     r.Category.ToLower().Equals(filterName.ToLower()));

        //    var total = await query.CountAsync();

        //    var items = await query.SortBy(request.GetAllProductRequest.SortBy)
        //                           .PageBy(request)
        //                           .ToListAsync();

        //    var productPagedList = new ProductPagedList
        //    {
        //        TotalItems = total,
        //        PageNumber = request.PageNumber,
        //        PageSize = request.PageSize
        //    };

        //    productPagedList.Items.AddRange(_mapper.Map<List<Product>>(items));

        //    return productPagedList;
        //}
    }
}
