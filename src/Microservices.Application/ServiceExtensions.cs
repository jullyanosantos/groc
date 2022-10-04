using FluentValidation;
using MediatR;
using Microservices.Application.Common.Behaviours;
using Microservices.Application.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservices.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddGrpc(
            options =>
            {
                options.Interceptors.Add<ExceptionInterceptor>();
            });

            return services;
        }

        //private static IServiceCollection ConfigureFLuentValidation(this IServiceCollection services)
        //{
        //    services.AddValidator<FindProductValidator>();

        //    return services;
        //}
    }
}