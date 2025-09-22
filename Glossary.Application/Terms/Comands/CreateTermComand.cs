using Glossary.Application.Interfaces;
using Glossary.Domain.Entities;
using MediatR;
using System.Security.Authentication;


namespace Glossary.Application.Terms.Comands
{
    public class CreateTermHandler : IRequestHandler<CreateTermComand, Guid>
    {
        private readonly ITermRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUser _auth;

        public CreateTermHandler(ITermRepository repo, IUnitOfWork uow, ICurrentUser auth)
        {
            _repo = repo; 
            _uow = uow;
            _auth = auth;
        }

        public async Task<Guid> Handle(CreateTermComand req, CancellationToken ct)
        {
            if (!_auth.IsAuthenticated || _auth.UserId == Guid.Empty)
                throw new AuthenticationException("User must be authenticated to create a term.");

            var term = Term.Create(req.Name, req.Definition, _auth.UserId);
            _repo.Add(term);
            await _uow.SaveChangesAsync(ct);
            return term.Id;
        }
    }
}
