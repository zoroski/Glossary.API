using Glossary.Application.Common.CQRS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Application.Terms.Comands
{
        public record CreateTermComand(string Name, string Definition) : ICommand<Guid>;

}
