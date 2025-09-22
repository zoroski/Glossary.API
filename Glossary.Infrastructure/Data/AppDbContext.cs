using Glossary.Domain;
using Glossary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Term> Terms => Set<Term>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TermConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Term>().HasData(
                new Term
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "abyssal plain",
                    Definition = "The ocean floor offshore from the continental margin, usually very flat with a slight slope.",
                    Status = Status.Published,
                    AuthorId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = DateTime.UtcNow
                },
                new Term
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "accrete",
                    Definition = "v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass.",
                    Status = Status.Published,
                    AuthorId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = DateTime.UtcNow
                },
                new Term
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "alkaline",
                    Definition = "Term pertaining to a highly basic, as opposed to acidic, substance. For example, hydroxide or carbonate of sodium or potassium.",
                    Status = Status.Draft,
                    AuthorId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
