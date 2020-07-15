using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Alpaki.Logic.PipelineBehaviours
{
    public class RequestValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>>validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var validations = await Task.WhenAll(_validators
                .Select(x => x.ValidateAsync(context, cancellationToken)));

            var errors = validations.Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .ToList();

            if(errors.Any())
                throw new ValidationException(errors);

            return await next();
        }
    }
}