using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Application.Common.CQRS
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
    public interface ICommand : IRequest { }

    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}
