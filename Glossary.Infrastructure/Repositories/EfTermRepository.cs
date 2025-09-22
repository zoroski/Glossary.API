using Glossary.Application.Interfaces;
using Glossary.Domain.Entities;
using Glossary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Infrastructure.Repositories
{
    public class EfTermRepository : ITermRepository
    {
        private readonly AppDbContext _db;
        public EfTermRepository(AppDbContext db) => _db = db;

        public Task<bool> ExistsWithNameAsync(string name, CancellationToken ct) =>
            _db.Terms.AnyAsync(t => t.Name == name, ct);

        public void Add(Term term) => _db.Terms.Add(term);

        public IQueryable<Term> GetAll() => _db.Terms.AsNoTracking();
    }
}
