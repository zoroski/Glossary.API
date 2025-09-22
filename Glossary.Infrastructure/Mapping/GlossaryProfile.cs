using AutoMapper;
using Glossary.Domain.Dto;
using Glossary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glossary.Infrastructure.Mapping
{
    public class GlossaryProfile : Profile
    {
        public GlossaryProfile()
        {
            CreateMap<Term, TermDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
