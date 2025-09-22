using Glossary.Application.Interfaces;
using Glossary.Domain.Entities;
using MediatR;


namespace Glossary.Application.Terms.Comands
{
    public class CreateTermHandler : IRequestHandler<CreateTermComand, Guid>
    {
        private readonly ITermRepository _repo;
        private readonly IUnitOfWork _uow;

        public CreateTermHandler(ITermRepository repo, IUnitOfWork uow)
        {
            _repo = repo; _uow = uow;
        }

        public async Task<Guid> Handle(CreateTermComand req, CancellationToken ct)
        {
            var term = Term.Create(req.Name, req.Definition, Guid.NewGuid());
            _repo.Add(term);
            await _uow.SaveChangesAsync(ct);
            return term.Id;
        }
    }
}
