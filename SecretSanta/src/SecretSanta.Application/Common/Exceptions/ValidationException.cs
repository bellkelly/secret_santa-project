using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace SecretSanta.Application.Common.Exceptions
{
    /// <summary>
    ///     An exception that holds a Dictionary of error keys to error messages.
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}