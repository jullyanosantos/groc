using FluentValidation;
using Microservices.Application.Queries.Products.FindProductById;

namespace Microservices.Application.Validators
{
    public class FindProductValidator : AbstractValidator<FindProductByIdQuery>
    {
        public FindProductValidator()
        {
            RuleFor(command => command.GetProductRequest.ProductId)
                .GreaterThan(0)
                .WithMessage("O campo productId deve ser maior que 0.");

        }
    }
}