using FluentValidation;
using MediatR;

namespace ETradeAPI.Application.Behaviors
{
    public class ValidationBehaivor<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where 
            TRequest : class
    {
        public async Task<TResponse> Handle(
            TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)))
                    .ConfigureAwait(false);

                var failures = validationResults
                    .Where(x => x.Errors.Count > 0)
                    .SelectMany(x => x.Errors)
                    .ToList();

                if (failures.Count > 0)
                {
                    throw new ValidationException(failures);
                }
            }
            return await next().ConfigureAwait(false);
        }
    }
}
