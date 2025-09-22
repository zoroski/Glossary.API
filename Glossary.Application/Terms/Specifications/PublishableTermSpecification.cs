using Glossary.Domain.Entities;
using Glossary.Domain.Entities.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Application.Terms.Specifications
{
    public sealed class PublishableTermSpecification : IPublishableSpecification
    {
        private static readonly string[] Forbidden = { "lorem", "test", "sample" };

        public bool IsSatisfiedBy(Term term, out string? reason)
        {
            if (string.IsNullOrWhiteSpace(term.Name))
            { reason = "Name must not be empty."; return false; }

            var def = term.Definition?.Trim() ?? "";
            if (def.Length < 30)
            { reason = "Definition must be at least 30 characters long."; return false; }

            foreach (var w in Forbidden)
                if (def.IndexOf(w, StringComparison.OrdinalIgnoreCase) >= 0)
                { reason = $"Definition contains a forbidden word: '{w}'."; return false; }

            reason = null;
            return true;
        }
    }
}
