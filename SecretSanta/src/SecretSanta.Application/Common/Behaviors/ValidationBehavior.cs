using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ValidationException = SecretSanta.Application.Common.Exceptions.ValidationException;

namespace SecretSanta.Application.Common.Behaviors
{
    /// <summary>
    ///     Run validation logic before handling a <see cref="MediatR" /> <see cref="IRequest" />.
    /// </summary>
    /// <typeparam name="TRequest">The type of the <see cref="MediatR" /> <see cref="IRequest" />.</typeparam>
    /// <typeparam name="TResponse">The expected type of the response on success.</typeparam>
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        ///     Run all validators and raise a <see cref="Exceptions.ValidationException" /> if any validations fail.
        /// </summary>
        /// <param name="request">The request to handle.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <param name="next">The next handler to execute.</param>
        /// <exception cref="Exceptions.ValidationException">Raised when any o the validations fail.</exception>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults =
                    await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}