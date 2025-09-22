using Glossary.Domain.Dto;
using Glossary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Glossary.Application.Interfaces
{
    public interface ITermRepository
    {
      
        Task<bool> ExistsWithNameAsync(string name, CancellationToken ct);
        void Add(Term term);

        IQueryable<Term> GetAll();

        Task<Term?> GetByIdAsync(Guid TermId, CancellationToken ct);

    }
}
