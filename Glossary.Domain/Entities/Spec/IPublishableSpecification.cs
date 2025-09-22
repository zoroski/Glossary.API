using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Domain.Entities.Spec
{
    public interface IPublishableSpecification
    {
        bool IsSatisfiedBy(Term term, out string? reason);
    }
}
