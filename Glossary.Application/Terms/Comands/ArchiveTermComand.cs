using Glossary.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Application.Terms.Comands
{

    public record ArchiveTermComand(Guid TermId) : IRequest<bool>;

    public class ArchiveTermHendler : IRequestHandler<ArchiveTermComand, bool>
    {
        private readonly ITermRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUser _auth;
        public ArchiveTermHendler(ITermRepository repo, IUnitOfWork uow, ICurrentUser auth)
        {
             _repo = repo;
             _uow = uow;
             _auth = auth;
        }

        public async Task<bool> Handle(ArchiveTermComand request, CancellationToken cancellationToken)
        {
            if (!_auth.IsAuthenticated || _auth.UserId == Guid.Empty)
                throw new AuthenticationException("User must be authenticated to publish a term.");

            var term = await _repo.GetByIdAsync(request.TermId, cancellationToken) ?? throw new KeyNotFoundException("Term not found.");
            term.Archive();
            await _uow.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
