using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Microservices.Application.Exceptions
{
    public class ExceptionInterceptor : Interceptor
    {
        private readonly ILogger<ExceptionInterceptor> _logger;

        public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            //catch (CustomWeatherException weatherException)
            //{
            //    var httpContext = context.GetHttpContext();
            //    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            //    throw new RpcException(new Status(StatusCode.InvalidArgument, "weather is terrible"));
            //}
            catch (EntityNotFoundException e)
            {
                var httpContext = context.GetHttpContext();
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                throw new RpcException(new Status(StatusCode.NotFound, e.Message));
            }
            catch (Exception exception)
            {
                var httpContext = context.GetHttpContext();
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                throw new RpcException(new Status(StatusCode.Internal, exception.Message));
            }
        }
    }
}