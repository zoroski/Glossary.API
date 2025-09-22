using MediatR;
using System.Security.Authentication;
using Glossary.Application.Common.CQRS;
using Glossary.Application.Interfaces;

namespace Glossary.Application.Common.Behaviors
{
    public sealed class AuthorizationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ICurrentUser _user;

        public AuthorizationBehavior(ICurrentUser user) => _user = user;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct)
        {
            if (request is IQuery<TResponse>)
                return await next();

            if ((request is ICommand || request is ICommand<TResponse>) && !_user.IsAuthenticated)
                throw new AuthenticationException("User must be authenticated.");

            return await next();
        }
    }
}
