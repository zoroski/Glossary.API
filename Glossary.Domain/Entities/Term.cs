using Glossary.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Domain.Entities
{
    public class Term : BaseEntity
    {
        public string Name { get; set; }

        public string Definition { get; set; }

        public Status Status { get; set; }

        public Guid AuthorId { get; set; }


        public static Term Create(string name, string definition, Guid authorId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(definition))
                throw new ArgumentException("Definition is required");

            return new Term
            {
                Id = Guid.NewGuid(),
                Name = name,
                Definition = definition,
                Status = Glossary.Domain.Status.Draft,
                AuthorId = authorId,
                CreatedAt = DateTime.UtcNow
            };
        }

    }
}
