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

        public string Status { get; set; }

        public Guid AuthorId { get; set; }
    }
}
