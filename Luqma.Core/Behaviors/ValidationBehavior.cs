using FluentValidation;
using Luqma.Core.Exceptions;
using MediatR;

namespace Luqma.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
            => this.validators = validators;
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResult = await Task.WhenAll(validators.Select(valid => valid.ValidateAsync(context, cancellationToken)));
                var failures = validationResult.SelectMany(error => error.Errors)
                                               .Where(error => error is not null)
                                               .ToList();
                if (failures.Count != 0)
                    throw new CustomValidationException(failures);
            }
            return await next();
        }
    }
}
