using Glossary.API.Models;
using Glossary.Application.Terms.Comands;
using Glossary.Application.Terms.Queries;
using Glossary.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Glossary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TermController : ControllerBase
    {
        private IMediator _mediator;
        public TermController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTermModel request)
        {
            var id = await _mediator.Send(new CreateTermComand(request.Name, request.Definition));
            return Ok(id);
        }

        [HttpGet]
        public async Task<List<TermDto>> GetAllTerms()
        {
            var terms = await _mediator.Send(new GetTermsQuery());
            return terms.ToList();
        }
    }
}
