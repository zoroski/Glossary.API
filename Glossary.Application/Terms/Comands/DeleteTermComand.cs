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

    public record DeleteTermComand(Guid TermId) : IRequest<bool>;

    public class DeleteTermHendler : IRequestHandler<DeleteTermComand, bool>
    {
        private readonly ITermRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUser _auth;
        public DeleteTermHendler(ITermRepository repo, IUnitOfWork uow, ICurrentUser auth)
        {
             _repo = repo;
             _uow = uow;
             _auth = auth;
        }

        public async Task<bool> Handle(DeleteTermComand request, CancellationToken cancellationToken)
        {
            if (!_auth.IsAuthenticated || _auth.UserId == Guid.Empty)
                throw new AuthenticationException("User must be authenticated to publish a term.");

            var term = await _repo.GetByIdAsync(request.TermId, cancellationToken) ?? throw new KeyNotFoundException("Term not found.");
            term.Delete(_auth.UserId);
            _repo.Delete(term);
            await _uow.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
