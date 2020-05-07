using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace EGID.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
            => Errors = new Dictionary<string, string[]>();

        public ValidationException(IReadOnlyCollection<ValidationFailure> failures) : this()
        {
            var failureGroups = failures
                .GroupBy(failure => failure.PropertyName, f => f.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}