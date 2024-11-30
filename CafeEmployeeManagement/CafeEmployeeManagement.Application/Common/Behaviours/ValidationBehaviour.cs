using FluentValidation;
using MediatR;

namespace CafeEmployeeManagement.Application.Common.Behaviours
{
    //public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //    where TRequest : IRequest<TResponse>
    //{
    //    private readonly IValidator<TRequest> validator;

    //    public ValidationBehaviour(IValidator<TRequest> validator)
    //    {
    //        this.validator = validator;
    //    }

    //    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //    {
    //        if (validator != null)
    //        {
    //            var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //            if (!validationResult.IsValid)
    //            {
    //                // Throw a validation exception or return a custom error if validation fails
    //                throw new FluentValidation.ValidationException(validationResult.Errors);
    //            }
    //        }

    //        // If validation passes, call the next handler in the pipeline
    //        return await next();
    //    }
    //}


    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? Enumerable.Empty<IValidator<TRequest>>();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Validation logic
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
