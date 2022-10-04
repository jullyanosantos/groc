using MediatR;
using Microservices.Application.Queries.Products.FindProductById;
using Microservices.Grpc;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new ProductRowIdFilter { ProductId = id };

            var response = await _mediator.Send(new FindProductByIdQuery(request));

            return new OkObjectResult(response);
        }
    }
}