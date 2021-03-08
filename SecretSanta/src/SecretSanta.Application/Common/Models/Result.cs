using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Application.Common.Models
{
    /// <summary>
    ///     Represents the result of an action.
    ///     Can be either a Success or a Failure with Errors.
    /// </summary>
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new(true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new(false, errors);
        }
    }
}