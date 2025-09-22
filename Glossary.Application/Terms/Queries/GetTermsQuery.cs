using AutoMapper;
using Glossary.Application.Common.CQRS;
using Glossary.Application.Interfaces;
using Glossary.Domain.Dto;
using Glossary.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Application.Terms.Queries
{
    public record GetTermsQuery() : IQuery<IReadOnlyList<TermDto>>;

    public sealed class GetTermsHandler : IRequestHandler<GetTermsQuery, IReadOnlyList<TermDto>>
    {

        private readonly ITermRepository _repo;
        private readonly IMapper _mapper;
        public GetTermsHandler(ITermRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TermDto>> Handle(GetTermsQuery request, CancellationToken cancellationToken)
        {
           var terms  = _repo.GetAll();
            return _mapper.Map<List<TermDto>>(terms.OrderBy(t => t.Name));
        }

    }
}
