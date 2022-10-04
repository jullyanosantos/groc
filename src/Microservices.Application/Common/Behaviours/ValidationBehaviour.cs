using FluentValidation;
using Grpc.Core;
using MediatR;

namespace Microservices.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationsResults = await Task.WhenAll(
                _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

            var failure = validationsResults.Where(r => r.Errors.Any())
                                            .SelectMany(r => r.Errors)
                                            .FirstOrDefault();

            if (failure is not null)
                throw new ValidationException(failure.ErrorMessage);
            //throw new RpcException(new Status(StatusCode.Internal, failure.ErrorMessage));
        }

        return await next();

    }
}