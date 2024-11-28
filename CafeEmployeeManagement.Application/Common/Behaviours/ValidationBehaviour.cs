using FluentValidation;
using MediatR;

namespace CafeEmployeeManagement.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> validator;

        public ValidationBehaviour(IValidator<TRequest> validator)
        {
            this.validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    // Throw a validation exception or return a custom error if validation fails
                    throw new FluentValidation.ValidationException(validationResult.Errors);
                }
            }

            // If validation passes, call the next handler in the pipeline
            return await next();
        }
    }
}
