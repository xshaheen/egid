using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = EGID.Application.Common.Exceptions.ValidationException;

namespace EGID.Application.Common.Behavior
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);

            List<ValidationFailure> failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(results => results.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any()) throw new ValidationException(failures);

            return next();
        }
    }
}