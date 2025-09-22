using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Domain.Dto
{
    public class TermDto
    {

        public TermDto(Guid id, string name, string definition, string status)
        {
            Id = id;
            Name = name;
            Definition = definition;
            this.Status = status;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public string Status { get; set; }
    }
}
