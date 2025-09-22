using Glossary.Application.Interfaces;
using Glossary.Domain.Dto;
using Glossary.Domain.Entities.Spec;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Application.Terms.Comands
{
    public record PublishTermComand(Guid  TermId) : IRequest<bool>;


    public class PublishTermmHandler : IRequestHandler<PublishTermComand, bool>
    {
        private readonly ITermRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUser _auth;
        private readonly IPublishableSpecification _spec;

        public PublishTermmHandler(ITermRepository repo, IUnitOfWork uow, ICurrentUser auth, IPublishableSpecification spec)
        {
            _repo = repo;
            _uow = uow;
            _auth = auth;
            _spec = spec;
        }

        public async Task<bool> Handle(PublishTermComand req, CancellationToken cancellationToken)
        {
            if (!_auth.IsAuthenticated || _auth.UserId == Guid.Empty)
                throw new AuthenticationException("User must be authenticated to publish a term.");

            var term = await _repo.GetByIdAsync(req.TermId, cancellationToken) ?? throw new KeyNotFoundException("Term not found.");
            term.Publish(_spec);
            await _uow.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
